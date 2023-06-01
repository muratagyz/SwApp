using SwAppData.Enum;

namespace SwAppViewModel.Panel;

public class UserVM
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Stat Status { get; set; }
    public Role Role { get; set; }
}