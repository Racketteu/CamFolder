using CamFolder.Model;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CamFolder.ViewModel
{
    public partial class MainViewModel : ObservableObject
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
                new MenuItem { Title = "Accueil", Icon = PackIconKind.Home, Command = new RelayCommand(ShowHome)},
                new MenuItem { Title = "Déplacer", Icon = PackIconKind.FolderMove, Command = new RelayCommand(ShowMove) },
                new MenuItem { Title = "Renommer", Icon = PackIconKind.Rename, Command = new RelayCommand(ShowRename) },
                new MenuItem { Title = "Paramètres", Icon = PackIconKind.Settings, Command = new RelayCommand(ShowSettings) }
            };
            CurrentViewModel = new HomeViewModel();
        }

        private void ShowHome() => CurrentViewModel = new HomeViewModel();
        private void ShowMove() => CurrentViewModel = new MoveFileViewModel();
        private void ShowRename() => CurrentViewModel = new RenameFileViewModel();
        private void ShowSettings() => CurrentViewModel = new SettingsViewModel();
    }
}
