using SwAppData.Enum;

namespace SwAppViewModel.Panel;

public class ProjectVM
{
    public int Id { get; set; }
    public string? ProjectUrl { get; set; }
    public string ProjectCategory { get; set; }
    public string ProjectDetails { get; set; }
    public string? ProjectImageUrl { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Stat Status { get; set; }
}