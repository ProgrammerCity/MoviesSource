﻿using MoviesProj.ViewModels.Pages;
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
        }
    }
}
