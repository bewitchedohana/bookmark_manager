using BookmarkManager.Models;
using BookmarkManager.Persistence.Contexts;
using System.Collections.ObjectModel;

namespace BookmarkManager.Persistence.Repositories;

public sealed class BookmarkRepository : IBookmarkRepository {
    private readonly ApplicationContext _context;

    public ObservableCollection<Bookmark> Bookmarks { get; private set; } = [];

    public BookmarkRepository(ApplicationContext context) {
        _context = context;
        LoadBookmarks();
        Bookmarks = _context.Bookmarks.Local.ToObservableCollection();
    }

    public void DeleteBookmark(Bookmark bookmark) {
        _context.Bookmarks.Remove(bookmark);
        _context.SaveChanges();
    }

    public void UpdateBookmark(Bookmark bookmark) {
        _context.Bookmarks.Update(bookmark);
        _context.SaveChanges();
    }

    public void AddBookmark(Bookmark bookmark) {
        _context.Bookmarks.Add(bookmark);
        _context.SaveChanges();
    }

    private void LoadBookmarks() => _context.Bookmarks.ToList();
}
