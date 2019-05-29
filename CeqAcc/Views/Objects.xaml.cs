using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Objects.xaml
    /// </summary>
    public partial class Objects : UserControl
    {
        public TAXIEntities ceqacc;
        private string login { get; set; }
        private string objectType { get; set; }
        private string objectStatus { get; set; }
        private string objectUser { get; set; }
        private string Tarif { get; set; }
   
        public Objects(string logins)
        {
            InitializeComponent();
            this.login = logins;
            ceqacc = new TAXIEntities();

            try
            {
                myDataGrid.ItemsSource = (from obj in ceqacc.Request
                                          select new
                                          {
                                              obj.code_request,
                                              obj.status,
                                              Tarif=obj.Discount_card.type_discount,
                                              obj.date,
                                           
                                              
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
            object obj = myDataGrid.SelectedItem;
            viewObject u = new viewObject(obj, "new", login);
            u.Show();
            u.Topmost = true;
            u.Activate();


        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            if (myDataGrid.SelectedItem != null)
            {
                try
                {

                    string[] words = myDataGrid.SelectedValue.ToString().Split(',');
                    int deleteObjectID = Convert.ToInt32(Regex.Match(words[0], @"\d+").Value);


                    var query = (from u in ceqacc.Request
                                 where u.code_request == deleteObjectID
                                 select u).SingleOrDefault();


                    ceqacc.Request.Remove(query);
                    ceqacc.SaveChanges();

                    Reload(sender, e);
                }
                catch
                {
                    MessageBox.Show("Помилка у видаленні");
                }

            }
        }

        private void Reload(object sender, RoutedEventArgs e)
        {


            try
            {
                myDataGrid.ItemsSource = (from obj in ceqacc.Login
                                          select new
                                          {
                                              obj.uid,
                                              obj.id,

                                              obj.name

                                          }).ToList();
            }
            catch
            {
                MessageBox.Show("Сталась помилка в роботі із базою, спробуйте ще раз. ");
                return;
            }
        }
        private void Search(object sender, RoutedEventArgs e)
        {

            string myquery = searchquery.Text;

            try
            {
                myDataGrid.ItemsSource = (from obj in ceqacc.Login
                                          where obj.name.Contains(myquery) 
                                          select new
                                          {
                                              obj.uid,
                                              obj.id,

                                              obj.name

                                          }).ToList();
            }
            catch
            {
                MessageBox.Show("Сталась помилка в роботі із базою, спробуйте ще раз. ");
                return;
            }
        }

        private void myDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            object obj = myDataGrid.SelectedItem;
            viewObject u = new viewObject(obj, "edit", login);
            u.Show();
            u.Topmost = true;
            u.Activate();

        }
    }
}
