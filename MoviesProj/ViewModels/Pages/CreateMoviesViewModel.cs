﻿using Domain.IRepositories;
using DomainShared.ViewModels.Actors;
using DomainShared.ViewModels.Categuries;
using DomainShared.ViewModels.Genres;
using System.Collections.ObjectModel;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace MoviesProj.ViewModels.Pages
{
    public partial class CreateMoviesViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISnackbarService _snackbarService;
        public CreateMoviesViewModel(IUnitOfWork unitOfWork, INavigationService navigationService, ISnackbarService snackbarService)
        {
            _unitOfWork = unitOfWork;
            _snackbarService = snackbarService;
            _navigationService = navigationService;
        }

        [ObservableProperty]
        private int _constructionYear = 2000;

        [ObservableProperty]
        private float _rate = 0;

        [ObservableProperty]
        private List<Guid> _genresId = [];

        [ObservableProperty]
        private List<Guid> _catequriesId = [];

        [ObservableProperty]
        private List<Guid> _actorsId = [];

        [ObservableProperty]
        private string _name = default!;

        [ObservableProperty]
        private string _directorName = default!;

        [ObservableProperty]
        private ObservableCollection<GenresListViewModel> _genreList = [];

        [ObservableProperty]
        private ObservableCollection<CateguryListViewModel> _categuryList = [];

        [ObservableProperty]
        private ObservableCollection<ActorsListViewModel> _actorList = [];

        public void OnNavigatedTo()
        {
            InitializeViewModel();
        }

        public void OnNavigatedFrom() { }

        private async void InitializeViewModel()
        {
            CateguryList = new ObservableCollection<CateguryListViewModel>(await _unitOfWork.CateguryRepository.GetCateguryList());
            GenreList = new ObservableCollection<GenresListViewModel>(await _unitOfWork.GenresRepository.GetGenresList());
            ActorList = new ObservableCollection<ActorsListViewModel>(await _unitOfWork.ActorsRepository.GetActorsList());
        }

        [RelayCommand]
        private async Task OnSumbit(Type pageType)
        {
            var (error, isSuccess) = await _unitOfWork.MoviesRepository.Create(Name, Rate, CatequriesId, GenresId, ConstructionYear, DirectorName);
            if (!isSuccess)
            {
                _snackbarService.Show("کاربر گرامی", error, ControlAppearance.Secondary, new SymbolIcon(SymbolRegular.Warning20), TimeSpan.FromMilliseconds(3000));
                return;
            }
            _snackbarService.Show("کاربر گرامی", "عملیات با موفقیت انجام شد.", ControlAppearance.Success, new SymbolIcon(SymbolRegular.CheckmarkCircle20), TimeSpan.FromMilliseconds(3000));

            _navigationService.Navigate(pageType);
        }
    }
}
