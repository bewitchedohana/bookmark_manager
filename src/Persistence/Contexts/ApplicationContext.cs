using BookmarkManager.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace BookmarkManager.Persistence.Contexts;

public class ApplicationContext : DbContext {
    private static string DatabasePath => GetDatabasePath();

    public virtual DbSet<Bookmark> Bookmarks { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlite($"Data Source={DatabasePath}");
        optionsBuilder.UseLazyLoadingProxies();
        base.OnConfiguring(optionsBuilder);
    }

    private static string GetDatabasePath() {
        string baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string databaseFolder = Path.Combine(baseFolder, App.ApplicationName);

        if (!Directory.Exists(databaseFolder)) {
            Directory.CreateDirectory(databaseFolder);
        }

        return Path.Combine(databaseFolder, "Bookmarks.db");
    }
}
