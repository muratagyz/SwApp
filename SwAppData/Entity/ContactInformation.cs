namespace SwAppData.Entity;

public class ContactInformation : BaseEntity
{
    public string ContactInformationTitle { get; set; }
    public string ContactInformationAddress { get; set; }
    public string? ContactInformationDirection { get; set; }
}