using SwAppData.Enum;

namespace SwAppData.Entity;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Stat Status { get; set; }
}