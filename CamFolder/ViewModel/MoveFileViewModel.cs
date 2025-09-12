using CamFolder.Model;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace CamFolder.ViewModel
{
    public class MoveFileViewModel : INotifyPropertyChanged
    {
        public List<CopiedFile> listCopiedFiles = new List<CopiedFile>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public MoveFileViewModel()
        {
            BrowseSourceCommand = new RelayCommand(BrowseSource);
            BrowseDestinationCommand = new RelayCommand(BrowseDestination);
            MoveFileCommand = new RelayCommand(MoveFiles);
        }

        #region MVVM Front
        private string _sourcePath { get; set; }
        public string SourcePath
        {
            get => _sourcePath;
            set { _sourcePath = value; OnPropertyChanged(); }
        }

        private string _destinationPath;
        public string DestinationPath
        {
            get => _destinationPath;
            set { _destinationPath = value; OnPropertyChanged(); }
        }

        public ICommand BrowseSourceCommand { get; }
        public ICommand BrowseDestinationCommand { get; }
        public ICommand MoveFileCommand { get; }



        #endregion

        public void MoveFiles()
        {
            if (Directory.Exists(_sourcePath))
            {
                try
                {
                    CopyDirectory(_sourcePath, _destinationPath);
                    MessageBox.Show("Le déplacement de fichiers a été complété avec succès.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Une erreur est survenue : {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Le dossier source n'existe pas.");
            }
        }

        public void CopyDirectory(string sourceDir, string destDir)
        {
            Directory.CreateDirectory(destDir);

            foreach (string file in Directory.GetFiles(sourceDir))
            {
                try
                {
                    string fileName = Path.GetFileName(file);
                    string destFile = Path.Combine(destDir, fileName);
                    File.Copy(file, destFile, true);
                    Console.WriteLine($"Fichier copié : {fileName}");
                    this.listCopiedFiles.Add(new CopiedFile { Path = sourceDir, Name = file, IsCopied = true });
                }
                catch (Exception ex)
                {
                    this.listCopiedFiles.Add(new CopiedFile { Path = sourceDir, Name = file, IsCopied = false });
                    throw;
                }
            }

            foreach (string subDir in Directory.GetDirectories(sourceDir))
            {
                string dirName = Path.GetFileName(subDir);
                string newDestDir = Path.Combine(destDir, dirName);
                CopyDirectory(subDir, newDestDir);
            }
        }

        private void BrowseSource()
        {
            SourcePath = "C:\\Source";
        }

        private void BrowseDestination()
        {
            DestinationPath = "C:\\Destination";
        }

    }
}
