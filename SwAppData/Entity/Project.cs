namespace SwAppData.Entity;

public class Project : BaseEntity
{
    public string? ProjectUrl { get; set; }
    public string ProjectCategory { get; set; }
    public string ProjectDetails { get; set; }
    public string? ProjectImageUrl { get; set; }
}