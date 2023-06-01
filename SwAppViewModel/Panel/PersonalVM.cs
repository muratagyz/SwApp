using SwAppData.Enum;

namespace SwAppViewModel.Panel;

public class PersonalVM
{
    public int Id { get; set; }
    public string PersonalFullName { get; set; }
    public string PersonalPosition { get; set; }
    public string? PersonalImageUrl { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Stat Status { get; set; }
}