namespace SwAppData.Entity;

public class Log : BaseEntity
{
    public string Name { get; set; }
    public string Detail { get; set; }
    public string? NesneId { get; set; }
}