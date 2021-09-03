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

namespace TRPO_Cursach
{
    /// <summary>
    /// Логика взаимодействия для Client_Window.xaml
    /// </summary>
    public partial class Client_Window : Window
    {
        gr691_shrnEntities db;
        Meth_B meth_b = new Meth_B();
        Meth_O meth_o = new Meth_O();
        public Client_Window()
        {
            InitializeComponent();
            db = new gr691_shrnEntities();
        }
        private void Digit_Check(object sender, TextCompositionEventArgs e)
        {
            if (meth_b.Digit_Check(e.Text) == false)
            {
                e.Handled = true;
                return;
            }
        }
        private void Basket_Insert(object sender, RoutedEventArgs e)
        {
            TRPO_Product trpo_product = Table_Product.SelectedItem as TRPO_Product;
            if (Table_Product.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали товар в каталоге.", "Магазин", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            db.TRPO_Product.Where(i => i.id_product == trpo_product.id_product).FirstOrDefault();
            if (meth_b.Add(trpo_product != null ? trpo_product.id_product.ToString() : "0", C_Quentity.Text) == true)
            {
                gr691_shrnEntities db = new gr691_shrnEntities();
                Table_Product.ItemsSource = db.TRPO_Product.Where(i => i.quantity_warehouse > 0).ToList();
                Table_Basket.ItemsSource = db.TRPO_Basket.Where(i => i.id_user == Meth_Auth.global_id_user && i.id_status == 1).ToList();
                C_Quentity.Clear();
                if (db.TRPO_Basket.Where(i => i.id_user == Meth_Auth.global_id_user && i.id_status == 1).Count() == 0)
                {
                    Result.Text = "";
                    return;
                }
                Result.Text = db.TRPO_Basket.Where(i => i.id_user == Meth_Auth.global_id_user && i.id_status == 1).Sum(s => s.total).ToString() + " руб.";
                return;
            }
        }
        private void Basket_Delete(object sender, RoutedEventArgs e)
        {
            TRPO_Basket trpo_basket = Table_Basket.SelectedItem as TRPO_Basket;
            if (Table_Basket.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали товар в корзине.", "Магазин", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            db.TRPO_Basket.Where(i => i.id_basket == trpo_basket.id_basket).FirstOrDefault();
            if (meth_b.Delete(trpo_basket != null ? trpo_basket.id_basket.ToString() : "0") == true) 
            {
                gr691_shrnEntities db = new gr691_shrnEntities();
                Table_Basket.ItemsSource = db.TRPO_Basket.Where(i => i.id_user == Meth_Auth.global_id_user && i.id_status == 1).ToList();
                Table_Product.ItemsSource = db.TRPO_Product.Where(i => i.quantity_warehouse > 0).ToList();
                if (db.TRPO_Basket.Where(i => i.id_user == Meth_Auth.global_id_user && i.id_status == 1).Count() == 0)
                {
                    Result.Text = "";
                    return;
                }
                Result.Text = db.TRPO_Basket.Where(i => i.id_user == Meth_Auth.global_id_user && i.id_status == 1).Sum(s => s.total).ToString() + " руб.";
            }
        }
        private void Basket_Update(object sender, RoutedEventArgs e)
        {
            TRPO_Basket trpo_basket = Table_Basket.SelectedItem as TRPO_Basket;
            if (Table_Basket.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали товар в корзине.", "Магазин", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            db.TRPO_Basket.Where(i => i.id_basket == trpo_basket.id_basket).FirstOrDefault();
            if (meth_b.Update(trpo_basket != null ? trpo_basket.id_basket.ToString() : "0", C_Quentity.Text) == true)
            {
                gr691_shrnEntities db = new gr691_shrnEntities();
                Table_Basket.ItemsSource = db.TRPO_Basket.Where(i => i.id_user == Meth_Auth.global_id_user && i.id_status == 1).ToList();
                Table_Product.ItemsSource = db.TRPO_Product.Where(i => i.quantity_warehouse > 0).ToList();
                C_Quentity.Clear();
                if (db.TRPO_Basket.Where(i => i.id_user == Meth_Auth.global_id_user && i.id_status == 1).Count() == 0)
                {
                    Result.Text = "";
                    return;
                }
                Result.Text = db.TRPO_Basket.Where(i => i.id_user == Meth_Auth.global_id_user && i.id_status == 1).Sum(s => s.total).ToString() + " руб.";
            }
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
        private void Checkout(object sender, RoutedEventArgs e)
        {
            if (db.TRPO_Basket.Where(i => i.id_user == Meth_Auth.global_id_user && i.id_status == 1).Count() == 0)
            {
                MessageBox.Show("Корзина пуста", "Магазин", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else if (meth_o.Add() == true)
            {
                gr691_shrnEntities db = new gr691_shrnEntities();
                MessageBox.Show("Заказ оформлен", "Магазин", MessageBoxButton.OK, MessageBoxImage.Information);
                Table_Basket.ItemsSource = db.TRPO_Basket.Where(i => i.id_user == Meth_Auth.global_id_user && i.id_status == 1).ToList();
                Result.Text = "";
            }
        }
        private void History(object sender, RoutedEventArgs e)
        {
            new History_Order().ShowDialog();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new gr691_shrnEntities();
            Table_Product.ItemsSource = db.TRPO_Product.Where(i => i.quantity_warehouse >= 0).ToList();
            Table_Basket.ItemsSource = db.TRPO_Basket.Where(i => i.id_user == Meth_Auth.global_id_user && i.id_status == 1).ToList();
            Cb_С_Product_Type.ItemsSource = db.TRPO_Product_Type.ToList();
            if (db.TRPO_Basket.Where(i => i.id_user == Meth_Auth.global_id_user && i.id_status == 1).Count() == 0)
            {
                Result.Text = "";
                return;
            }
            Result.Text = db.TRPO_Basket.Where(i => i.id_user == Meth_Auth.global_id_user && i.id_status == 1).Sum(s => s.total).ToString() + " руб.";
        }
    }
}
