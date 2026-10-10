using MTWalk.Services;
using MTWalk.WPF.Converters;
using MTWalk.WPF.Settings.Expert;
using System.ComponentModel;

namespace MTWalk.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        Services = ConfigureServices();
        this.InitializeComponent();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        if (ST.Default.UpgradeRequired)
        {
            ST.Default.Upgrade();
            ST.Default.UpgradeRequired = false;
            ST.Default.Save();
        }
        LocalizedEnumConverter.AddAssembly(typeof(ExecutionMode).Assembly);
        LocalizedEnumConverter.AddType<DateMode>();
    }

    /// <summary>
    /// Gets the current <see cref="App"/> instance in use
    /// </summary>
    public new static App? Current => Application.Current as App;

    /// <summary>
    /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
    /// </summary>
    public IServiceProvider Services { get; }

    /// <summary>
    /// Configures the services for the application.
    /// </summary>
    static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddWalker();
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<ExecutorViewModel>();
        services.AddSingleton<ResultGraphViewModel>();
        services.AddSingleton<InputsViewModel>();
        services.AddSingleton<SettingsViewModel>();
        services.AddTransient<FileViewModel>();
        services.AddTransient<DirectoryViewModel>();

        return services.BuildServiceProvider();
    }
}
