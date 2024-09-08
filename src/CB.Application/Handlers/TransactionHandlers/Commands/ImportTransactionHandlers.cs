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

        var bots = await this.db.Bots.ToDictionaryAsync(o => o.Name, cancellationToken);

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

                    models.Add(new ImportTransactionModel {
                        RowNumber = row.RowNumber(),
                        Name = row.Cell(1).GetString(),
                        Email = row.Cell(2).GetString(),
                        BrokerSever = row.Cell(3).GetString(),
                        IDMT4 = (long)row.Cell(4).GetDouble(),
                        PassView = row.Cell(5).GetString(),
                        PassWeb = row.Cell(6).GetString(),
                        Banlance = (decimal)row.Cell(7).GetDouble(),
                        Bot = row.Cell(8).GetString(),
                        Ev = (long)row.Cell(9).GetDouble(),
                        Ref = (long)row.Cell(10).GetDouble(),
                        InCome = (long)row.Cell(11).GetDouble(),
                        OutCome = (long)row.Cell(12).GetDouble(),
                        Description = row.Cell(13).GetString(),
                    });
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

        //var insertData = models.Select(o => new Transaction {
        //    Code = o.Code,
        //    Name = o.Name,
        //    Phone = o.Phone,
        //    Email = o.Email,
        //}).Select(o => o.ToEntity(request.MerchantId)).ToList();
        //await this.db.Brands.AddRangeAsync(insertData, cancellationToken);
        //await this.db.SaveChangesAsync(cancellationToken);

        return null;
    }
}
