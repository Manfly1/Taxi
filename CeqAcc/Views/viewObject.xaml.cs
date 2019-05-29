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
using System.Windows.Shapes;
using CeqAcc.Entity;
using System.Data;


namespace CeqAcc.Views
{
    /// <summary>
    /// Логика взаимодействия для viewObject.xaml
    /// </summary>
    public partial class viewObject : Window
    {
        public TAXIEntities ceqacc;
        private object obj { get; set; }
        private string operation { get; set; }
        private string admin { get; set; }
        private int globalID { get; set; }

        public string objectName { get; set; }
        public string objectStatus { get; set; }
        public string objectType { get; set; }
        public string objectInventary { get; set; }
        public string objectSerialNumber { get; set; }
        public Nullable<decimal> objectPrice { get; set; }
        public string objectWarrantly { get; set; }
        public Nullable<System.DateTime> objectWarrantlyEndDate { get; set; }
        public string objectUser { get; set; }

        public System.DateTime objectAddTime { get; set; }
        public string objectNotes { get; set; }
        public Nullable<bool> objectDead { get; set; }

        public List<string> warrantyList;

        public string commandName { get; set; }

        public string softwareNameTable { get; set; }
        public string softwareManufacturerTable { get; set; }


        public viewObject(object obj, string operation, string admin)
        {
            InitializeComponent();
            this.obj = obj;
            this.operation = operation;
            this.admin = admin;
            ceqacc = new TAXIEntities();

            warrantyList = new List<string>();
            warrantyList.Add("Так, повна");
            warrantyList.Add("Так, неповна");
            warrantyList.Add("Ні");


           


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
            var objectInfo = ceqacc.Login.Where(x => x.uid == globalID).First();
            if (objectInfo == null)
            {
                MessageBox.Show("Помилка у вибірці!");
                this.Close();

            }



           





        }

        public void createCommand()
        {
            commandName = "Додати";
            Title = "Додати користувача";

          
            this.Height = 500;
            rowedit.Height = new GridLength(0.2, GridUnitType.Star);
        }



      

        private void CommandEditClick()
        {
            if (objectNameText.Text.Trim() == "")
            {
                MessageBox.Show("Заповніть ім'я об'єта");
                return;
            }

            if (objectInventaryText.Text.Trim() == "")
            {
                MessageBox.Show("Заповніть інвертарний номер користувача");
                return;
            }

            if (StatusBox.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Оберіть стан об'єкту");
                return;
            }

            if (TypeBox.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Оберіть тип об'єкту");
                return;
            }

          
        }
    }
}




           
