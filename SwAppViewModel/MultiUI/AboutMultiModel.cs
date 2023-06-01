using SwAppViewModel.UI;

namespace SwAppViewModel.MultiUI;

public class AboutMultiModel
{
    public AboutOurWhoUI AboutOurWhoUI { get; set; }
    public List<HomeWhySorswareUI> HomeWhySorswareUI { get; set; }
    public List<AboutSubTableUI> AboutSubTableUI { get; set; }
    public AboutUI AboutUI { get; set; }
}