using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;

namespace TRPO_Cursach
{
    /// <summary>
    /// Логика взаимодействия для Admin_Window.xaml
    /// </summary>
    public partial class Admin_Window : Window
    {
        Meth_Prov meth_prov = new Meth_Prov();
        Meth_PT meth_pt = new Meth_PT();
        Meth_Prod meth_prod = new Meth_Prod();
        Meth_E meth_e = new Meth_E();
        gr691_shrnEntities db;
        public Admin_Window()
        {
            InitializeComponent();
            db = new gr691_shrnEntities();
        }
        //сортировка полей на ввод только кириллицы
        private void Word_Check(object sender, TextCompositionEventArgs e)
        {
            if (meth_e.Word_Check(e.Text) == false || meth_pt.Word_Check(e.Text) == false)
            {
                e.Handled = true;
            }
        }
        //сортировка полей на ввод только чисел
        private void Digit_Check(object sender, TextCompositionEventArgs e)
        {
            if (meth_prod.Digit_Check(e.Text) == false)
            {
                e.Handled = true;
                return;
            }
        }
        private void Digit_Check1(object sender, TextCompositionEventArgs e)
        {
            if (meth_e.Digit_Check(e.Text) == false)
            {
                e.Handled = true;

            }
        }
        private void Digit_Check2(object sender, TextCompositionEventArgs e)
        {
            if (meth_prov.Digit_Check(e.Text) == false)
            {
                e.Handled = true;
            }
        }


