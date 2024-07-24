using Domain.IRepositories;
using DomainShared.ViewModels.Movies;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace MoviesProj.ViewModels.Pages
{
    public partial class DashboardViewModel(IUnitOfWork unitOfWork) : ObservableObject, INavigationAware
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [ObservableProperty]
        private ObservableCollection<MoviListViewModel> _list;

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
            List = new ObservableCollection<MoviListViewModel>(await _unitOfWork.MoviesRepository.GetDashboardMovies());
        }
    }
}
