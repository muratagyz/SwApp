using SwAppData.Enum;

namespace SwAppViewModel.Panel;

public class HomeSliderVM
{
    public int Id { get; set; }
    public string HomeSliderTitle { get; set; }
    public string HomeSliderDetails { get; set; }
    public string HomeSliderImageUrl { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Stat Status { get; set; }
}