using MovieCatalog.Model;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace MovieCatalog
{
    /// <summary>
    /// Логика взаимодействия для CatalogPage.xaml
    /// </summary>
    public partial class CatalogPage : Page
    {
        ObservableCollection<Film> filmColection = new ObservableCollection<Film>();
        Film film = new Film();
        NavigationService navService;
        string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\FilmBase.mdb";
        public CatalogPage()
        {
            InitializeComponent();
            FillList();
            GendeComboBoxFill();
            AgeLimitComboBoxFill();
            YearComboBoxFill();
        }
        private BitmapImage LoadImage(string filename)
        {
            BitmapImage source = new BitmapImage();
            source.BeginInit();
            source.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + filename);
            source.DecodePixelHeight = 175;
            source.DecodePixelWidth = 120;
            source.EndInit();

            return source;
        }

        public void FillList()
        {
            OleDbConnection eventConnection = new OleDbConnection(connectionString);

            OleDbCommand eventFillCommand = new OleDbCommand("Select * from Film", eventConnection);
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(eventFillCommand);

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "Film");

            eventConnection.Open();

            filmColection.Clear();

            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                filmColection.Add(new Film
                {
                    ID = Convert.ToInt32(dr[0]),
                    Title = dr[1].ToString(),
                    FilmGenre = dr[2].ToString(),
                    Age = Convert.ToInt32(dr[3]),
                    Release = Convert.ToDateTime(dr[4]),
                    Completion = Convert.ToDateTime(dr[5]),
                    Director = dr[6].ToString(),
                    Timing = Convert.ToInt32(dr[7]),
                    Description = dr[8].ToString(),
                    Actors = dr[9].ToString(),
                    Rating = Convert.ToInt32(dr[10]),
                    Picture = LoadImage(dr[11].ToString()),
                });
            }
            try
            {
                listView.ItemsSource = filmColection;
                cardView.ItemsSource = filmColection;
            }
            catch (Exception)
            {

            }
            finally
            {
                dataSet = null;
                dataAdapter.Dispose();
                eventConnection.Close();
                eventConnection.Dispose();
            }
        }

        private void Display_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Header.Equals("Карточки"))
            {
                cardView.Visibility = Visibility.Visible;
                listView.Visibility = Visibility.Hidden;
                Display.Header = "Список";
            }
            else
            {
                cardView.Visibility = Visibility.Hidden;
                listView.Visibility = Visibility.Visible;
                Display.Header = "Карточки";
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            OleDbConnection eventConnection = new OleDbConnection(connectionString);

            OleDbCommand eventFillCommand = new OleDbCommand("SELECT * FROM Film WHERE Title LIKE '%" + textBox.Text + "%'", eventConnection);
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(eventFillCommand);

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "Film");

            eventConnection.Open();

            filmColection.Clear();

            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                filmColection.Add(new Film
                {
                    ID = Convert.ToInt32(dr[0]),
                    Title = dr[1].ToString(),
                    FilmGenre = dr[2].ToString(),
                    Age = Convert.ToInt32(dr[3]),
                    Release = Convert.ToDateTime(dr[4]),
                    Completion = Convert.ToDateTime(dr[5]),
                    Director = dr[6].ToString(),
                    Timing = Convert.ToInt32(dr[7]),
                    Description = dr[8].ToString(),
                    Actors = dr[9].ToString(),
                    Rating = Convert.ToInt32(dr[10]),
                    Picture = LoadImage(dr[11].ToString()),
                });
            }
            try
            {

            }
            catch (Exception)
            {

            }
            finally
            {
                dataSet = null;
                dataAdapter.Dispose();
                eventConnection.Close();
                eventConnection.Dispose();
            }
        }

        private void GendeComboBoxFill()
        {
            string[] genre = new string[]{"Триллер", "Детектив", "Драма", "Криминал", "Фантастика", "Ужасы", "Фэнтези", "Боевик", "Комедия",
            "Приключения", "Мультфильм", "Мюзикл", "Биография", "Спорт"};
            comboBox.ItemsSource = genre;
        }



        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OleDbConnection eventConnection = new OleDbConnection(connectionString);

            OleDbCommand eventFillCommand = new OleDbCommand("SELECT * FROM Film WHERE FilmGenre LIKE '%" + comboBox.SelectedItem.ToString() + "%'", eventConnection);
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(eventFillCommand);

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "Film");

            eventConnection.Open();

            filmColection.Clear();

            filmColection.Clear();

            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                filmColection.Add(new Film
                {
                    ID = Convert.ToInt32(dr[0]),
                    Title = dr[1].ToString(),
                    FilmGenre = dr[2].ToString(),
                    Age = Convert.ToInt32(dr[3]),
                    Release = Convert.ToDateTime(dr[4]),
                    Completion = Convert.ToDateTime(dr[5]),
                    Director = dr[6].ToString(),
                    Timing = Convert.ToInt32(dr[7]),
                    Description = dr[8].ToString(),
                    Actors = dr[9].ToString(),
                    Rating = Convert.ToInt32(dr[10]),
                    Picture = LoadImage(dr[11].ToString()),
                });
            }
            try
            {

            }
            catch (Exception)
            {

            }
            finally
            {
                dataSet = null;
                dataAdapter.Dispose();
                eventConnection.Close();
                eventConnection.Dispose();
            }
        }

        private void AgeLimitComboBoxFill()
        {
            int[] ageLimit = new int[] { 6, 12, 16, 18 };
            comboBox1.ItemsSource = ageLimit;
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OleDbConnection eventConnection = new OleDbConnection(connectionString);

            OleDbCommand eventFillCommand = new OleDbCommand("SELECT * FROM Film WHERE Age = " + Convert.ToInt32(comboBox1.SelectedItem), eventConnection);
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(eventFillCommand);

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "Film");

            eventConnection.Open();

            filmColection.Clear();

            filmColection.Clear();

            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                filmColection.Add(new Film
                {
                    ID = Convert.ToInt32(dr[0]),
                    Title = dr[1].ToString(),
                    FilmGenre = dr[2].ToString(),
                    Age = Convert.ToInt32(dr[3]),
                    Release = Convert.ToDateTime(dr[4]),
                    Completion = Convert.ToDateTime(dr[5]),
                    Director = dr[6].ToString(),
                    Timing = Convert.ToInt32(dr[7]),
                    Description = dr[8].ToString(),
                    Actors = dr[9].ToString(),
                    Rating = Convert.ToInt32(dr[10]),
                    Picture = LoadImage(dr[11].ToString()),
                });
            }
            try
            {

            }
            catch (Exception)
            {

            }
            finally
            {
                dataSet = null;
                dataAdapter.Dispose();
                eventConnection.Close();
                eventConnection.Dispose();
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            FillList();
        }

        private void YearComboBoxFill()
        {
            int[] year = new int[] {1989, 1992, 1998, 1999, 2002, 2004, 2009, 2010,
            2014, 2019};
            comboBox2.ItemsSource = year;
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DateTime StartTime = new DateTime(Convert.ToInt32(comboBox2.SelectedItem), 1, 1);
            OleDbConnection eventConnection = new OleDbConnection(connectionString);

            OleDbCommand eventFillCommand = new OleDbCommand("SELECT * FROM Film WHERE YEAR(Release) = " + Convert.ToInt32(comboBox2.SelectedItem), eventConnection);
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(eventFillCommand);

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "Film");

            eventConnection.Open();

            filmColection.Clear();

            filmColection.Clear();

            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                filmColection.Add(new Film
                {
                    ID = Convert.ToInt32(dr[0]),
                    Title = dr[1].ToString(),
                    FilmGenre = dr[2].ToString(),
                    Age = Convert.ToInt32(dr[3]),
                    Release = Convert.ToDateTime(dr[4]),
                    Completion = Convert.ToDateTime(dr[5]),
                    Director = dr[6].ToString(),
                    Timing = Convert.ToInt32(dr[7]),
                    Description = dr[8].ToString(),
                    Actors = dr[9].ToString(),
                    Rating = Convert.ToInt32(dr[10]),
                    Picture = LoadImage(dr[11].ToString()),
                });
            }
            try
            {

            }
            catch (Exception)
            {

            }
            finally
            {
                dataSet = null;
                dataAdapter.Dispose();
                eventConnection.Close();
                eventConnection.Dispose();
            }
        }

        private void label2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (User.Login != null && User.Pass != null)
            {
                label2.Content = "Панель администратора";
                User.Login = null;
                User.Pass = null;
                adminBox.Visibility = Visibility.Hidden;
            }
            else
            {
                navService = NavigationService.GetNavigationService(this);
                navService.Navigate(new System.Uri(@"View\Entry.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        public void CatalogPage_Load(object sender, RoutedEventArgs e)
        {
            if (User.Login != null && User.Pass != null)
            {
                label2.Content = User.Login + " (Выход)";
                adminBox.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cardView.SelectedItem != null)
            {
                try
                {
                    Film chFilm = cardView.SelectedItem as Film;

                    OleDbConnection dbConnection = new OleDbConnection(connectionString);
                    OleDbCommand dbCommand = new OleDbCommand();
                    dbCommand.CommandType = CommandType.Text;

                    dbCommand.CommandText = "DELETE from Film where ID = @id and Title = @title";

                    dbCommand.Parameters.AddWithValue("@id", chFilm.ID);
                    dbCommand.Parameters.AddWithValue("@title", chFilm.Title);

                    dbCommand.Connection = dbConnection;
                    dbConnection.Open();
                    dbCommand.ExecuteNonQuery();
                    dbConnection.Close();

                    MessageBox.Show("Фильм успешно удален!");
                    FillList();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
                cardView.SelectedItem = null;
            }
            else if (listView.SelectedItem != null)
            {
                try
                {
                    Film chFilm = listView.SelectedItem as Film;

                    OleDbConnection dbConnection = new OleDbConnection(connectionString);
                    OleDbCommand dbCommand = new OleDbCommand();
                    dbCommand.CommandType = CommandType.Text;

                    dbCommand.CommandText = "DELETE from Film where ID = @id and Title = @title";

                    dbCommand.Parameters.AddWithValue("@id", chFilm.ID);
                    dbCommand.Parameters.AddWithValue("@title", chFilm.Title);

                    dbCommand.Connection = dbConnection;
                    dbConnection.Open();
                    dbCommand.ExecuteNonQuery();
                    dbConnection.Close();

                    MessageBox.Show("Фильм успешно удален!");
                    FillList();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
                listView.SelectedItem = null;
            }
        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                try
                {
                    Film chFilm = listView.SelectedItem as Film;
                    ChoosenFilm.chID = chFilm.ID;
                    ChoosenFilm.chTitle = chFilm.Title;
                    navService = NavigationService.GetNavigationService(this);
                    navService.Navigate(new System.Uri(@"View\FilmInfo.xaml", UriKind.RelativeOrAbsolute));
                    listView.SelectedItem = null;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void cardView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (cardView.SelectedItem != null)
            {
                try
                {
                    Film chFilm = cardView.SelectedItem as Film;
                    ChoosenFilm.chID = chFilm.ID;
                    ChoosenFilm.chTitle = chFilm.Title;
                    navService = NavigationService.GetNavigationService(this);
                    navService.Navigate(new System.Uri(@"View\FilmInfo.xaml", UriKind.RelativeOrAbsolute));
                    cardView.SelectedItem = null;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
        //изменить
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                try
                {
                    Film chFilm = listView.SelectedItem as Film;
                    ChoosenFilm.chID = chFilm.ID;
                    ChoosenFilm.chTitle = chFilm.Title;
                    navService = NavigationService.GetNavigationService(this);
                    navService.Navigate(new System.Uri(@"View\FilmInfoChange.xaml", UriKind.RelativeOrAbsolute));
                    listView.SelectedItem = null;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
            else if (cardView.SelectedItem != null)
            {
                try
                {
                    Film chFilm = cardView.SelectedItem as Film;
                    ChoosenFilm.chID = chFilm.ID;
                    ChoosenFilm.chTitle = chFilm.Title;
                    navService = NavigationService.GetNavigationService(this);
                    navService.Navigate(new System.Uri(@"View\FilmInfoChange.xaml", UriKind.RelativeOrAbsolute));
                    cardView.SelectedItem = null;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
        //добавить
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            navService = NavigationService.GetNavigationService(this);
            navService.Navigate(new System.Uri(@"View\FilmInfoadd.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
