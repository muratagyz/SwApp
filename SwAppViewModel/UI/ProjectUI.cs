using SwAppData.Enum;

namespace SwAppViewModel.UI;

public class ProjectUI
{
    public string? ProjectUrl { get; set; }
    public string ProjectCategory { get; set; }
    public string ProjectDetails { get; set; }
    public string? ProjectImageUrl { get; set; }
    public Stat Status { get; set; }
}