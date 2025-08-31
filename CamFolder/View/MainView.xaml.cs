using CamFolder.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CamFolder.View
{
    /// <summary>
    /// Logique d'interaction pour MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = App.serviceProvider.GetService<MainViewModel>();
        }
    }
}
