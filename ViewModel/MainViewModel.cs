using BookmarkManager.Commands;
using BookmarkManager.Views;
using System.Windows;
using System.Windows.Input;

namespace BookmarkManager.ViewModel;

public sealed class MainViewModel {
    private readonly NewBookmarkWindow _newBookmarWindow;
    public ICommand OpenNewBookmarkWindow_Command { get; private set; }

    public MainViewModel(
        NewBookmarkWindow newBookmarkWindow) {
        _newBookmarWindow = newBookmarkWindow;
        OpenNewBookmarkWindow_Command = new RelayCommand(CanOpenNewWindow, OpenNewWindow);
    }

    private void OpenNewWindow(object? obj) {
        MainWindow? mainWindow = obj as MainWindow;
        _newBookmarWindow.Owner = mainWindow;
        _newBookmarWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        _newBookmarWindow.ShowDialog();
    }

    private bool CanOpenNewWindow(object? obj) => true;
}
