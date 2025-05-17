using BookmarkManager.Models;
using System.Collections.ObjectModel;

namespace BookmarkManager.Persistence.Repositories {
    public interface IBookmarkRepository {
        ObservableCollection<Bookmark> Bookmarks { get; }

        void AddBookmark(Bookmark bookmark);
        void DeleteBookmark(Bookmark bookmark);
        void UpdateBookmark(Bookmark bookmark);
    }
}