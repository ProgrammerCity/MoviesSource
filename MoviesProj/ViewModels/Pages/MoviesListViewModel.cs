using Domain.IRepositories;
using DomainShared.ViewModels.Actors;
using DomainShared.ViewModels.Categuries;
using DomainShared.ViewModels.Genres;
using DomainShared.ViewModels.Movies;
using MoviesProj.Views.Pages;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace MoviesProj.ViewModels.Pages
{
    public partial class MoviesListViewModel : ObservableObject, INavigationAware
    {
        private bool _isInit;
        private readonly INavigationService _navigationService;
        private readonly IContentDialogService _contentDialogService;
        private readonly ISnackbarService _snackbarService;
        private readonly IUnitOfWork _unitOfWork;

        public MoviesListViewModel(INavigationService navigationService, IUnitOfWork unitOfWork, IContentDialogService contentDialogService, ISnackbarService snackbarService)
        {
            _navigationService = navigationService;
            _unitOfWork = unitOfWork;
            _contentDialogService = contentDialogService;
            _snackbarService = snackbarService;
        }

        [ObservableProperty]
        private int _pageCount;

        [ObservableProperty]
        private int _currentPage = 1;

        [ObservableProperty]
        private int? _constructionYear;

        [ObservableProperty]
        private Guid? _genreId;

        [ObservableProperty]
        private string _name = default!;

        /// <summary>
        /// SuggestBox List
        /// </summary>
        [ObservableProperty]
        private List<GenresListViewModel> _genreList = [];

        /// <summary>
        /// DataGrid List
        /// </summary>
        [ObservableProperty]
        private IEnumerable<MoviListViewModel> _list = default!;

        public async void OnNavigatedTo()
        {
            await InitializeViewModel();
        }

        public void OnNavigatedFrom()
        {
        }

        [RelayCommand]
        private async Task InitializeViewModel()
        {
            _isInit = true;
            GenreList = await _unitOfWork.GenresRepository.GetGenresList();
            var t = await _unitOfWork.MoviesRepository.GetMoviesList(Name, GenreId, ConstructionYear, CurrentPage, 20);
            CurrentPage = t.CurrentPage;
            List = t.Items;
            PageCount = t.PageCount;
            _isInit = false;
        }

        [RelayCommand]
        private async Task OnChangePage()
        {
            if (_isInit)
            {
                return;
            }
            var t = await _unitOfWork.MoviesRepository.GetMoviesList(Name, GenreId, ConstructionYear, CurrentPage, 20);
            List = t.Items;
            PageCount = t.PageCount;
        }

        [RelayCommand]
        private async Task OnSubmit()
        {
            _isInit = true;
            var t = await _unitOfWork.MoviesRepository.GetMoviesList(Name, GenreId, ConstructionYear, CurrentPage, 20);
            List = t.Items;
            CurrentPage = t.CurrentPage;
            PageCount = t.PageCount;
            _isInit = false;
        }

        [RelayCommand]
        private async Task OnUpdate(Guid parameter)
        {
            // get movie=> include(Genres , categury , Actors)
            var movie = await _unitOfWork.MoviesRepository.GetMovieById(parameter);

            // Get ListView Items
            var categuryList = new ObservableCollection<CateguryListViewModel>(await _unitOfWork.CateguryRepository.GetCateguryList());
            var genreList = new ObservableCollection<GenresListViewModel>(await _unitOfWork.GenresRepository.GetGenresList());
            var actorList = new ObservableCollection<ActorsListViewModel>(await _unitOfWork.ActorsRepository.GetActorsList());

            foreach (var id in movie.Genres)
            {
                genreList.First(t => t.Id == id).Selected = true;
            }

            foreach (var id in movie.Catequries)
            {
                categuryList.First(t => t.Id == id).Selected = true;
            }

            foreach (var id in movie.Actors)
            {
                actorList.First(t => t.Id == id).Selected = true;
            }

            var context = new UpdateMoviePage(new UpdateMovieViewModel(_unitOfWork, _snackbarService, _navigationService)
            {
                ConstructionYear = movie.ConstructionYear,
                DirectorName = movie.DirectorName,
                MovieId = movie.Id,
                Name = movie.Name,
                Rate = movie.Rate,
                FilePath = movie.bannerPath,
                ActorList = actorList,
                CateguryList = categuryList,
                GenreList = genreList,
            });
            _navigationService.Navigate(typeof(UpdateMoviePage), context);
        }


        [RelayCommand]
        private async Task OnRemove(Guid parameter)
        {
            var result = await _contentDialogService.ShowSimpleDialogAsync(new SimpleContentDialogCreateOptions()
            {
                Title = "هشدار !!!",
                Content = new TextBlock() { Text = "آیا از حذف فیلم اطمینان دارید ؟", FlowDirection = FlowDirection.RightToLeft, FontFamily = new FontFamily("Calibri"), FontSize = 16 },
                PrimaryButtonText = "بله",
                SecondaryButtonText = "خیر",
                CloseButtonText = "انصراف",
            });

            if (result == ContentDialogResult.Primary)
            {
                var (e, s) = await _unitOfWork.MoviesRepository.DeleteMovie(parameter);
                if (!s)
                {
                    _snackbarService.Show("خطا", e, ControlAppearance.Secondary, new SymbolIcon(SymbolRegular.Warning20), TimeSpan.FromMilliseconds(3000));
                    return;
                }

                await _unitOfWork.SaveChangesAsync();
                _snackbarService.Show("کاربر گرامی", "حذف با موفقیت انجام شد.", ControlAppearance.Success, new SymbolIcon(SymbolRegular.CheckmarkCircle20), TimeSpan.FromMilliseconds(3000));
                var t = await _unitOfWork.MoviesRepository.GetMoviesList(Name, GenreId, ConstructionYear, CurrentPage, 20);
                List = t.Items;
                PageCount = t.PageCount;
            }
        }
    }
}
