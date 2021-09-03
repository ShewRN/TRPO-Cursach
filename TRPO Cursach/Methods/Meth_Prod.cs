using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace TRPO_Cursach
{
    public class Meth_Prod
    {
        public bool Digit_Check(string text)
        {
            Regex regex = new Regex("[^0-9,.]+");
            bool check = regex.IsMatch(text);
            if (check == true)
            {
                return false;
            }
            else return true;
        }
        public bool Add(string name, TRPO_Product_Type prod_type, TRPO_Provider provider, string price, string quantity)
        {
            gr691_shrnEntities db = new gr691_shrnEntities();
            try
            {
                TRPO_Product trpo_product = new TRPO_Product();
                if (prod_type == null)
                {
                    MessageBox.Show("Вы не выбрали тип продукта", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (provider == null)
                {
                    MessageBox.Show("Вы не выбрали поставщика", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(price) || string.IsNullOrWhiteSpace(quantity))
                {
                    MessageBox.Show("Заполнены не все поля.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    decimal conv_price = Convert.ToDecimal(price);
                    int conv_quantity = Convert.ToInt32(quantity);
                    trpo_product.name = name;
                    trpo_product.id_product_type = prod_type.id_product_type;
                    trpo_product.id_provider = provider.id_provider;
                    trpo_product.price = conv_price;
                    trpo_product.quantity_warehouse = conv_quantity;
                    db.TRPO_Product.Add(trpo_product);
                    db.SaveChanges();
                    MessageBox.Show("Товар добавлен", "Администратор", MessageBoxButton.OK, MessageBoxImage.Information);
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
                var d_prod = db.TRPO_Product.Where(i => i.id_product == num).FirstOrDefault();
                if (d_prod == null)
                {
                    MessageBox.Show("Вы не выбрали строку.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    db.TRPO_Product.Remove(d_prod);
                    db.SaveChanges();
                    MessageBox.Show("Товар удалён", "Администратор", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Внесите корректные значения.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        public bool Update(string id, string name, TRPO_Product_Type prod_type, TRPO_Provider provider, string price, string quantity)
        {
            gr691_shrnEntities db = new gr691_shrnEntities();
            try
            {
                int num = Convert.ToInt32(id);
                var u_prod = db.TRPO_Product.Where(u => u.id_product == num).FirstOrDefault();
                if (prod_type == null)
                {
                    MessageBox.Show("Вы не выбрали тип товара", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (provider == null)
                {
                    MessageBox.Show("Вы не выбрали поставщика", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(price) || string.IsNullOrWhiteSpace(quantity))
                {
                    MessageBox.Show("Заполнены не все поля.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    int conv_price = Convert.ToInt32(price),
                        conv_quantity = Convert.ToInt32(quantity);
                    if (u_prod == null)
                    {
                        MessageBox.Show("Вы не выбрали строку.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    u_prod.name = name;
                    u_prod.id_product_type = prod_type.id_product_type;
                    u_prod.id_provider = provider.id_provider;
                    u_prod.price = conv_price;
                    u_prod.quantity_warehouse = conv_quantity;
                    db.SaveChanges();
                    MessageBox.Show("Товар изменён", "Администратор", MessageBoxButton.OK, MessageBoxImage.Information);
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
