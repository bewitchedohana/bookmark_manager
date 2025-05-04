namespace BookmarkManager.Models;

public interface IBookmarkManager {
    void Create(Bookmark bookmark);
    List<Bookmark> GetBookmarks();
}
