namespace CB.Application.Handlers.ReportHandlers;

public class BotReportQuery : IRequest {
    public int Month { get; set; }
    public int Year { get; set; }
}

internal class BotReportHandler {
}
