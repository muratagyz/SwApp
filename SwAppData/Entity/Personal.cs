namespace SwAppData.Entity;

public class Personal : BaseEntity
{
    public string PersonalFullName { get; set; }
    public string PersonalPosition { get; set; }
    public string? PersonalImageUrl { get; set; }
}