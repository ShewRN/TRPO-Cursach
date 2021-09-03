using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace TRPO_Cursach
{
    public class Meth_Prov
    {
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
        public bool Add(string name, string address, string number)
        {
            gr691_shrnEntities db = new gr691_shrnEntities();
            try
            {
                TRPO_Provider trpo_provider = new TRPO_Provider();
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(number))
                {
                    MessageBox.Show("Вы не заполнили все поля.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    trpo_provider.name = name;
                    trpo_provider.legal_address = address;
                    trpo_provider.contact_number = number;
                    db.TRPO_Provider.Add(trpo_provider);
                    db.SaveChanges();
                    MessageBox.Show("Поставщик добавлен.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Внесите корректные значения", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
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
                var d_prov = db.TRPO_Provider.Where(i => i.id_provider == num).FirstOrDefault();
                if (d_prov == null)
                {
                    MessageBox.Show("Вы не выбрали строку.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    db.TRPO_Provider.Remove(d_prov);
                    db.SaveChanges();
                    MessageBox.Show("Поставщик удалён.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Данного поставщика невозможно удалить, так как он внесён в таблицу <Каталог>.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        public bool Update(string id, string name, string address, string number)
        {
            gr691_shrnEntities db = new gr691_shrnEntities();
            try
            {
                int num = Convert.ToInt32(id);
                var u_prov = db.TRPO_Provider.Where(u => u.id_provider == num).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(number))
                {
                    MessageBox.Show("Вы не заполнили все поля.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    if (u_prov == null)
                    {
                        MessageBox.Show("Вы не выбрали строку.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    u_prov.name = name;
                    u_prov.legal_address = address;
                    u_prov.contact_number = number;
                    db.SaveChanges();
                    MessageBox.Show("Поставщик изменён", "Администратор", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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
