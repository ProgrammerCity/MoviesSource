﻿using MoviesProj.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace MoviesProj.Views.Pages
{
    /// <summary>
    /// Interaction logic for MoviesPage.xaml
    /// </summary>
    public partial class CreateMoviesPage : INavigableView<CreateMoviesViewModel>
    {
        public CreateMoviesViewModel ViewModel { get; }
        public CreateMoviesPage(CreateMoviesViewModel viewModel )
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

        private void Btn_submit_Click(object sender, RoutedEventArgs e)
        {
            var s = lst_Gen.SelectedItems;
        }
    }
}
