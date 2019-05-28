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
using System.Data;

namespace CeqAcc.Views
{
    /// <summary>
    /// Логика взаимодействия для Software.xaml
    /// </summary>
    public partial class Software : UserControl
    {
        public TAXIEntities ceqacc;
        private string login { get; set; }
        public string type { get; set; }
        public string objects { get; set; }
        public string sfManufacturer { get; set; }
        public Software(string logins)
        {
            InitializeComponent();
            this.login = logins;
            ceqacc = new TAXIEntities();

            try
            {
                myDataGrid.ItemsSource = (from obj in ceqacc.Car

                                          select new
                                          {
                                              obj.state_number,
                                              obj.cod_car,
                                           
                                              obj.color,
                                              obj.graduation_year,
                                              obj.technical_condition




                                          }).ToList();
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

        private void myDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
