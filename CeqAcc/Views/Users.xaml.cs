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
using CeqAcc.Views;
using System.Data.Entity.Core.Objects;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;

namespace CeqAcc.Views
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : UserControl
    {
        public static string login;
        public TAXIEntities ceqacc;
        

        public string userRole { get; set; }
        public string fullName { get; set; }
        public int countObjects { get; set; }
        public Users(string logins)
        {
            InitializeComponent();
            ceqacc = new TAXIEntities();
            login = logins;

            try {
                var query = (from user in ceqacc.Login
                             select new
                             {
                                 user.uid,
                                 user.name,
                                 fullName = user.Admin.full_name,
                                 user.role
                             }).ToList();


                myDataGrid.ItemsSource = query;
            }
            catch 
            {
                MessageBox.Show("Сталась помилка v роботі із базою, спробуйте ще раз. ");
                return;
            }
            this.DataContext = this;



        }

        private static Users _instance;

        public static Users GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Users(login);
            }
            return _instance;
        }

        private void myDataGrid_SelectionChanged(object sender,SelectedCellsChangedEventArgs e)
        {
          
           

        }

        private void Add(object sender, RoutedEventArgs e)
        {
            object obj = myDataGrid.SelectedItem;
            viewUserInfo u = new viewUserInfo(obj, "new", login);
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
                    int deleteUserID = Convert.ToInt32(Regex.Match(words[0], @"\d+").Value);


                    var query = (from u in ceqacc.Login
                                 where u.uid == deleteUserID
                                 select u).SingleOrDefault();

                    
                    ceqacc.Login.Remove(query);
                    ceqacc.SaveChanges();
                    
                    Reload(sender,e);
                }
                catch {
                    MessageBox.Show("Помилка у видаленні");
                }
             
            }
        }

        private void Reload(object sender, RoutedEventArgs e)
        {

            try
            {
                myDataGrid.ItemsSource = (from user in ceqacc.Login
                                          select new
                                          {
                                              user.uid,
                                              user.id,
                                              user.name,
                                              user.role
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

            viewUserInfo u = new viewUserInfo(obj, "edit", login);
            u.Show();
            u.Topmost = true;  
            u.Activate();
            
        }

        private void myDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
 
   
}

