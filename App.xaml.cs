using BookmarkManager.Models;
using BookmarkManager.ViewModel;
using BookmarkManager.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace BookmarkManager;

public partial class App : Application {
    private readonly IServiceProvider _serviceProvider;

    public App() {
        IServiceCollection serviceCollection = new ServiceCollection();
        _serviceProvider = ConfigureServices(serviceCollection).BuildServiceProvider();
    }

    private IServiceCollection ConfigureServices(IServiceCollection services) {
        services.AddSingleton<MainWindow, MainWindow>();
        services.AddSingleton<MainViewModel, MainViewModel>();

        services.AddScoped<IBookmarkManager, Models.BookmarkManager>();

        services.AddScoped<NewBookmarkWindow, NewBookmarkWindow>();
        services.AddScoped<NewBookmarkViewModel, NewBookmarkViewModel>();        

        return services;
    }

    private void Application_Startup(object sender, StartupEventArgs e) {
        _serviceProvider.GetRequiredService<MainWindow>().Show();
    }
}
