using Domain.IRepositories;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace MoviesProj.ViewModels.Pages
{
    public partial class UpdateVideoViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISnackbarService _snackbarService;
        public UpdateVideoViewModel(IUnitOfWork unitOfWork, ISnackbarService snackbarService, INavigationService navigationService)
        {
            _unitOfWork = unitOfWork;
            _snackbarService = snackbarService;
            _navigationService = navigationService;
        }

        [ObservableProperty]
        private int _constructionYear;

        [ObservableProperty]
        private byte _rate;

        [ObservableProperty]
        private Guid _genreId;
        
        [ObservableProperty]
        private Guid _movieId;

        [ObservableProperty]
        private Guid _catequryId;

        [ObservableProperty]
        private string _name = default!;

        [ObservableProperty]
        private string _directorName = default!;

        [RelayCommand]
        private async Task OnSumbit(Type pageType)
        {
            //var (error, isSuccess) = await _unitOfWork.MoviesRepository.UpdataMovie(MovieId,Name, Rate, CatequryId, GenreId, ConstructionYear, DirectorName);
            //if (!isSuccess)
            //{
            //    _snackbarService.Show("کاربر گرامی", error, ControlAppearance.Secondary, new SymbolIcon(SymbolRegular.Warning20), TimeSpan.FromMilliseconds(3000));
            //    return;
            //}
            _snackbarService.Show("کاربر گرامی", "عملیات با موفقیت انجام شد.", ControlAppearance.Success, new SymbolIcon(SymbolRegular.CheckmarkCircle20), TimeSpan.FromMilliseconds(3000));

            _navigationService.Navigate(pageType);
        }
    }
}
