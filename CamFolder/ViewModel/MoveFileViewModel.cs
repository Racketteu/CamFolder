using CamFolder.Model;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls.Primitives;
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
            KeepStructure = true;
            BrowseSourceCommand = new RelayCommand(BrowseSource);
            BrowseDestinationCommand = new RelayCommand(BrowseDestination);
            MoveFilesCommand = new RelayCommand(MoveFiles);
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
        public ICommand MoveFilesCommand { get; }

        private bool _keepStructure;
        public bool KeepStructure
        {
            get => _keepStructure;
            set { _keepStructure = value; OnPropertyChanged(); }
        }

        private bool _oneFolder;
        public bool OneFolder
        {
            get => _oneFolder;
            set { _oneFolder = value; OnPropertyChanged(); }
        }



        #endregion

        public void MoveFiles()
        {
            if (Directory.Exists(_sourcePath))
            {
                try
                {
                    if(KeepStructure)
                        CopyDirectoryKeepStructure(_sourcePath, _destinationPath);
                    else
                        CopyDirectory(_sourcePath, _destinationPath);
                    MessageBox.Show("Le déplacement de fichiers a été complété avec succès.", "Information");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Une erreur est survenue : {ex.Message}", "Erreur");
                }
            }
            else
            {
                MessageBox.Show("Le dossier source n'existe pas.", "Erreur");
            }
        }

        public void CopyDirectoryKeepStructure(string sourcePath, string destinationPath)
        {
            if(!Directory.Exists(destinationPath))
                Directory.CreateDirectory(destinationPath);

            foreach (string file in Directory.GetFiles(sourcePath))
            {
                try
                {
                    string fileName = Path.GetFileName(file);
                    string destFile = Path.Combine(destinationPath, fileName);
                    File.Copy(file, destFile, true);

                    this.listCopiedFiles.Add(new CopiedFile { Path = sourcePath, Name = file, IsCopied = true });
                }
                catch (Exception ex)
                {
                    this.listCopiedFiles.Add(new CopiedFile { Path = sourcePath, Name = file, IsCopied = false });
                    throw;
                }
            }

            foreach (string subDir in Directory.GetDirectories(sourcePath))
            {
                string dirName = Path.GetFileName(subDir);
                string newDestDir = Path.Combine(destinationPath, dirName);
                CopyDirectoryKeepStructure(subDir, newDestDir);
            }
        }

        public void CopyDirectory(string sourcePath, string destinationPath)
        {
            if (!Directory.Exists(destinationPath))
                Directory.CreateDirectory(destinationPath);

            foreach (string file in Directory.GetFiles(sourcePath))
            {
                try
                {
                    string fileName = Path.GetFileName(file);
                    string destFile = Path.Combine(destinationPath, fileName);
                    File.Copy(file, destFile, true);

                    this.listCopiedFiles.Add(new CopiedFile { Path = sourcePath, Name = file, IsCopied = true });
                }
                catch (Exception ex)
                {
                    this.listCopiedFiles.Add(new CopiedFile { Path = sourcePath, Name = file, IsCopied = false });
                    throw;
                }
            }

            foreach (string subDir in Directory.GetDirectories(sourcePath))
            {
                string dirName = Path.GetFileName(subDir);
                string newDestDir = Path.Combine(destinationPath, dirName);
                CopyDirectory(subDir, destinationPath);
            }
        }

        private void BrowseSource()
        {
            SourcePath = OpenBrowserFile();
        }

        private void BrowseDestination()
        {
            DestinationPath = OpenBrowserFile();
        }

        private string OpenBrowserFile()
        {
            var folderDialog = new OpenFolderDialog
            {
                Multiselect = false,
            };

            if (folderDialog.ShowDialog() == true)
            {
                return folderDialog.FolderName;
            }
            return string.Empty;

        }
    }
}
