using BookmarkManager.ViewModel;
using System.Windows;

namespace BookmarkManager.Views;

public partial class NewBookmarkWindow : Window {
    public NewBookmarkWindow(NewBookmarkViewModel viewModel) {
        InitializeComponent();
        this.DataContext = viewModel;
    }
}
