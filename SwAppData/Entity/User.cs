using SwAppData.Enum;

namespace SwAppData.Entity;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string? Token { get; set; }
    public Role Role { get; set; }
}