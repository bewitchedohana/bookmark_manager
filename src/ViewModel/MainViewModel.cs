using BookmarkManager.Commands;
using BookmarkManager.Models;
using BookmarkManager.Persistence.Repositories;
using BookmarkManager.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace BookmarkManager.ViewModel;

public sealed class MainViewModel : INotifyPropertyChanged {
    private readonly UpdateBookmarkViewModel _updateBookmarkViewModel;
    private readonly IWindowActivator _windowActivator;
    private readonly IBookmarkRepository _bookmarkRepository;

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

    public ICommand OpenURL_Command { get; private set; }

    public ObservableCollection<Bookmark> Bookmarks => _bookmarkRepository.Bookmarks;

    public MainViewModel(
        IWindowActivator windowActivator,
        IBookmarkRepository bookmarkRepository,
        UpdateBookmarkViewModel updateBookmarkViewModel) {
        _windowActivator = windowActivator;
        _bookmarkRepository = bookmarkRepository;
        _updateBookmarkViewModel = updateBookmarkViewModel;

        OpenNewBookmarkWindow_Command = new RelayCommand(CanOpenNewWindow, OpenNewWindow);
        OpenUpdateBookmarkWindow_Command = new RelayCommand(CanOpenUpdateWindow, OpenUpdateWindow);
        DeleteBookmark_Command = new RelayCommand(CanDeleteBookmark, DeleteBookmark);
        OpenURL_Command = new RelayCommand(CanOpenURL, OpenURL);
    }

    private void OpenURL(object? obj) {
        ProcessStartInfo processInfo = new ProcessStartInfo(SelectedBookmark!.Url) {
             UseShellExecute = true
        };

        Process.Start(processInfo);
    }

    private bool CanOpenURL(object? obj) => SelectedBookmark is not null;

    private void DeleteBookmark(object? obj) {
        ArgumentNullException.ThrowIfNull(SelectedBookmark, nameof(SelectedBookmark));
        MessageBoxResult response = MessageBox.Show(
            "Are you sure you want to delete this bookmark?",
            "Delete bookmark?",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (response.Equals(MessageBoxResult.Yes)) { 
            _bookmarkRepository.DeleteBookmark(SelectedBookmark);
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
    }

    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void OpenNewWindow(object? obj) {
        MainWindow? mainWindow = obj as MainWindow;
        NewBookmarkWindow newBookmarkWindow = _windowActivator.Activate<NewBookmarkWindow>();
        newBookmarkWindow.Owner = mainWindow;
        newBookmarkWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        newBookmarkWindow.ShowDialog();
    }

    private bool CanOpenNewWindow(object? obj) => true;
}
