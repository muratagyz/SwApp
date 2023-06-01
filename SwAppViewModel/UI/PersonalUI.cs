using SwAppData.Enum;

namespace SwAppViewModel.UI;

public class PersonalUI
{
    public string PersonalFullName { get; set; }
    public string PersonalPosition { get; set; }
    public string? PersonalImageUrl { get; set; }
    public Stat Status { get; set; }
}