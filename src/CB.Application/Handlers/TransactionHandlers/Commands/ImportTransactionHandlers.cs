using CB.Application.Models.ImportModels;
using CB.Domain.Common;
using CB.Domain.Constants;
using CB.Domain.Extentions;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;

namespace CB.Application.Handlers.TransactionHandlers.Commands;

public class ImportTransactionCommand : Request<FileResult?> {
    public IFormFile? File { get; set; }
}

public class ImportTransactionHandlers(IServiceProvider serviceProvider) : BaseHandler<ImportTransactionCommand, FileResult?>(serviceProvider) {

    public override async Task<FileResult?> Handle(ImportTransactionCommand request, CancellationToken cancellationToken) {
        CbException.ThrowIf(request.File == null || request.File.Length == 0, Messages.File_NotEmpty);

        var bots = await this.db.Bots.Where(o => !o.IsDelete).ToDictionaryAsync(o => o.Name, cancellationToken);
        var models = new List<ImportTransactionModel>();

        using (var stream = new MemoryStream()) {
            request.File.CopyTo(stream);
            stream.Position = 0;
            using (var workbook = new XLWorkbook(stream)) {
                var worksheet = workbook.Worksheet(1);

                bool headerRow = false;
                foreach (var row in worksheet.RowsUsed()) {
                    if (!headerRow) {
                        headerRow = true;
                        continue;
                    }
                    try {
                        models.Add(new ImportTransactionModel {
                            RowNumber = row.RowNumber(),
                            TransactionDate = DateTime.ParseExact(row.Cell(1).GetString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),  // Cột 1: Day (ngày giao dịch)
                            Name = row.Cell(2).GetString(),               // Cột 2: Name
                            Email = row.Cell(3).GetString(),              // Cột 3: Gmail
                            BrokerSever = row.Cell(4).GetString(),        // Cột 4: Broker Server
                            IDMT4 = (long)row.Cell(5).GetDouble(),        // Cột 5: ID MT4
                            PassView = row.Cell(6).GetString(),           // Cột 6: Pass View
                            PassWeb = row.Cell(7).GetString(),            // Cột 7: Pass Web
                            Banlance = (decimal)row.Cell(8).GetDouble(),  // Cột 8: Vốn (USD)
                            Bot = row.Cell(9).GetString(),                // Cột 9: BOT
                            Ev = (long)row.Cell(10).GetDouble(),          // Cột 10: Ev
                            Ref = (long)row.Cell(11).GetDouble(),         // Cột 11: Ref
                        });
                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                }

                var validator = new TransactionValidator();
                foreach (var model in models) {
                    var result = await validator.ValidateAsync(model, cancellationToken);
                    model.Errors = result.Errors.Select(o => o.ErrorMessage).ToList();
                }

                if (models.Any(o => o.IsError)) {
                    var errorColumn = worksheet.LastColumnUsed().ColumnNumber() + 1;
                    worksheet.Cell(1, errorColumn).SetValue("Lỗi");

                    foreach (var model in models) {
                        if (!model.IsError) continue;
                        worksheet.Cell(model.RowNumber, errorColumn).SetValue(string.Join(". ", model.Errors));
                    }

                    worksheet.Column(errorColumn).AdjustToContents();

                    using (var memoryStream = new MemoryStream()) {
                        workbook.SaveAs(memoryStream);
                        return new FileResult {
                            FileName = request.File.FileName,
                            ByteArray = memoryStream.ToArray()
                        };
                    }
                }
            }
        }

        //await this.db.Brands.AddRangeAsync(insertData, cancellationToken);
        //await this.db.SaveChangesAsync(cancellationToken);

        return null;
    }

}
