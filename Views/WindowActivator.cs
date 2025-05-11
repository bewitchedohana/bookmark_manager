using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Windows;

namespace BookmarkManager.Views;

internal sealed class WindowActivator(
    IServiceScopeFactory serviceScopeFactory) : IWindowActivator {
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

    [STAThread]
    public T Activate<T>() {
        Type windowType = typeof(T);
        bool isWindowType = windowType.BaseType is not null && windowType.BaseType.Equals(typeof(Window));
        if (!isWindowType) {
            throw new ArgumentException($"The type {windowType.FullName} is not a {nameof(Window)}");
        }

        Assembly viewAssembly = typeof(WindowActivator).Assembly;
        IEnumerable<Type> assemblyTypes = viewAssembly.GetTypes().AsEnumerable();

        bool isValidType = assemblyTypes.Any(at => (at.FullName ?? string.Empty).Equals(windowType.FullName));
        if (!isValidType) {
            throw new InvalidOperationException($"Type {windowType} does not exists in {viewAssembly.FullName}");
        }

        ConstructorInfo? constructor = windowType.GetConstructors().FirstOrDefault();
        ArgumentNullException.ThrowIfNull(constructor, nameof(constructor));

        IServiceProvider serviceProvider = _serviceScopeFactory.CreateScope().ServiceProvider;
        ParameterInfo[] parameters = constructor.GetParameters();
        object[] resolvedParameters = parameters.Select(p => serviceProvider.GetRequiredService(p.ParameterType)).ToArray();

        T? instance = (T?)Activator.CreateInstance(windowType, resolvedParameters);
        ArgumentNullException.ThrowIfNull(instance, nameof(instance));

        return instance;
    }
}
