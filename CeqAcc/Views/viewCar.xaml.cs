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
    /// Логика взаимодействия для viewUserInfo.xaml
    /// </summary>
    public partial class viewCar : Window
    {
        public TAXIEntities ceqacc;
        private object obj { get; set; }
        private string operation { get; set; }
        private string admin { get; set; }

        private int globalID { get; set; }


        public string iconName { get; set; }
        public string userName { get; set; }
        public string role { get; set; }
        public string userFullName { get; set; }
        public string userPassword { get; set; }
        public DateTime userBirthDate { get; set; }
        public Nullable<bool> dead { get; set; }
        public DateTime addTime { get; set; }


        public string objectStatus { get; set; }
        public string visibleVar { get; set; }

        public string commandName { get; set; }


        public viewCar(object obj, string operation, string admin)
        {
            InitializeComponent();
            this.obj = obj;
            this.operation = operation;
            this.admin = admin;


            ceqacc = new TAXIEntities();


            rolesBox.ItemsSource = (from r in ceqacc.Car select r.technical_condition).ToList();





            if (operation == "edit")
            {
                editCommand();
            }
            else if (operation == "new")
            {
                createCommand();
            }


            this.DataContext = this;

        }

        public void editCommand()
        {
            commandName = "Зберегти зміни";
            string[] words = obj.ToString().Split(',');
            globalID = Convert.ToInt32(Regex.Match(words[0], @"\d+").Value);
            var userInfo = ceqacc.Login.Where(x => x.uid == globalID).First();
            if (userInfo == null)
            {
                MessageBox.Show("Помилка у вибірці!");
                this.Close();

            }
            var resultObject = (from o in ceqacc.Request
                                where o.code_request == userInfo.uid
                                select new
                                {
                                    o.date,
                                    o.id_street_pos,
                                    o.id_area_pos,
                                    o.status

                                }).ToList();

        }

        public void createCommand()
        {
            commandName = "Додати";
            Title = "Додати користувача";
            this.Height = 500;
            rowedit.Height = new GridLength(0.2, GridUnitType.Star);
            userBirthDate = new DateTime(2001, 1, 1);


        }



        private void Command_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "edit")
            {

            }
            else if (operation == "new")
            {
                CommandNewClick();
            }

        }

        private void CommandNewClick()
        {
            if (userNameText.Text.Trim() == "")
            {
                MessageBox.Show("Заповніть нікнейм користувача");
                return;
            }

            if (userFullNameText.Text.Trim() == "")
            {
                MessageBox.Show("Заповніть повне ім'я користувача");
                return;
            }

            if (rolesBox.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Оберіть роль користувача");
                return;
            }
            string[] tempFullName = userFullNameText.Text.Trim().Split(' ');
            if (tempFullName.Count() < 2)
            {

                MessageBox.Show("Повне ім'я користувача повинне складатись що найменше з двох слів");
                return;
            }
        }
    }
}



