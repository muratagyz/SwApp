using SwAppData.Enum;

namespace SwAppViewModel.Panel;

public class ContactInfoVM
{
    public int Id { get; set; }
    public string OfficeLocationName { get; set; }
    public string OfficeLocationNumber { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Stat Status { get; set; }
}