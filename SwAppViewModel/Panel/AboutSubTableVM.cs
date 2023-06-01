using SwAppData.Enum;

namespace SwAppViewModel.Panel;

public class AboutSubTableVM
{
    public int Id { get; set; }
    public string AboutSubTableTitle { get; set; }
    public string AboutSubTableDetails { get; set; }
    public string? AboutSubTableImageUrl { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Stat Status { get; set; }
}