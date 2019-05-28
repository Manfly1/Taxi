using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeqAcc;
using CeqAcc.Entity;
using CeqAcc.ViewModels;
using CeqAcc.Views;


namespace CeqAcc.Controller
{
    public class ControllerPanel
    {
        private TAXIEntities ceqacc;

        public ControllerPanel()
        {
            ceqacc = new TAXIEntities();
        }

        public bool Create_Window(string Userlogin)
        {
            var role = ceqacc.Login.Where(x => x.name == Userlogin);
            switch (role.First().role)
            {

                case 0: //права адміністратора
                    Home h = new Home(Userlogin);
                    h.Show();

                    break;
                case 2:
                    accountant ac = new accountant(Userlogin);
                    ac.Show();
                    break;

                    
                case 3: //права спеціаліста

                    Home home = new Home(Userlogin);
                    home.Show();
                    break;

            }
            return true;
        }
    }
}
