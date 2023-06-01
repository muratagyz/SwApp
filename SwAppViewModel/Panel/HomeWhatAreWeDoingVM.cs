using SwAppData.Enum;

namespace SwAppViewModel.Panel;

public class HomeWhatAreWeDoingVM
{
    public int Id { get; set; }
    public string HomeWhatAreWeDoingTİtle { get; set; }
    public string HomeWhatAreWeDoingDetail { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Stat Status { get; set; }
}