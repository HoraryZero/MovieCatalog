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
    public partial class FilmInfoChange : Page
    {
        ObservableCollection<Film> filmColection = new ObservableCollection<Film>();
        Film film = new Film();
        String fileName, file, newFile, path;
        NavigationService navService;
        string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\FilmBase.mdb";

        public FilmInfoChange()
        {
            InitializeComponent();
            FillInfo(ChoosenFilm.chID, ChoosenFilm.chTitle);
            path = @"/films/" + Title1.Text + @"/" + Title1.Text + ".jpg";
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
            if (Title1.Text != "")
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.CheckFileExists = true;
                openFileDialog.AddExtension = true;
                openFileDialog.Filter = "Image files (*.jpg; *.png; *.jpeg)|*.jpg; *png; *jpeg";

                file = Title1.Text + ".jpg";

                if (openFileDialog.ShowDialog() == true)
                {
                    fileName = openFileDialog.FileName;
                    Directory.CreateDirectory(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"/films/", Title1.Text));
                    newFile = System.AppDomain.CurrentDomain.BaseDirectory + @"/films/" + Title1.Text + @"/" + file;
                    try
                    {
                        File.Copy(fileName, newFile);
                        MessageBox.Show("Файл успешно загружен!");
                    }
                    catch (Exception)
                    {
                        imagefilm1.Source = null;
                        File.Delete(newFile);
                        File.Copy(fileName, newFile);
                        MessageBox.Show("Файл успешно перезаписан!");
                    }
                }
                path = @"/films/" + Title1.Text + @"/" + file;
                imagefilm1.Source = LoadImage(path);
            }
            else
            {
                MessageBox.Show("Перед загрузкой обложки введите название фильма!");
            }
        }

        private void FillInfo(int chID, string chTitle)
        {
            OleDbConnection eventConnection = new OleDbConnection(connectionString);

            OleDbCommand eventFillCommand = new OleDbCommand("Select * from Film where ID = " + chID + " and Title = '" + chTitle + "'", eventConnection);

            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(eventFillCommand);

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "Film");

            eventConnection.Open();

            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                //ID = Convert.ToInt32(dr[0]),
                Title1.Text = dr[1].ToString();
                FilmGenre.Text = dr[2].ToString();
                Age.Text = dr[3].ToString();
                Release.SelectedDate = Convert.ToDateTime(dr[4]);
                Completion.SelectedDate = Convert.ToDateTime(dr[5]);
                Director.Text = dr[6].ToString();
                Timing.Text = dr[7].ToString();
                Description.Text = dr[8].ToString();
                Actors.Text = dr[9].ToString();
                Rating.Text = dr[10].ToString();
                imagefilm1.Source = LoadImage(dr[11].ToString());
            }

            dataSet = null;
            dataAdapter.Dispose();
            eventConnection.Close();
            eventConnection.Dispose();

        }
        private void saveChange_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OleDbConnection dbConnection = new OleDbConnection(connectionString);

            if (Title1.Text != "")
            {
                try
                {
                    OleDbCommand taskCommand = new OleDbCommand();
                    taskCommand.CommandType = CommandType.Text;
                    taskCommand.CommandText = "Update [Film] set [Title] = @title, [FilmGenre] = @filmGenre, [Age] = @age, [Release] = @release, [Completion] = @completion, " +
                        "[Director] = @director, [Timing] = @timing, [Description] = @description, [Actors] = @actors, [Rating] = @rating " +
                        "where ID = @oldID and Title = @oldTitle";
                    taskCommand.Parameters.AddWithValue("@title", Title1.Text);
                    taskCommand.Parameters.AddWithValue("@filmGenre", FilmGenre.Text);
                    taskCommand.Parameters.AddWithValue("@age", Convert.ToInt32(Age.Text));
                    taskCommand.Parameters.AddWithValue("@release", Release.SelectedDate);
                    taskCommand.Parameters.AddWithValue("@completion", Completion.SelectedDate);
                    taskCommand.Parameters.AddWithValue("@director", Director.Text);
                    taskCommand.Parameters.AddWithValue("@timing", Convert.ToInt32(Timing.Text));
                    taskCommand.Parameters.AddWithValue("@description", Description.Text);
                    taskCommand.Parameters.AddWithValue("@actors", Actors.Text);
                    taskCommand.Parameters.AddWithValue("@rating", Rating.Text);
                    //taskCommand.Parameters.AddWithValue("@picture", path);
                    taskCommand.Parameters.AddWithValue("@oldID", ChoosenFilm.chID);
                    taskCommand.Parameters.AddWithValue("@oldTitle", ChoosenFilm.chTitle);
                    taskCommand.Connection = dbConnection;

                    dbConnection.Open();

                    taskCommand.ExecuteNonQuery();

                    MessageBox.Show("Вы изменили информацию о фильме!");

                    dbConnection.Close();

                    navService = NavigationService.GetNavigationService(this);
                    navService.Navigate(new System.Uri(@"View\CatalogPage.xaml", UriKind.RelativeOrAbsolute));

                }
                catch (Exception exc)
                {
                    //MessageBox.Show(exc.Message);
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
    /*   ObservableCollection<Film> filmColection = new ObservableCollection<Film>();
Film film = new Film();
NavigationService navService;
string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\FilmBase.mdb";

public FilmInfo()
{
  InitializeComponent();
  FillInfo(ChoosenFilm.chID, ChoosenFilm.chTitle);
}

private BitmapImage LoadImage(string filename)
{
  BitmapImage source = new BitmapImage();
  source.BeginInit();
  source.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + filename);
  //source.DecodePixelHeight = 175;
  //source.DecodePixelWidth = 120;
  source.EndInit();

  return source;
}
*/