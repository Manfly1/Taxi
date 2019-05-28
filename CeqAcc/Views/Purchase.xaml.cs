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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CeqAcc.Entity;

namespace CeqAcc.Views
{
    /// <summary>
    /// Логика взаимодействия для Purchase.xaml
    /// </summary>
    public partial class Purchase : UserControl
    {
        public TAXIEntities ceqacc;
        private string login { get; set; }

        private string ObjName { get; set; }

        public Purchase(string logins)
        {
            InitializeComponent();
            this.login = logins;
            ceqacc = new TAXIEntities();

            try
            {
                
                var rn = (from t3 in ceqacc.Login
                          from t2 in t3.password
                          select new
                          {
                            
                          }).ToList();

                myDataGrid.ItemsSource = rn;
            }
            catch
            {
                MessageBox.Show("Сталась помилка в роботі із базою, спробуйте ще раз. ");
                return;
            }

            this.DataContext = this;
        }
        private void Add(object sender, RoutedEventArgs e)
        {



        }

        private void Remove(object sender, RoutedEventArgs e)
        {

        }

        private void Reload(object sender, RoutedEventArgs e)
        {

        }
    }
}
