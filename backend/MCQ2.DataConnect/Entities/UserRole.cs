namespace MCQ3.DataConnect.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public ICollection<UserAccount> Users { get; set; } = new List<UserAccount>();
}