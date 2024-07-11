using MoviesProj.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace MoviesProj.Views.Pages
{
    /// <summary>
    /// Interaction logic for UpdateMoviePage.xaml
    /// </summary>
    public partial class UpdateMoviePage : INavigableView<UpdateMovieViewModel>
    {
        public UpdateMovieViewModel ViewModel { get; }
        public UpdateMoviePage(UpdateMovieViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
