using SQLite;

namespace BookmarkManager.Models;

public abstract class BaseModel {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Column("created_on")]
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    [Column("modified_on")]
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;

    protected BaseModel() { }
}
