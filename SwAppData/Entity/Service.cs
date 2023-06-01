namespace SwAppData.Entity;

public class Service : BaseEntity
{
    public string ServiceTitle { get; set; }
    public string ServiceDetails { get; set; }
    public string? ServiceImageUrl { get; set; }
}