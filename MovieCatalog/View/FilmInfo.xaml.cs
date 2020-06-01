using MovieCatalog.Model;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace MovieCatalog.View
{
    /// <summary>
    /// Логика взаимодействия для FilmInfo.xaml
    /// </summary>
    public partial class FilmInfo : Page
    {
        ObservableCollection<Film> filmColection = new ObservableCollection<Film>();
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
                Title.Content = dr[1].ToString();
                FilmGenre.Content = dr[2].ToString();
                Age.Content = Convert.ToInt32(dr[3]);
                Release.Content = Convert.ToDateTime(dr[4]);
                Completion.Content = Convert.ToDateTime(dr[5]);
                Director.Content = dr[6].ToString();
                Timing.Content = Convert.ToInt32(dr[7]);
                Description.Text = dr[8].ToString();
                Actors.Content = dr[9].ToString();
                Rating.Content = Convert.ToInt32(dr[10]);
                imagefilm.Source = LoadImage(dr[11].ToString());
            }

            dataSet = null;
            dataAdapter.Dispose();
            eventConnection.Close();
            eventConnection.Dispose();

        }
    }
}
