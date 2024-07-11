using Domain.IRepositories;
using DomainShared.ViewModels.Actors;
using DomainShared.ViewModels.Categuries;
using DomainShared.ViewModels.Genres;
using MoviesProj.Views.Pages;
using System.Collections.ObjectModel;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace MoviesProj.ViewModels.Pages
{
    public partial class UpdateMovieViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISnackbarService _snackbarService;
        public UpdateMovieViewModel(IUnitOfWork unitOfWork, ISnackbarService snackbarService, INavigationService navigationService)
        {
            _unitOfWork = unitOfWork;
            _snackbarService = snackbarService;
            _navigationService = navigationService;
        }

        [ObservableProperty]
        private int _constructionYear;

        [ObservableProperty]
        private float _rate;

        [ObservableProperty]
        private Guid _movieId;

        [ObservableProperty]
        private List<Guid> _genresId;

        [ObservableProperty]
        private List<Guid> _catequriesId;

        [ObservableProperty]
        private List<Guid> _actorsId;

        [ObservableProperty]
        private string _name = default!;

        [ObservableProperty]
        private string _directorName = default!;

        [ObservableProperty]
        private ObservableCollection<GenresListViewModel> _genreList;

        [ObservableProperty]
        private ObservableCollection<CateguryListViewModel> _categuryList;

        [ObservableProperty]
        private ObservableCollection<ActorsListViewModel> _actorList;


        [RelayCommand]
        private async Task OnSumbit()
        {
            //var (error, isSuccess) = await _unitOfWork.MoviesRepository.UpdataMovie(MovieId,Name, Rate, CatequryId, GenreId, ConstructionYear, DirectorName);
            //if (!isSuccess)
            //{
            //    _snackbarService.Show("کاربر گرامی", error, ControlAppearance.Secondary, new SymbolIcon(SymbolRegular.Warning20), TimeSpan.FromMilliseconds(3000));
            //    return;
            //}
            _snackbarService.Show("کاربر گرامی", "عملیات با موفقیت انجام شد.", ControlAppearance.Success, new SymbolIcon(SymbolRegular.CheckmarkCircle20), TimeSpan.FromMilliseconds(3000));

            _navigationService.Navigate(typeof(MoviesListPage));
        }
    }
}
