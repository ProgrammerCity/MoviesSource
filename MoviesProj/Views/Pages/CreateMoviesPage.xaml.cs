using DomainShared.ViewModels.Actors;
using DomainShared.ViewModels.Categuries;
using DomainShared.ViewModels.Genres;
using MoviesProj.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace MoviesProj.Views.Pages
{
    /// <summary>
    /// Interaction logic for MoviesPage.xaml
    /// </summary>
    public partial class CreateMoviesPage : INavigableView<CreateMoviesViewModel>
    {
        public CreateMoviesViewModel ViewModel { get; }
        public CreateMoviesPage(CreateMoviesViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

        private async void Btn_submit_Click(object sender, RoutedEventArgs e)
        {
            foreach (GenresListViewModel itm in lst_gen.SelectedItems)
            {
                ViewModel.GenresId.Add(itm.Id);
            };

            foreach (CateguryListViewModel itm in lst_cat.SelectedItems)
            {
                ViewModel.CatequriesId.Add(itm.Id);

            }

            foreach (ActorsListViewModel itm in lst_act.SelectedItems)
            {
                ViewModel.ActorsId.Add(itm.Id);

            }
            await ViewModel.SumbitCommand.ExecuteAsync(null);
        }
    }
}