        //PRODUCT
        private void Product_Insert(object sender, RoutedEventArgs e)
        {
            if (meth_prod.Add(Product_name.Text, Cb_P_Product_Type.SelectedItem as TRPO_Product_Type, Cb_P_Provider.SelectedItem as TRPO_Provider, Product_price.Text, Product_quantity.Text) == true)
            {
                Product_name.Clear();
                Product_price.Clear();
                Product_quantity.Clear();
            }
            Table_Product.ItemsSource = db.TRPO_Product.ToList();
        }
        private void Product_Delete(object sender, RoutedEventArgs e)
        {
            TRPO_Product trpo_product = Table_Product.SelectedItem as TRPO_Product;
            if (Table_Product.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали строку.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            db.TRPO_Product.Where(i => i.id_product == trpo_product.id_product).FirstOrDefault();
            meth_prod.Delete(trpo_product != null ? trpo_product.id_product.ToString() : "0");
            Table_Product.ItemsSource = db.TRPO_Product.ToList();
        }
        private void Product_Update(object sender, RoutedEventArgs e)
        {
            TRPO_Product trpo_product = Table_Product.SelectedItem as TRPO_Product;
            if (Table_Product.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали строку.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            db.TRPO_Product.Where(i => i.id_product == trpo_product.id_product).FirstOrDefault();
            if (meth_prod.Update(trpo_product != null ? trpo_product.id_product.ToString() : "0", Product_name.Text, Cb_P_Product_Type.SelectedItem as TRPO_Product_Type, Cb_P_Provider.SelectedItem as TRPO_Provider, Product_price.Text, Product_quantity.Text) == true)
            {
                gr691_shrnEntities db = new gr691_shrnEntities();
                Product_name.Clear();
                Product_price.Clear();
                Product_quantity.Clear();
            }
            Table_Product.ItemsSource = db.TRPO_Product.ToList();
        }

        //EMPLOYEE
        private void Employee_Insert(object sender, RoutedEventArgs e)
        {
            if (meth_e.Add(Employee_last.Text, Employee_first.Text, Employee_middle.Text, Employee_number.Text, Cb_E_User.SelectedItem as TRPO_User) == true)
            {
                Employee_last.Clear();
                Employee_first.Clear();
                Employee_middle.Clear();
                Employee_number.Clear();
                Cb_E_User.SelectedIndex = -1;
                Table_Employee.ItemsSource = db.TRPO_Employee.ToList();
            }
        }
        private void Employee_Delete(object sender, RoutedEventArgs e)
        {
            TRPO_Employee trpo_employee = Table_Employee.SelectedItem as TRPO_Employee;
            if (Table_Employee.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали строку.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            db.TRPO_Employee.Where(i => i.id_employee == trpo_employee.id_employee).FirstOrDefault();
            meth_e.Delete(trpo_employee != null ? trpo_employee.id_employee.ToString() : "0");
            Table_Employee.ItemsSource = db.TRPO_Employee.ToList();
        }
        private void Employee_Update(object sender, RoutedEventArgs e)
        {
            TRPO_Employee trpo_employee = Table_Employee.SelectedItem as TRPO_Employee;
            if (Table_Employee.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали строку.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            db.TRPO_Employee.Where(i => i.id_employee == trpo_employee.id_employee).FirstOrDefault();
            if (meth_e.Update(trpo_employee != null ? trpo_employee.id_employee.ToString() : "0", Employee_last.Text, Employee_first.Text, Employee_middle.Text, Employee_number.Text, Cb_E_User.SelectedItem as TRPO_User) == true)
            {
                gr691_shrnEntities db = new gr691_shrnEntities();
                Employee_last.Clear();
                Employee_first.Clear();
                Employee_middle.Clear();
                Employee_number.Clear();
                Cb_E_User.SelectedIndex = -1;
                Table_Employee.ItemsSource = db.TRPO_Employee.ToList();
            }
        }

        //PRODUCT_TYPE
        private void Product_Type_Insert(object sender, RoutedEventArgs e)
        {
            if (meth_pt.Add(Product_Type_name.Text) == true)
            {
                Product_Type_name.Clear();
            }
            Table_Product_Type.ItemsSource = db.TRPO_Product_Type.ToList();
            Cb_P_Product_Type.ItemsSource = db.TRPO_Product_Type.ToList();
        }
        private void Product_Type_Delete(object sender, RoutedEventArgs e)
        {
            TRPO_Product_Type trpo_product_type = Table_Product_Type.SelectedItem as TRPO_Product_Type;
            if (Table_Product_Type.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали строку.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            db.TRPO_Product_Type.Where(i => i.id_product_type == trpo_product_type.id_product_type).FirstOrDefault();
            meth_pt.Delete(trpo_product_type != null ? trpo_product_type.id_product_type.ToString() : "0");
            Table_Product_Type.ItemsSource = db.TRPO_Product_Type.ToList();
            Cb_P_Product_Type.ItemsSource = db.TRPO_Product_Type.ToList();
        }
        private void Product_Type_Update(object sender, RoutedEventArgs e)
        {
            TRPO_Product_Type trpo_product_type = Table_Product_Type.SelectedItem as TRPO_Product_Type;
            if (Table_Product_Type.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали строку.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            db.TRPO_Product_Type.Where(i => i.id_product_type == trpo_product_type.id_product_type).FirstOrDefault();
            if (meth_pt.Update(trpo_product_type != null ? trpo_product_type.id_product_type.ToString() : "0", Product_Type_name.Text) == true)
            {
                gr691_shrnEntities db = new gr691_shrnEntities();
                Product_Type_name.Clear();
                Table_Product_Type.ItemsSource = db.TRPO_Product_Type.ToList();
                Cb_P_Product_Type.ItemsSource = db.TRPO_Product_Type.ToList();
            }
        }

        //PROVIDER
        private void Provider_Insert(object sender, RoutedEventArgs e)
        {
            if (meth_prov.Add(Provider_name.Text, Provider_address.Text, Provider_number.Text) == true)
            {
                Provider_name.Clear();
                Provider_address.Clear();
                Provider_number.Clear();
            }
            Table_Provider.ItemsSource = db.TRPO_Provider.ToList();
            Cb_P_Provider.ItemsSource = db.TRPO_Provider.ToList();
        }
        private void Provider_Delete(object sender, RoutedEventArgs e)
        {
            TRPO_Provider trpo_provider = Table_Provider.SelectedItem as TRPO_Provider;
            if (Table_Provider.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали строку.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            db.TRPO_Provider.Where(i => i.id_provider == trpo_provider.id_provider).FirstOrDefault();
            meth_prov.Delete(trpo_provider != null ? trpo_provider.id_provider.ToString() : "0");
            Table_Provider.ItemsSource = db.TRPO_Provider.ToList();
            Cb_P_Provider.ItemsSource = db.TRPO_Provider.ToList();
        }
        private void Provider_Update(object sender, RoutedEventArgs e)
        {
            TRPO_Provider trpo_provider = Table_Provider.SelectedItem as TRPO_Provider;
            if (Table_Provider.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали строку.", "Администратор", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            db.TRPO_Provider.Where(i => i.id_provider == trpo_provider.id_provider).FirstOrDefault();
            if (meth_prov.Update(trpo_provider != null ? trpo_provider.id_provider.ToString() : "0", Provider_name.Text, Provider_address.Text, Provider_number.Text) == true)
            {
                gr691_shrnEntities db = new gr691_shrnEntities();
                Provider_name.Clear();
                Provider_address.Clear();
                Provider_number.Clear();
                Table_Provider.ItemsSource = db.TRPO_Provider.ToList();
                Cb_P_Provider.ItemsSource = db.TRPO_Provider.ToList();
            }
        }

        //ORDER

        private void Cb_Order_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Cb_Order.SelectedItem == null)
            {
                if(db.TRPO_Basket.Where(w => w.id_status == 3).Count() == 0)
                {
                    Cb_Order.SelectedIndex = -1;
                    Cb_Order.ItemsSource = null;
                    Table_Order.ItemsSource = null;
                    FIO_Client.Text = "";
                    FIO_Employee.Text = "";
                    Number.Text = "";
                    Address.Text = "";
                    Date.Text = "";
                    return;
                }
                var query = (from basket in db.TRPO_Basket
                             join orde in db.TRPO_Order on basket.id_order equals orde.id_order
                             where basket.id_status == 3
                             select new
                             {
                                 order = orde.id_order
                             }).Distinct();
                Cb_Order.ItemsSource = query.ToList();
                Cb_Order.SelectedIndex = 0;
            }
            int.TryParse(string.Join("", Cb_Order.SelectedItem.ToString().Where(c => char.IsDigit(c))), out int cb);
            var order = db.TRPO_Order.Where(i => i.id_order == cb && i.id_client == i.TRPO_Client.id_client).FirstOrDefault();
            var client = db.TRPO_Client.Where(i => i.id_client == order.id_client).FirstOrDefault();
            var employee = db.TRPO_Employee.Where(i => i.id_employee == order.id_employee).FirstOrDefault();
            FIO_Client.Text = client.last_name + " " + client.first_name + " " + client.middle_name;
            Number.Text = client.contact_number;
            Address.Text = client.address;
            FIO_Employee.Text = employee.last_name + " " + employee.first_name + " " + employee.middle_name;
            string dt = order.date.ToString();
            DateTime date = Convert.ToDateTime(dt);
            var onlyDate = date.Date.ToShortDateString();
            Date.Text = onlyDate.ToString();
            Table_Order.ItemsSource = db.TRPO_Basket.Where(w => w.id_order == cb && w.id_status == 3).ToList();
        }
        private void Confirm(object sender, RoutedEventArgs e)
        {
            db = new gr691_shrnEntities();
            if (Table_Order.ItemsSource == null)
            {
                MessageBox.Show("Вы не выбрали заказ", "Сотрудник", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int cb = Convert.ToInt32(Cb_Order.Text.ToString());
            db.Database.ExecuteSqlCommand("update TRPO_Basket set id_status = 4 where id_order =" + cb + "");
            db.Database.ExecuteSqlCommand("update TRPO_Order set date = '" + DateTime.Today.ToShortDateString() + "' where id_order =" + cb + "");
            if (db.TRPO_Basket.Where(w => w.id_status == 3).Count() == 0) 
            {
                Cb_Order.SelectedIndex = -1;
                Cb_Order.ItemsSource = null;
                Table_Order.ItemsSource = null;
                FIO_Client.Text = "";
                FIO_Employee.Text = "";
                Number.Text = "";
                Address.Text = "";
                Date.Text = "";
                return;
            }
            var query = (from basket in db.TRPO_Basket
                         join orde in db.TRPO_Order on basket.id_order equals orde.id_order
                         where basket.id_status == 3
                         select new
                         {
                             order = orde.id_order
                         }).Distinct();
            Cb_Order.ItemsSource = query.ToList();
            Cb_Order.SelectedIndex = 0;
        }
        private void Update(object sender, RoutedEventArgs e)
        {
            db = new gr691_shrnEntities();
            if (db.TRPO_Basket.Where(w => w.id_status == 3).Count() == 0)
            {
                Cb_Order.SelectedIndex = -1;
                Cb_Order.ItemsSource = null;
                Table_Order.ItemsSource = null;
                return;
            }
            var query = (from basket in db.TRPO_Basket
                         join orde in db.TRPO_Order on basket.id_order equals orde.id_order
                         where basket.id_status == 3
                         select new
                         {
                             order = orde.id_order
                         }).Distinct();
            Cb_Order.ItemsSource = query.ToList();
            Cb_Order.SelectedIndex = 0;
        }
        private void Reset_Filter(object sender, RoutedEventArgs e)
        {
            Cb_С_Product_Type.SelectedIndex = -1;
        }
        private void Cb_С_Product_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = Cb_С_Product_Type.SelectedItem as TRPO_Product_Type;
            string text = C_Search.Text;
            if (Cb_С_Product_Type.SelectedIndex != -1)
            {
                if (C_Search.Text == "")
                {
                    Table_Product.ItemsSource = db.TRPO_Product.Where(w => w.TRPO_Product_Type.name == cb.name).ToList();
                    db = new gr691_shrnEntities();
                    return;
                }
                else
                {
                    Table_Product.ItemsSource = db.TRPO_Product.Where(w =>
                                             w.TRPO_Product_Type.name == cb.name && w.name.Contains(text)
                                          || w.TRPO_Product_Type.name == cb.name && w.price.ToString().Contains(text)).ToList();
                    return;
                }
            }
            if (Cb_С_Product_Type.SelectedIndex == -1)
            {
                if (C_Search.Text == "")
                {
                    Table_Product.ItemsSource = db.TRPO_Product.ToList();
                    db = new gr691_shrnEntities();
                    return;
                }
                else
                {
                    Table_Product.ItemsSource = db.TRPO_Product.Where(w => w.name.Contains(text) || w.price.ToString().Contains(text)).ToList();
                    return;
                }
            }
        }
        private void Search(object sender, TextChangedEventArgs e)
        {
            var cb = Cb_С_Product_Type.SelectedItem as TRPO_Product_Type;
            string text = C_Search.Text;
            if (Cb_С_Product_Type.SelectedIndex != -1)
            {
                if (C_Search.Text == "")
                {
                    Table_Product.ItemsSource = db.TRPO_Product.Where(w => w.TRPO_Product_Type.name == cb.name).ToList();
                    db = new gr691_shrnEntities();
                    return;
                }
                else
                {
                    Table_Product.ItemsSource = db.TRPO_Product.Where(w =>
                                             w.TRPO_Product_Type.name == cb.name && w.name.Contains(text)
                                          || w.TRPO_Product_Type.name == cb.name && w.price.ToString().Contains(text)).ToList();
                }
            }
            if (Cb_С_Product_Type.SelectedIndex == -1)
            {
                if (C_Search.Text == "")
                {
                    Table_Product.ItemsSource = db.TRPO_Product.ToList();
                    db = new gr691_shrnEntities();
                    return;
                }
                else
                {
                    Table_Product.ItemsSource = db.TRPO_Product.Where(w => w.name.Contains(text) || w.price.ToString().Contains(text)).ToList();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new gr691_shrnEntities();
            Table_Product_Type.ItemsSource = db.TRPO_Product_Type.ToList();
            Table_Provider.ItemsSource = db.TRPO_Provider.ToList();
            Table_Product.ItemsSource = db.TRPO_Product.ToList();
            Table_Employee.ItemsSource = db.TRPO_Employee.ToList();
            Cb_E_User.ItemsSource = db.TRPO_User.Where(i => i.id_role == 2).ToList();
            Cb_P_Product_Type.ItemsSource = db.TRPO_Product_Type.ToList();
            Cb_P_Provider.ItemsSource = db.TRPO_Provider.ToList();
            if (db.TRPO_Basket.Where(w => w.id_status == 3).Count() == 0)
            {
                Cb_Order.SelectedIndex = -1;
                return;
            }
            var query = (from basket in db.TRPO_Basket
                         join orde in db.TRPO_Order on basket.id_order equals orde.id_order
                         where basket.id_status == 3
                         select new
                         {
                             order = orde.id_order
                         }).Distinct();
            Cb_Order.ItemsSource = query.ToList();
            Cb_Order.SelectedIndex = 0;
        }
    }
}
