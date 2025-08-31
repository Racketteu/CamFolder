using CamFolder.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace CamFolder.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<MenuItem> MenuButton { get; set; }

        public MainViewModel()
        {
            MenuButton = new ObservableCollection<MenuItem>
            {
                new MenuItem { Title = "Accueil", Icon = PackIconKind.Home},
                new MenuItem { Title = "Déplacer", Icon = PackIconKind.FolderMove },
                new MenuItem { Title = "Renommer", Icon = PackIconKind.Rename },
                new MenuItem { Title = "Drive", Icon = PackIconKind.Cloud },
                new MenuItem { Title = "Paramètres", Icon = PackIconKind.Settings }
            };
        }


    }
}
