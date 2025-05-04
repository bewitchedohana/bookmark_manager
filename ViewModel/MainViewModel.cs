using BookmarkManager.Commands;
using BookmarkManager.Models;
using BookmarkManager.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace BookmarkManager.ViewModel;

public sealed class MainViewModel {
    private readonly NewBookmarkWindow _newBookmarWindow;
    private readonly IBookmarkManager _bookmarkManager;

    public ICommand OpenNewBookmarkWindow_Command { get; private set; }

    public ObservableCollection<Bookmark> Bookmarks { get; private set; } = [];

    public MainViewModel(
        NewBookmarkWindow newBookmarkWindow,
        IBookmarkManager bookmarkManager) {
        _newBookmarWindow = newBookmarkWindow;
        _bookmarkManager = bookmarkManager;
        OpenNewBookmarkWindow_Command = new RelayCommand(CanOpenNewWindow, OpenNewWindow);
        LoadItems();
    }

    private void OpenNewWindow(object? obj) {
        MainWindow? mainWindow = obj as MainWindow;
        _newBookmarWindow.Owner = mainWindow;
        _newBookmarWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        _newBookmarWindow.ShowDialog();
        LoadItems();
    }

    private bool CanOpenNewWindow(object? obj) => true;

    private void LoadItems() {
        Bookmarks.Clear();
        List<Bookmark> bookmarks = _bookmarkManager.GetBookmarks();
        foreach(Bookmark bookmark in bookmarks) {
            Bookmarks.Add(bookmark);
        }
    }
}
