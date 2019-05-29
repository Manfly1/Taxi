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
    public partial class viewUserInfo : Window
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


        public viewUserInfo(object obj, string operation, string admin)
        {
            InitializeComponent();
            this.obj = obj;
            this.operation = operation;
            this.admin = admin;


            ceqacc = new TAXIEntities();


            rolesBox.ItemsSource = (from r in ceqacc.Role select r.role_name).ToList();
          




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

            userPassword = "21232F297A57A5A743894A0E4A801FC3";
            userName = userNameText.Text.Trim();
            userFullName = userFullNameText.Text.Trim();
          
            string tempRole = rolesBox.SelectedValue.ToString();

            try{
                role = (from r in ceqacc.Role where r.role_name == tempRole select r.role_id).First().ToString();
              
            }
            catch
            {
                MessageBox.Show("Сталась системна помилка");
                return;
            }


            if ((from u in ceqacc.Login where u.name == userName select u.uid).Count() > 0)
            {
                MessageBox.Show("Користувач з таким ім'ям вже існує");
                return;
            }

            try
            {
                int newuID = (from u in ceqacc.Login select u.uid).ToArray().Max() + 1;

                if(Convert.ToInt32(role)==0 || Convert.ToInt32(role) == 1)
                {
                    int newID = (from u in ceqacc.Admin select u.admin_id).ToArray().Max() + 1;
                    Entity.Admin ad = new Entity.Admin
                    {
                        admin_id = newID,
                        full_name = userFullName
                    };

                 
                    ceqacc.Admin.Add(ad);
              

                    Entity.Login lgg = new Entity.Login
                    {
                        uid = newuID,
                        id = newID,
                        password = userPassword,
                        role_id = Convert.ToInt16(role),
                        name = userName
                    };

                    ceqacc.Login.Add(lgg);
                    ceqacc.SaveChanges();

                    MessageBox.Show("Користувача успішно додано!");
                    this.Hide();



                }

                //else if(Convert.ToInt32(role) == 2)
                //{
                // int newID = (from u in ceqacc.Driver select u.id_driver).ToArray().Max() + 1;
                //   userBirthDate = new DateTime(2001, 1, 1);

                //Entity.Driver ads = new Entity.Driver
                //{
                // id_driver = newID,
                //last_name = userFullName,
                //};
                //      ceqacc.Driver.Add(ads);

                //Entity.Login lgs = new Entity.Login
                // {
                //        uid = newuID,
                //        id = newID,
                //        password = userPassword,
                //    role_id = Convert.ToInt16(role),
                //    name = userName
                //};

                 
                //  ceqacc.Login.Add(lgs);
                //   ceqacc.SaveChanges();

                //   MessageBox.Show("Користувача успішно додано!");
                //   this.Hide();
                //}

               

            }
            catch
            {
                MessageBox.Show("Системна помилка при додаванні користувача");
            }

        }
    }
}
           
            

