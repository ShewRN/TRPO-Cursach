using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace TRPO_Cursach
{
    public class Meth_B
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
        public bool Add(string id, string quantity)
        {
            gr691_shrnEntities db = new gr691_shrnEntities();
            int num = Convert.ToInt32(id);
            var a_prod = db.TRPO_Product.Where(i => i.id_product == num).FirstOrDefault();
            try
            {
                TRPO_Basket trpo_basket = new TRPO_Basket();
                var union = db.TRPO_Basket.Where(i => i.id_product == num && i.id_status == 1 && i.id_user == Meth_Auth.global_id_user).FirstOrDefault();
                if (a_prod == null)
                {
                    MessageBox.Show("Вы не выбрали товар в каталоге", "Магазин", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(quantity))
                {
                    MessageBox.Show("Вы не написали количество товара.", "Магазин", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                int conv_quantity = Convert.ToInt32(quantity);
                int difference = Convert.ToInt32(a_prod.quantity_warehouse) - Convert.ToInt32(quantity);
                decimal total = a_prod.price * Convert.ToInt32(quantity);
                if (difference < 0)
                {
                    MessageBox.Show("На складе нет такого количества выбранного товара.", "Магазин", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (union != null)
                {
                    if (union.quantity + Convert.ToInt32(quantity) > a_prod.quantity_warehouse + union.quantity)
                    {
                        MessageBox.Show("На складе нет такого количества выбранного товара.", "Магазин", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    int difference_plus = union.quantity + Convert.ToInt32(quantity);
                    union.quantity = difference_plus;
                    union.total = union.total + total;
                    a_prod.quantity_warehouse = a_prod.quantity_warehouse - (Convert.ToInt32(quantity));
                    db.SaveChanges();
                    MessageBox.Show("Товар добавлен в корзину", "Магазин", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    trpo_basket.id_product = a_prod.id_product;
                    trpo_basket.quantity = conv_quantity;
                    trpo_basket.total = total;
                    trpo_basket.id_user = Meth_Auth.global_id_user;
                    trpo_basket.id_status = 1;
                    db.TRPO_Basket.Add(trpo_basket);
                    MessageBox.Show("Товар добавлен в корзину", "Магазин", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch 
            {
                MessageBox.Show("Внесите корректные значения", "Магазин", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            a_prod.quantity_warehouse = a_prod.quantity_warehouse - Convert.ToInt32(quantity);
            db.SaveChanges();
            return true;
        }
        public bool Delete(string id)
        {
            gr691_shrnEntities db = new gr691_shrnEntities();
            try
            {
                int num = Convert.ToInt32(id);
                var d_bask = db.TRPO_Basket.Where(i => i.id_basket == num).FirstOrDefault();
                var prod = db.TRPO_Product.Where(i => i.id_product == d_bask.id_product).FirstOrDefault();
                if (d_bask == null)
                {
                    MessageBox.Show("Вы не выбрали строку в корзине.", "Магазин", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    prod.quantity_warehouse = prod.quantity_warehouse + d_bask.quantity;
                    db.TRPO_Basket.Remove(d_bask);
                    db.SaveChanges();
                    MessageBox.Show("Товар удалён из корзины", "Магазин", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Внесите корректные значения.", "Магазин", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        public bool Update(string id, string quantity)
        {
            gr691_shrnEntities db = new gr691_shrnEntities();
            try
            {
                int num = Convert.ToInt32(id);
                var u_bask = db.TRPO_Basket.Where(i => i.id_basket == num).FirstOrDefault();
                var prod = db.TRPO_Product.Where(i => i.id_product == u_bask.id_product).FirstOrDefault();
                if (u_bask == null)
                {
                    MessageBox.Show("Вы не выбрали товар в корзине", "Магазин", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(quantity))
                {
                    MessageBox.Show("Вы не написали количество товара.", "Магазин", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                int conv_quantity = Convert.ToInt32(quantity);
                int difference = Convert.ToInt32(prod.quantity_warehouse) + (u_bask.quantity - conv_quantity);
                decimal total = prod.price * conv_quantity;
                if (difference < 0)
                {
                    MessageBox.Show("На складе нет такого количества выбранного товара.", "Магазин", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    u_bask.quantity = conv_quantity;
                    u_bask.total = total;
                    prod.quantity_warehouse = difference;
                    db.SaveChanges();
                    MessageBox.Show("Количество товара изменено", "Магазин", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Внесите корректные значения", "Магазин", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;

        }
    }
}
