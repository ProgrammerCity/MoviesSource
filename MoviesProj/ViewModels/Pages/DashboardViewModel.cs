using Domain.IRepositories;
using DomainShared.ViewModels.Movies;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace MoviesProj.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject, INavigationAware
    {
        private bool _isInit;
        private readonly INavigationService _navigationService;
        private readonly IUnitOfWork _unitOfWork;

        public DashboardViewModel(INavigationService navigationService, IUnitOfWork unitOfWork)
        {
            _navigationService = navigationService;
            _unitOfWork = unitOfWork;
        }

        [ObservableProperty]
        private int _pageCount = 20;

        [ObservableProperty]
        private int _currentPage = 1;

        [ObservableProperty]
        private int? _constructionYear;

        [ObservableProperty]
        private float? _rate;

        [ObservableProperty]
        private Guid? _genreId;

        [ObservableProperty]
        private Guid? _catequryId;

        [ObservableProperty]
        private string _name = default!;

        [ObservableProperty]
        private string _directorName = default!;

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
            var t = await _unitOfWork.MoviesRepository.GetMoviesList(Name, Rate, CatequryId, GenreId, ConstructionYear, DirectorName, CurrentPage, PageCount);
            //CurrentPage = t.CurrentPage;
            //List = t.Items;
            //PageCount = t.PageCount;
            //_isInit = false;
        }

        [RelayCommand]
        private async Task OnChangePage()
        {
            if (_isInit)
            {
                return;
            }
            var t = await _unitOfWork.MoviesRepository.GetMoviesList(Name, Rate, CatequryId, GenreId, ConstructionYear, DirectorName, CurrentPage, PageCount);
            List = t.Items;
            PageCount = t.PageCount;
        }
    }
}
