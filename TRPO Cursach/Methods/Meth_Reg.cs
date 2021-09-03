using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace TRPO_Cursach
{
    public class Meth_Reg
    {
        public bool Word_Check(string text)
        {
            Regex regex = new Regex("[^А-ЯЁа-яё]+");
            bool check = regex.IsMatch(text);
            if (check == true)
            {
                return false;
            }
            else return true;
        }
        public bool Digit_Check(string text)
        {
            Regex regex = new Regex("[^0-9-+]+");
            bool check = regex.IsMatch(text);
            if (check == true)
            {
                return false;
            }
            else return true;
        }
        public bool Add(string last, string first, string middle, string address, string number, string login, string password, string password_confirm)
        {
            {
                gr691_shrnEntities db = new gr691_shrnEntities();
                TRPO_Client trpo_client = new TRPO_Client();
                TRPO_User trpo_user = new TRPO_User();
                try
                {
                    var login_check = db.TRPO_User.FirstOrDefault(ch => ch.login == login);
                    if (string.IsNullOrWhiteSpace(last) || string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(middle) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(number) || string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                    {
                        MessageBox.Show("Заполнены не все поля.", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    else if (login_check != null)
                    {
                        MessageBox.Show("Данный логин уже существует.", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    else if (password != password_confirm)
                    {
                        MessageBox.Show("Пароли не совпадают.", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    else
                    {
                        trpo_user.login = login;
                        trpo_user.password = password;
                        trpo_user.id_role = 3;
                        db.TRPO_User.Add(trpo_user);

                        trpo_client.id_user = trpo_user.id_user;
                        trpo_client.last_name = last;
                        trpo_client.first_name = first;
                        trpo_client.middle_name = middle;
                        trpo_client.address = address;
                        trpo_client.contact_number = number;
                        db.TRPO_Client.Add(trpo_client);
                        db.SaveChanges();

                        MessageBox.Show("Регистрация прошла успешно!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch
                {
                    MessageBox.Show("Введите корректные значения.", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                return true;
            }
        }

    }
}
