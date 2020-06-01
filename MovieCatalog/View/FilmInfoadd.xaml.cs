using Microsoft.Win32;
using MovieCatalog.Model;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace MovieCatalog.View
{
    /// <summary>
    /// Логика взаимодействия для FilmInfo.xaml
    /// </summary>
    public partial class FilmInfoAdd : Page
    {
        ObservableCollection<Film> filmColection = new ObservableCollection<Film>();
        Film film = new Film();
        String fileName, file, newFile, path;
        NavigationService navService;
        string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\FilmBase.mdb";

        public FilmInfoAdd()
        {
            InitializeComponent();
            //FillInfo(ChoosenFilm.chID, ChoosenFilm.chTitle);
        }

        private BitmapImage LoadImage(string filename)
        {
            BitmapImage source = new BitmapImage();
            source.BeginInit();
            source.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + filename);
            source.EndInit();

            return source;
        }

        private void addPict_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Title2.Text != "")
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.CheckFileExists = true;
                openFileDialog.AddExtension = true;
                openFileDialog.Filter = "Image files (*.jpg; *.png; *.jpeg)|*.jpg; *png; *jpeg";

                file = Title2.Text + ".jpg";

                if (openFileDialog.ShowDialog() == true)
                {
                    fileName = openFileDialog.FileName;
                    Directory.CreateDirectory(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"/films/", Title2.Text));
                    newFile = System.AppDomain.CurrentDomain.BaseDirectory + @"/films/" + Title2.Text + @"/" + file;
                    try
                    {
                        File.Copy(fileName, newFile);
                        MessageBox.Show("Файл успешно загружен!");
                    }
                    catch (Exception)
                    {
                        File.Delete(newFile);
                        File.Copy(fileName, newFile);
                        MessageBox.Show("Файл успешно перезаписан!");
                    }
                }
                path = @"/films/" + Title2.Text + @"/" + file;
                imagefilm2.Source = LoadImage(path);
            }
            else
            {
                MessageBox.Show("Перед загрузкой обложки введите название фильма!");
            }
        }

        private void saveChange_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OleDbConnection dbConnection = new OleDbConnection(connectionString);

            if (Title2.Text != "")
            {
                try
                {
                    OleDbCommand taskCommand = new OleDbCommand();
                    taskCommand.CommandType = CommandType.Text;
                    taskCommand.CommandText = "INSERT INTO [Film] ([Title], [FilmGenre], [Age], [Release], [Completion], " +
                        "[Director], [Timing], [Description], [Actors], [Rating], [Picture]) " +
                        "VALUES(@title, @filmGenre, @age, @release, @completion, @director, @timing, @description, @actors, @rating, @picture)";
                    taskCommand.Parameters.AddWithValue("@title", Title2.Text);
                    taskCommand.Parameters.AddWithValue("@filmGenre", FilmGenre.Text);
                    taskCommand.Parameters.AddWithValue("@age", Convert.ToInt32(Age.Text));
                    taskCommand.Parameters.AddWithValue("@release", Release.SelectedDate);
                    taskCommand.Parameters.AddWithValue("@completion", Completion.SelectedDate);
                    taskCommand.Parameters.AddWithValue("@director", Director.Text);
                    taskCommand.Parameters.AddWithValue("@timing", Convert.ToInt32(Timing.Text));
                    taskCommand.Parameters.AddWithValue("@description", Description.Text);
                    taskCommand.Parameters.AddWithValue("@actors", Actors.Text);
                    taskCommand.Parameters.AddWithValue("@rating", Rating.Text);
                    taskCommand.Parameters.AddWithValue("@picture", path);
                    taskCommand.Connection = dbConnection;

                    dbConnection.Open();

                    taskCommand.ExecuteNonQuery();

                    MessageBox.Show("Вы добавили фильм!");

                    dbConnection.Close();

                    navService = NavigationService.GetNavigationService(this);
                    navService.Navigate(new System.Uri(@"View\CatalogPage.xaml", UriKind.RelativeOrAbsolute));

                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    MessageBox.Show("Заполните все поля верными данными!");
                }
            }
            else
            {
                MessageBox.Show("Введите название фильма для добавления!");
            }
        }       
    }
}