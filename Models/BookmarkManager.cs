using SQLite;
using System.IO;

namespace BookmarkManager.Models;

internal sealed class BookmarkManager : IBookmarkManager {
    private readonly string DatabasePath;
    private readonly string ApplicationName;

    public BookmarkManager() {
        ApplicationName = "BookmarkManager";
        string baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string databaseFolder = Path.Combine(baseFolder, ApplicationName);

        if (!Directory.Exists(databaseFolder)) { 
            Directory.CreateDirectory(databaseFolder);
        }

        DatabasePath = Path.Combine(databaseFolder, "Bookmarks.db");
    }

    public void Create(Bookmark bookmark) {
        using SQLiteConnection connection = new(DatabasePath);
        connection.CreateTable<Bookmark>();
        connection.Insert(bookmark);
    }

    public void Delete(Bookmark bookmark) {
        using SQLiteConnection connection = new(DatabasePath);
        connection.CreateTable<Bookmark>();
        connection.Delete(bookmark);
    }

    public List<Bookmark> GetBookmarks() {
        using SQLiteConnection connection = new(DatabasePath);
        connection.CreateTable<Bookmark>();
        return [.. connection.Table<Bookmark>()];
    }

    public void Update(Bookmark bookmark) {
        using SQLiteConnection connection = new(DatabasePath);
        connection.CreateTable<Bookmark>();
        connection.Update(bookmark);
    }
}
