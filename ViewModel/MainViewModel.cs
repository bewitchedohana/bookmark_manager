using BookmarkManager.Commands;
using BookmarkManager.Models;
using BookmarkManager.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace BookmarkManager.ViewModel;

public sealed class MainViewModel : INotifyPropertyChanged {
    private readonly UpdateBookmarkViewModel _updateBookmarkViewModel;
    private readonly IBookmarkManager _bookmarkManager;
    private readonly IWindowActivator _windowActivator;

    public event PropertyChangedEventHandler? PropertyChanged;

    private Bookmark? _selectedBookmark;

    public Bookmark? SelectedBookmark {
        get => _selectedBookmark;
        set {
            _selectedBookmark = value;
            NotifyPropertyChanged();
            (OpenUpdateBookmarkWindow_Command as RelayCommand)!.RaiseCanExecuteChanged();
            (DeleteBookmark_Command as RelayCommand)!.RaiseCanExecuteChanged();
        }
    }

    public ICommand OpenNewBookmarkWindow_Command { get; private set; }

    public ICommand OpenUpdateBookmarkWindow_Command { get; private set; }

    public ICommand DeleteBookmark_Command { get; private set; }

    public ObservableCollection<Bookmark> Bookmarks { get; private set; } = [];

    public MainViewModel(
        IWindowActivator windowActivator,
        IBookmarkManager bookmarkManager,
        UpdateBookmarkViewModel updateBookmarkViewModel) {
        _windowActivator = windowActivator;
        _bookmarkManager = bookmarkManager;
        _updateBookmarkViewModel = updateBookmarkViewModel;

        OpenNewBookmarkWindow_Command = new RelayCommand(CanOpenNewWindow, OpenNewWindow);
        OpenUpdateBookmarkWindow_Command = new RelayCommand(CanOpenUpdateWindow, OpenUpdateWindow);
        DeleteBookmark_Command = new RelayCommand(CanDeleteBookmark, DeleteBookmark);

        LoadItems();
    }

    private void DeleteBookmark(object? obj) {
        ArgumentNullException.ThrowIfNull(SelectedBookmark, nameof(SelectedBookmark));
        MessageBoxResult response = MessageBox.Show(
            "Are you sure you want to delete this bookmark?",
            "Delete bookmark?",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (response.Equals(MessageBoxResult.Yes)) { 
            _bookmarkManager.Delete(SelectedBookmark);
            LoadItems();
        }
    }

    private bool CanDeleteBookmark(object? obj) => SelectedBookmark is not null;

    private bool CanOpenUpdateWindow(object? obj) => SelectedBookmark is not null;

    private void OpenUpdateWindow(object? obj) {
        MainWindow? mainWindow = obj as MainWindow;
        _updateBookmarkViewModel.SelectedBookmark = SelectedBookmark;

        UpdateBookmarkWindow updateBookmarkWindow = _windowActivator.Activate<UpdateBookmarkWindow>();
        updateBookmarkWindow.Owner = mainWindow;
        updateBookmarkWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        updateBookmarkWindow.WindowStyle = WindowStyle.None;
        updateBookmarkWindow.ShowDialog();

        LoadItems();
    }

    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void OpenNewWindow(object? obj) {
        MainWindow? mainWindow = obj as MainWindow;
        NewBookmarkWindow newBookmarkWindow = _windowActivator.Activate<NewBookmarkWindow>();
        newBookmarkWindow.Owner = mainWindow;
        newBookmarkWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        newBookmarkWindow.ShowDialog();
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
