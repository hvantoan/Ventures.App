namespace CB.Api.Models.ImportModels;

public class ImportModel {
    public int RowNumber { get; set; }
    public List<string> Errors { get; set; } = [];
    public bool IsError => Errors.Count > 0;
}
