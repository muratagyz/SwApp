using SwAppData.Enum;

namespace SwAppViewModel.Panel;

public class AboutVM
{
    public int Id { get; set; }
    public string AboutTitle { get; set; }
    public string AboutDetails { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Stat Status { get; set; }
}