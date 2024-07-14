using Domain.IRepositories;
using DomainShared.ViewModels.Movies;
using Wpf.Ui.Controls;

namespace MoviesProj.ViewModels.Pages
{
    public partial class DashboardViewModel(IUnitOfWork unitOfWork) : ObservableObject, INavigationAware
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [ObservableProperty]
        private IEnumerable<MoviListViewModel> _list;

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
            List = await _unitOfWork.MoviesRepository.GetDashboardMovies();
        }
    }
}
