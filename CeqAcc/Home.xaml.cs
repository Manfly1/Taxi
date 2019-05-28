using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using CeqAcc.Views;
using CeqAcc.ViewModels;

namespace CeqAcc
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private object reapet;
      
        private string login;
        public Home( string login)
        {
            InitializeComponent();
            this.login = login;
            DataContext = new HomePageModel(login);
            reapet = DataContext;
           
        }



        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CreateUsers(object sender, RoutedEventArgs e)
        {
            DataContext = new Users(login);
        }

        private void BackHome(object sender, RoutedEventArgs e)
        {
            DataContext = reapet;
        }

        private void Objects(object sender, RoutedEventArgs e)
        {
            DataContext = new Objects(login);
        }

        
        private void Purchase(object sender, RoutedEventArgs e)
        {
            DataContext = new Purchase(login);
        }

        private void Software(object sender, RoutedEventArgs e)
        {
            DataContext = new Software(login);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
