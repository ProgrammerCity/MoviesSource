using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace MoviesProj.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "MoviesProj";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Home",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(Views.Pages.DashboardPage)
            },
            new NavigationViewItem()
            {
                Content = "moviesList",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
                TargetPageType = typeof(Views.Pages.MoviesListPage),
                MenuItemsSource = new ObservableCollection<object>
                {
                    new NavigationViewItem("ListMovies", SymbolRegular.DataHistogram24 ,typeof(Views.Pages.MoviesListPage)),
                    new NavigationViewItem("CreateMovie", SymbolRegular.DataHistogram24 ,typeof(Views.Pages.CreateMoviesPage)),
                    new NavigationViewItem("update", SymbolRegular.DataHistogram24 ,typeof(Views.Pages.UpdateMoviePage)),
                }
            },
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };
    }
}
