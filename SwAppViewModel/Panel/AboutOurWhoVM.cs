using SwAppData.Enum;

namespace SwAppViewModel.Panel;

public class AboutOurWhoVM
{
    public int Id { get; set; }
    public string AboutOurWhoTitle { get; set; }
    public string AboutOurWhoDetails { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Stat Status { get; set; }
}