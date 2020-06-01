using MovieCatalog.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MovieCatalog.View
{
    /// <summary>
    /// Логика взаимодействия для Entry.xaml
    /// </summary>
    public partial class Entry : Page
    {
        string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\FilmBase.mdb";
        NavigationService navService;

        public Entry()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            OleDbCommand dbCommand = new OleDbCommand();
            dbCommand.CommandType = CommandType.Text;

            dbCommand.CommandText = "Select * from [User] where Login = @login and Pass = @pass";

            dbCommand.Parameters.AddWithValue("@login", Login.Text);
            dbCommand.Parameters.AddWithValue("@pass", Password.Text);

            dbCommand.Connection = dbConnection;

            dbConnection.Open();
            OleDbDataReader dataAllReader = dbCommand.ExecuteReader();

            if (dataAllReader.Read())
            {
                User.ID  = Convert.ToInt32(dataAllReader[0]);
                User.Login = dataAllReader[1].ToString();
                User.Pass = dataAllReader[2].ToString();
                MessageBox.Show("Вы успешно вошли под пользователем " + User.Login);
                navService = NavigationService.GetNavigationService(this);
                navService.Navigate(new System.Uri(@"View\CatalogPage.xaml", UriKind.RelativeOrAbsolute));
            }
            else
            {
                MessageBox.Show("Вы ввели не верные данные!");
            }
            
            dbConnection.Close();
        }
    }
}
