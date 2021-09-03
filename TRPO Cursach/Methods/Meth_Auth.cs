using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace TRPO_Cursach
{
    public class Meth_Auth
    {
        public static int global_id_user;
        public bool Enter(string login, string password)
        {
            gr691_shrnEntities db = new gr691_shrnEntities();
            if (login == "" || password == "")
            {
                MessageBox.Show("Вы не заполнили все поля", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            var auth_check = db.TRPO_User.FirstOrDefault(ch => ch.login == login && ch.password == password);
            if (auth_check == null)
            {
                MessageBox.Show("Логин или пароль введены не верно", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            global_id_user = auth_check.id_user;
            return true;
        }
    }
}
