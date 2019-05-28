using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeqAcc.Entity;

namespace CeqAcc.ViewModels
{
    public class HomePageModel
    {
        public string login { get; set; }
        public string name { get; set; }
        public string fullname { get; set; }

        public string role { get; set; }

        public string session { get; set; }
    

    private TAXIEntities ceqacc;

        

        public HomePageModel(string login)
        {
            this.login = "Ваш логін: "+login;
            ceqacc = new TAXIEntities();

            var userInfo = ceqacc.Login.Where(x => x.name == login).First();

            if (userInfo.role == 0 || userInfo.role == 1)
            {
                var userInfoTable = ceqacc.Admin.Where(x => x.admin_id == userInfo.id).First();
                this.fullname = userInfoTable.full_name;

                if (userInfo.role == 0) this.role = "Роль: Адміністратор";
                else if (userInfo.role == 1) this.role = "Роль: Диспетчер";
            }
            else if(userInfo.role == 2) //водій
            {
                var userInfoTable = ceqacc.Driver.Where(x => x.id_driver == userInfo.id).First();
                this.fullname = userInfoTable.last_name;
                this.role = "Роль: Водій";
            }


            this.name = "Повне ім'я: "+ this.fullname;
            
           
           
           
        }
    }
}
