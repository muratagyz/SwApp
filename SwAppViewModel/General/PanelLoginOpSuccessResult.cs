using SwAppData.Enum;

namespace SwAppViewModel.General;

public class PanelLoginOpSuccessResult : PanelLoginResult
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? Token { get; set; }
    public Role Role { get; set; }
}