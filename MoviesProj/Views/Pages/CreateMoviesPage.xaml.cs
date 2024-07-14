using DomainShared.ViewModels.Actors;
using DomainShared.ViewModels.Categuries;
using DomainShared.ViewModels.Genres;
using MoviesProj.ViewModels.Pages;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Forms;
using Wpf.Ui.Controls;

namespace MoviesProj.Views.Pages
{
    /// <summary>
    /// Interaction logic for MoviesPage.xaml
    /// </summary>
    public partial class CreateMoviesPage : INavigableView<CreateMoviesViewModel>
    {
        public CreateMoviesViewModel ViewModel { get; }
        public CreateMoviesPage(CreateMoviesViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

        private async void Btn_submit_Click(object sender, RoutedEventArgs e)
        {
            foreach (GenresListViewModel itm in lst_gen.SelectedItems)
            {
                ViewModel.GenresId.Add(itm.Id);
            };

            foreach (CateguryListViewModel itm in lst_cat.SelectedItems)
            {
                ViewModel.CatequriesId.Add(itm.Id);

            }

            foreach (ActorsListViewModel itm in lst_act.SelectedItems)
            {
                ViewModel.ActorsId.Add(itm.Id);

            }
            await ViewModel.SumbitCommand.ExecuteAsync(null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new()
            {
                Title = "Select image",
                InitialDirectory = "",
                Filter = "Image Files (*.gif,*.jpg,*.jpeg,*.bmp,*.png)|*.gif;*.jpg;*.jpeg;*.bmp;*.png"
            };

            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
                ViewModel.FilePath = @"/Images/" + newName;
            }
        }
    }
}
