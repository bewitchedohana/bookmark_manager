using BookmarkManager.Commands;
using BookmarkManager.Models;
using BookmarkManager.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace BookmarkManager.ViewModel;

public sealed class UpdateBookmarkViewModel : INotifyPropertyChanged {
    private readonly IBookmarkManager _bookmarkManager;
	private Bookmark? _selectedBookmark;

	public  Bookmark? SelectedBookmark {
		get => _selectedBookmark;
		set { 
			_selectedBookmark = value;
			NotifyPropertyChanged();
		}
	}

	public event PropertyChangedEventHandler? PropertyChanged;

    public ICommand CloseWindow_Command { get; private set; }
    public ICommand UpdateBookmark_Command { get; private set; }

    public UpdateBookmarkViewModel(IBookmarkManager bookmarkManager) {
        _bookmarkManager = bookmarkManager;

		CloseWindow_Command = new RelayCommand(CanCloseWindow, CloseWindow);
        UpdateBookmark_Command = new RelayCommand(CanUpdateBookmark, UpdateBookmark);
    }

    private void SelectedBookmark_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        => (UpdateBookmark_Command as RelayCommand)?.RaiseCanExecuteChanged();

    private void UpdateBookmark(object? obj) {
        ArgumentNullException.ThrowIfNull(SelectedBookmark, nameof(SelectedBookmark));
        _bookmarkManager.Update(SelectedBookmark);

        if (obj is not null) {
            UpdateBookmarkWindow? window = obj as UpdateBookmarkWindow;
            window!.Visibility = Visibility.Hidden;
        }
    }

    private bool CanUpdateBookmark(object? obj) {
        return SelectedBookmark is not null
            && !string.IsNullOrWhiteSpace(SelectedBookmark.Name)
            && !string.IsNullOrWhiteSpace(SelectedBookmark.Url)
            && Uri.TryCreate(SelectedBookmark.Url, UriKind.Absolute, out Uri? urlResult)
            && urlResult is not null
            && (urlResult.Scheme.Equals(Uri.UriSchemeHttp) || urlResult.Scheme.Equals(Uri.UriSchemeHttps));
    }

    private void CloseWindow(object? obj) {
        UpdateBookmarkWindow? window = obj as UpdateBookmarkWindow;
        ArgumentNullException.ThrowIfNull(window, nameof(window));
        window.Visibility = Visibility.Hidden;
    }

    private bool CanCloseWindow(object? obj) => true;

    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        if (propertyName.Equals(nameof(SelectedBookmark))) {
            SelectedBookmark!.PropertyChanged += SelectedBookmark_PropertyChanged;
        }
    }
}
