using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CamFolder
{
    public partial class App : Application
    {
        public static IServiceProvider serviceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            serviceProvider = serviceCollection.BuildServiceProvider();

            var mainView = serviceProvider.GetService<View.MainView>();
            mainView.DataContext = serviceProvider.GetService<ViewModel.MainViewModel>();
            mainView.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //View
            services.AddTransient<View.MainView>();

            //ViewModels
            services.AddTransient<ViewModel.MainViewModel>();
            services.AddTransient<ViewModel.MenuViewModel>();

        }
    }
}
