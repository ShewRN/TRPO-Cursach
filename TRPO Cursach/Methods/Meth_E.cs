using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace TRPO_Cursach
{
    public class Meth_E
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
        public bool Add(string last, string first, string middle, string number, TRPO_User user)
        {
            gr691_shrnEntities db = new gr691_shrnEntities();
            TRPO_Employee trpo_employee = new TRPO_Employee();
            try
            {
                if (string.IsNullOrWhiteSpace(last) || string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(middle) || string.IsNullOrWhiteSpace(number))
                {
                    MessageBox.Show("Заполнены не все поля.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (user == null)
                {
                    MessageBox.Show("Вы не выбрали пользователя", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                trpo_employee.last_name = last;
                trpo_employee.first_name = first;
                trpo_employee.middle_name = middle;
                trpo_employee.contact_number = number;
                trpo_employee.id_user = user.id_user;
                db.TRPO_Employee.Add(trpo_employee);
                db.SaveChanges();
                MessageBox.Show("Сотрудник добавлен", "Журнал", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Введите корректные значения.", "Журнал", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        public bool Delete(string id)
        {
            gr691_shrnEntities db = new gr691_shrnEntities();
            try
            {
                int num = Convert.ToInt32(id);
                var d_e = db.TRPO_Employee.Where(i => i.id_employee == num).FirstOrDefault();
                if (d_e == null)
                {
                    MessageBox.Show("Вы не выбрали строку.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    db.TRPO_Employee.Remove(d_e);
                    db.SaveChanges();
                    MessageBox.Show("Сотрудник удалён", "Администратор", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Данного поставщика невозможно удалить, так как он внесён в таблицу <Каталог>.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        public bool Update(string id, string last, string first, string middle, string number, TRPO_User user)
        {
            gr691_shrnEntities db = new gr691_shrnEntities();
            try
            {
                int num = Convert.ToInt32(id);
                var u_e = db.TRPO_Employee.Where(u => u.id_employee == num).FirstOrDefault();
                if (u_e == null)
                {
                    MessageBox.Show("Вы не выбрали строку.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(last) || string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(middle) || string.IsNullOrWhiteSpace(number))
                {
                    MessageBox.Show("Заполнены не все поля.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (user == null)
                {
                    MessageBox.Show("Вы не выбрали пользователя", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                u_e.last_name = last;
                u_e.first_name = first;
                u_e.middle_name = middle;
                u_e.contact_number = number;
                u_e.id_user = user.id_user;
                db.SaveChanges();
                MessageBox.Show("Сотрудник изменён", "Администратор", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Введите корректные значения.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
