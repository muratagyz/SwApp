using SwAppData.Enum;

namespace SwAppViewModel.Panel;

public class ContactInformationVM
{
    public int Id { get; set; }
    public string ContactInformationTitle { get; set; }
    public string ContactInformationAddress { get; set; }
    public string? ContactInformationDirection { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Stat Status { get; set; }
}