using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CamFolder.Model
{
    public class MenuItem
    {
        public string Title { get; set; }
        public PackIconKind Icon { get; set; }
        public IRelayCommand Command { get; set; }
    }
}
