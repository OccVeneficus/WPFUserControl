using System;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using MainVM;

namespace MainView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Container = ConfigureDependencyInjection();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow();
            var container = Container;
            mainWindow.DataContext = (MainVM.MainVM) ActivatorUtilities.GetServiceOrCreateInstance(container, typeof(MainVM.MainVM));
            mainWindow.Show();
        }

        public IServiceProvider Container { get; }

        IServiceProvider ConfigureDependencyInjection()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IFileWindowService, FileWindowService>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
