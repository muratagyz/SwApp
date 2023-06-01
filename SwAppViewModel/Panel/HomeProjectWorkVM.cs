using SwAppData.Enum;

namespace SwAppViewModel.Panel;

public class HomeProjectWorkVM
{
    public int Id { get; set; }
    public string HomeProjectWorkSubTitle { get; set; }
    public string HomeProjectWorkSubDetails { get; set; }
    public string HomeProjectWorkImageUrl { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Stat Status { get; set; }
}