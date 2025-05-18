using BookmarkManager.Commands;
using BookmarkManager.ViewModel;
using System.Windows;

namespace BookmarkManager;

public partial class MainWindow : Window {
    private MainViewModel? ViewModel => DataContext as MainViewModel;
    public MainWindow(MainViewModel viewModel) {
        InitializeComponent();
        this.DataContext = viewModel;
    }

    private async void StackPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
        if (e.ClickCount != 2) {
            e.Handled = true;
            return;
        }
        RelayCommand? command = (ViewModel!.OpenURL_Command as RelayCommand);
        ArgumentNullException.ThrowIfNull(command);

        await Task.Delay(50);
        command.RaiseCanExecuteChanged();
        command.Execute(null);
    }
}