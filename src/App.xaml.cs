using BookmarkManager.Persistence.Contexts;
using BookmarkManager.Persistence.Repositories;
using BookmarkManager.ViewModel;
using BookmarkManager.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace BookmarkManager;

public partial class App : Application {
    public const string ApplicationName = "BookmarkManager";
    private readonly IServiceProvider _serviceProvider;

    public App() {
        IServiceCollection serviceCollection = new ServiceCollection();
        _serviceProvider = ConfigureServices(serviceCollection).BuildServiceProvider();
    }

    private IServiceCollection ConfigureServices(IServiceCollection services) {
        services.AddDbContext<ApplicationContext>();
        services.AddScoped<IWindowActivator, WindowActivator>();

        services.AddSingleton<MainWindow, MainWindow>();
        services.AddSingleton<MainViewModel, MainViewModel>();

        services.AddSingleton<IBookmarkRepository, BookmarkRepository>();

        services.AddSingleton<NewBookmarkWindow, NewBookmarkWindow>();
        services.AddScoped<NewBookmarkViewModel, NewBookmarkViewModel>();       
        
        services.AddSingleton<UpdateBookmarkViewModel, UpdateBookmarkViewModel>();
        services.AddSingleton<UpdateBookmarkWindow, UpdateBookmarkWindow>();

        return services;
    }

    private void Application_Startup(object sender, StartupEventArgs e) {
        IWindowActivator windowActivator = _serviceProvider.GetRequiredService<IWindowActivator>();
        MainWindow mainWindow = windowActivator.Activate<MainWindow>();
        mainWindow.Show();
    }
}
