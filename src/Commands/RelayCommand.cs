using System.Windows.Input;

namespace BookmarkManager.Commands;

public sealed class RelayCommand(
    Predicate<object?> canExecute,
    Action<object?> execute) : ICommand {
    private readonly Action<object?> _execute = execute;
    private readonly Predicate<object?> _predicate = canExecute;

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) 
        => _predicate(parameter);

    public void Execute(object? parameter) 
        => _execute(parameter);

    public void RaiseCanExecuteChanged()
        => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
