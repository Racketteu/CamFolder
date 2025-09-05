using CamFolder.Model;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;

namespace CamFolder.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private object _currentViewModel;
        public object CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(); // Notifie la vue que la propriété a changé
            }
        }


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
            CurrentViewModel = new HomeViewModel();
        }


    }
}
