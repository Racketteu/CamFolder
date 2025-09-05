using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows.Input;

namespace CamFolder.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {
        public HomeViewModel()
        {
        
        }

        [RelayCommand]
        private void OpenGitLab()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/Racketteu/CamFolder",
                UseShellExecute = true
            });
        }
    }
}
