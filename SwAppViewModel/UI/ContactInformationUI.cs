using SwAppData.Enum;

namespace SwAppViewModel.UI;

public class ContactInformationUI
{
    public string ContactInformationTitle { get; set; }
    public string ContactInformationAddress { get; set; }
    public string? ContactInformationDirection { get; set; }
    public Stat Status { get; set; }
}