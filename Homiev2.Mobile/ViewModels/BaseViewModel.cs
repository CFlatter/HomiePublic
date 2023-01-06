using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Homiev2.Mobile.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private string _title;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null) =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
