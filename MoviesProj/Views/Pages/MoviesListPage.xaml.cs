using DomainShared.ViewModels.Genres;
using MoviesProj.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace MoviesProj.Views.Pages
{
    /// <summary>
    /// Interaction logic for MoviesListPage.xaml
    /// </summary>
    public partial class MoviesListPage : INavigableView<MoviesListViewModel>
    {
        public MoviesListViewModel ViewModel { get; }
        public MoviesListPage(MoviesListViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            MvoieTitele_txt.Focus();
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (!IsInitialized)
            {
                return;
            }
            var us = ((GenresListViewModel)args.SelectedItem);
            ViewModel.GenreId = us.Id;
            
        }
        private void Pagination_PageChosen(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            if (!IsInitialized)
            {
                return;
            }
            ViewModel.ChangePageCommand.ExecuteAsync(null);
        }
    }
}
