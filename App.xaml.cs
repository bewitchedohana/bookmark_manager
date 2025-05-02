using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace BookmarkManager;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application {
    private readonly IServiceProvider _serviceProvider;

    public App() {
        IServiceCollection serviceCollection = new ServiceCollection();
        _serviceProvider = ConfigureServices(serviceCollection).BuildServiceProvider();
    }

    private IServiceCollection ConfigureServices(IServiceCollection services) {
        services.AddSingleton<MainWindow, MainWindow>();
        return services;
    }

    private void Application_Startup(object sender, StartupEventArgs e) {
        _serviceProvider.GetRequiredService<MainWindow>().Show();
    }
}
