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
using System.Data.SqlClient;
using CeqAcc.Entity;
using CeqAcc.Controller;

namespace CeqAcc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        TAXIEntities my_DataBase;
        private void Onload(object sender, EventArgs e)
        {
            //виконується при загрузці форми
            try
            {
                my_DataBase = new TAXIEntities();
                statusConnect.Text = "Статус: З'єднано з сервером";
                st.Background = Brushes.LightGreen;
            }
            catch (SqlException ex)
            {
                statusConnect.Text = "Статус: Нема з'єднання";
                MessageBox.Show(Convert.ToString(ex));
                st.Background = Brushes.Red;
            }
        }

        Hash Hash = new Hash();
        ControllerPanel controller = new ControllerPanel();
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            
            if (login.Text.Trim() == "" || password.Password.Trim()=="")
            {
                MessageBox.Show("Заповніть всі поля!");
                return;
            }

            login.Text = login.Text.Trim();


            try
            {
                var logdata = my_DataBase.Login.Where(l => l.name == login.Text).FirstOrDefault();


                string hash = Hash.CreateMD5Hash(password.Password.Trim());

                // 21232F297A57A5A743894A0E4A801FC3 into base
                
                if (logdata != null && hash == logdata.password && controller.Create_Window(login.Text)) //перевірка паролю
                {
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Ви ввели не коректні дані");
                }
            }
            catch
            {
                MessageBox.Show("Помилка в зв'язку із базою даних");
                return;
            }

        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
