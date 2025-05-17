namespace BookmarkManager.Models;

public abstract class BaseModel {
    public Guid Id { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;

    protected BaseModel() { }
}
