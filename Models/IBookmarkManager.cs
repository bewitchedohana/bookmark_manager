namespace BookmarkManager.Models;

public interface IBookmarkManager {
    void Create(Bookmark bookmark);
    void Update(Bookmark bookmark);
    List<Bookmark> GetBookmarks();
}
