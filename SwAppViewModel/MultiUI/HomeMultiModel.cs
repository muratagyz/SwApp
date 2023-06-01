using SwAppViewModel.UI;

namespace SwAppViewModel.MultiUI;

public class HomeMultiModel
{
    public List<HomeSliderUI> HomeSliderUI { get; set; }
    public List<HomeWhySorswareUI> HomeWhySorswareUI { get; set; }
    public List<HomeProjectWorkUI> HomeProjectWorkUI { get; set; }
    public List<ProjectUI> ProjectUI { get; set; }
    public HomeWhatAreWeDoingUI HomeWhatAreWeDoingUI { get; set; }
    public ContactMessage ContactMessage { get; set; }
}