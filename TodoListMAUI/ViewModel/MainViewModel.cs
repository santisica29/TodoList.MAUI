using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace TodoListMAUI.ViewModel;
public partial class MainViewModel : ObservableObject
{
    IConnectivity connectivity;
    public MainViewModel(IConnectivity connectivity)
    {
        items = new ObservableCollection<string>();
        this.connectivity = connectivity;
    }
    [ObservableProperty]
     ObservableCollection<string> items;

    [ObservableProperty]
    string text;

    [RelayCommand]
    async Task Add()
    {
        if (string.IsNullOrWhiteSpace(Text))
            return;

        if (connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("Error","No Internet", "Ok");
        }

        // add our item
        Items.Add(Text);
        Text = string.Empty;
    }

    [RelayCommand]
    void Delete(string s)
    {
        // Items.Contains is not necessary since Items.Remove first search if it exists
        Items.Remove(s);
    }

    [RelayCommand]
    async Task Tap(string s)
    {
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
    }
}
