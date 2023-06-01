using SwAppData.Enum;

namespace SwAppViewModel.Panel;

public class ServiceVM
{
    public int Id { get; set; }
    public string ServiceTitle { get; set; }
    public string ServiceDetails { get; set; }
    public string ServiceImageUrl { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Stat Status { get; set; }
}