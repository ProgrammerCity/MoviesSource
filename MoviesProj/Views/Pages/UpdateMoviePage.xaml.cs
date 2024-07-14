using DomainShared.ViewModels.Actors;
using DomainShared.ViewModels.Categuries;
using DomainShared.ViewModels.Genres;
using MoviesProj.ViewModels.Pages;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Controls;
using System.Reflection;
using System.Windows.Media;
using Wpf.Ui.Controls;
using System.Windows.Forms;

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

        private async void Btn_submit_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is UpdateMoviePage c)
            {
                foreach (GenresListViewModel itm in lst_gen.SelectedItems)
                {
                    c.ViewModel.GenresId.Add(itm.Id);
                };

                foreach (CateguryListViewModel itm in lst_cat.SelectedItems)
                {
                    c.ViewModel.CatequriesId.Add(itm.Id);

                }

                foreach (ActorsListViewModel itm in lst_act.SelectedItems)
                {
                    c.ViewModel.ActorsId.Add(itm.Id);

                }
                await c.ViewModel.SumbitCommand.ExecuteAsync(null);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new()
            {
                Title = "Select image",
                InitialDirectory = "",
                Filter = "Image Files (*.gif,*.jpg,*.jpeg,*.bmp,*.png)|*.gif;*.jpg;*.jpeg;*.bmp;*.png"
            };

            if (fd.ShowDialog() == DialogResult.OK)
            {
                string ext = Path.GetExtension(fd.FileName);
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var imagePath = path + @"\Images";
                if (!Directory.Exists(imagePath))
                {
                    _ = Directory.CreateDirectory(imagePath);
                }

                var newName = Guid.NewGuid().ToString() + ext;
                string newPathToFile = Path.Combine(imagePath, newName);
                File.Copy(fd.FileName, newPathToFile);
                if (DataContext is UpdateMoviePage c)
                {
                    c.ViewModel.FilePath = @"/Images/" + newName;
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is UpdateMoviePage c)
            {
                foreach (var item in c.ViewModel.GenreList)
                {
                    if (item.Selected)
                    {
                        lst_gen.SelectedItems.Add(item);
                    }
                }

                foreach (var item in c.ViewModel.CateguryList)
                {
                    if (item.Selected)
                    {
                        lst_cat.SelectedItems.Add(item);
                    }
                }

                foreach (var item in c.ViewModel.ActorList)
                {
                    if (item.Selected)
                    {
                        lst_act.SelectedItems.Add(item);
                    }
                }
            }
        }
    }
}
