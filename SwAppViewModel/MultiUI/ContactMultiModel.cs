using SwAppViewModel.UI;

namespace SwAppViewModel.MultiUI;

public class ContactMultiModel
{
    public List<ContactInfoUI> ContactInfoUI { get; set; }
    public List<ContactInformationUI> ContactInformationUI { get; set; }
    public ContactMessage ContactMessage { get; set; }
}