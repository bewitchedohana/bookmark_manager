using BookmarkManager.ViewModel;
using System.Windows;

namespace BookmarkManager;

public partial class MainWindow : Window {
    public MainWindow(MainViewModel viewModel) {
        InitializeComponent();
        this.DataContext = viewModel;
    }
}