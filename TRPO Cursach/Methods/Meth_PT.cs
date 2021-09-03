using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace TRPO_Cursach
{
    public class Meth_PT
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
        public bool Add(string name)
        {
            gr691_shrnEntities db = new gr691_shrnEntities();
            try
            {
                TRPO_Product_Type trpo_product_type = new TRPO_Product_Type();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Вы не заполнили поле.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    trpo_product_type.name = name;
                    db.TRPO_Product_Type.Add(trpo_product_type);
                    db.SaveChanges();
                    MessageBox.Show("Тип товара добавлен.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Information);
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
                var d_pt = db.TRPO_Product_Type.Where(i => i.id_product_type == num).FirstOrDefault();
                if (d_pt == null)
                {
                    MessageBox.Show("Вы не выбрали строку.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    db.TRPO_Product_Type.Remove(d_pt);
                    db.SaveChanges();
                    MessageBox.Show("Тип товара удалён.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Данный тип товара невозможно удалить, так как он внесён в таблицу <Каталог>.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        public bool Update(string id, string name)
        {
            gr691_shrnEntities db = new gr691_shrnEntities();
            try
            {
                int num = Convert.ToInt32(id);
                var u_pt = db.TRPO_Product_Type.Where(u => u.id_product_type == num).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Вы не заполнили поле.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    if (u_pt == null)
                    {
                        MessageBox.Show("Вы не выбрали строку.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    u_pt.name = name;
                    db.SaveChanges();
                    MessageBox.Show("Тип товара изменён", "Администратор", MessageBoxButton.OK, MessageBoxImage.Information);
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
