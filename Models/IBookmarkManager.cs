namespace BookmarkManager.Models;

public interface IBookmarkManager {
    void Create(Bookmark bookmark);
    void Update(Bookmark bookmark);
    void Delete(Bookmark bookmark);
    List<Bookmark> GetBookmarks();
}
