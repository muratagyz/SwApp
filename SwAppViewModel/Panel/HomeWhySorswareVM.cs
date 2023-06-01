using SwAppData.Enum;

namespace SwAppViewModel.Panel;

public class HomeWhySorswareVM
{
    public int Id { get; set; }
    public string HomeWhySorswareSubTitle { get; set; }
    public string HomeWhySorswareSubDetails { get; set; }
    public string HomeWhySorswareSubImageUrl { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Stat Status { get; set; }
}