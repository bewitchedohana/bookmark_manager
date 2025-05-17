using BookmarkManager.ViewModel;
using System.Windows;

namespace BookmarkManager.Views;

public partial class UpdateBookmarkWindow : Window {
    public UpdateBookmarkWindow(UpdateBookmarkViewModel viewModel) {
        InitializeComponent();
        this.DataContext = viewModel;
    }
}
