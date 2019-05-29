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
using CeqAcc.Entity;
using System.Data;

namespace CeqAcc
{
    /// <summary>
    /// Логика взаимодействия для accountant.xaml
    /// </summary>
    public partial class accountant : Window
    {


        public string login { get; set; }
        public string name { get; set; }

        public string role { get; set; }

        public string session { get; set; }

        public string countObject { get; set; }

        private TAXIEntities ceqacc;

        public int countSoftware { get; set; }

        public List<string> NameS;
        public List<string> Price;
        public List<string> Type;
        public List<string> CountO;
        

        public accountant(string login)
        {
            InitializeComponent();
            this.login = "Ваш логін: " + login;
            ceqacc = new TAXIEntities();

            var userInfo = ceqacc.Login.Where(x => x.name == login).First();
            this.name = "Повне ім'я: " + userInfo.password;

            this.role = "Роль: " + userInfo.role_id;
            
            

           

            
            var query = (from t3 in ceqacc.Admin
                      
                         from t2 in ceqacc.Admin
                         select new
                         {

                             NameS = t3.full_name,
                             Price = t3.full_name,
                           
                         }
                ).ToList();

            
            var query2 = (from t3 in ceqacc.Login
                          
                          from t2 in ceqacc.Driver
                          select new
                          {
                              NameS = t2.Login,
                              Price = t2.last_name

                          }
            ).ToList();



            


            myDataGrid.ItemsSource = query;
           
            myDataGrid.ItemsSource = query2;

            this.DataContext = this;

           


        }
        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }



    }
}

