namespace Application.Models;

public class NoteParameters
{
    public int? MinId { get; set; }
    public int? MaxId { get; set; }
    public int? MinKey { get; set; }
    public int? MaxKey { get; set; }
    public bool? FromEnd { get; set; }
    public int? Count { get; set; }
    public int? Page { get; set; }
}