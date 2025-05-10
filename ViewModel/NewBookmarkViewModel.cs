using BookmarkManager.Commands;
using BookmarkManager.Models;
using BookmarkManager.Views;
using System.ComponentModel;
using System.Windows.Input;

namespace BookmarkManager.ViewModel;

public sealed class NewBookmarkViewModel : INotifyPropertyChanged {
    private readonly IBookmarkManager _bookmarkManager;

    private string _name = string.Empty;

    public string Name {
        get => _name;
        set {
            _name = value;
            OnPropertyChanged(nameof(Name));
            RaiseCanCreateChanged();
        }
    }

    private string _url = string.Empty;

    public string Url {
        get => _url;
        set {
            _url = value;
            OnPropertyChanged(nameof(Url));
            RaiseCanCreateChanged();
        }
    }

    public ICommand CloseWindow_Command { get; private set; }

    public ICommand CreateBookmark_Command { get; private set; }
    
    public NewBookmarkViewModel(IBookmarkManager bookmarkManager) {
        _bookmarkManager = bookmarkManager;

        CloseWindow_Command = new RelayCommand(CanCloseWindow, CloseWindow);
        CreateBookmark_Command = new RelayCommand(CanCreateBookmark, CreateBookmark);
    }

    private void CreateBookmark(object? obj) {
        Bookmark bookmark = new(Url, Name);
        _bookmarkManager.Create(bookmark);
        CloseWindow(obj);
    }

    private bool CanCreateBookmark(object? obj) {
        return !string.IsNullOrWhiteSpace(Name)
            && !string.IsNullOrWhiteSpace(Url)
            && Uri.TryCreate(Url, UriKind.Absolute, out Uri? urlResult)
            && urlResult is not null
            && (urlResult.Scheme.Equals(Uri.UriSchemeHttp) || urlResult.Scheme.Equals(Uri.UriSchemeHttps));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new(propertyName));
    }

    private void CloseWindow(object? obj) {
        if (obj is not null) {
            NewBookmarkWindow? newBookmarkWindow = obj as NewBookmarkWindow;
            ArgumentNullException.ThrowIfNull(newBookmarkWindow, nameof(obj));
            newBookmarkWindow.Visibility = System.Windows.Visibility.Hidden;
        }
    }

    private bool CanCloseWindow(object? obj) => true;

    private void RaiseCanCreateChanged()
        => ((RelayCommand)CreateBookmark_Command).RaiseCanExecuteChanged();
}
