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
    /// Логика взаимодействия для Employee_Window.xaml
    /// </summary>
    public partial class Employee_Window : Window
    {
        gr691_shrnEntities db;
        public Employee_Window()
        {
            InitializeComponent();
            db = new gr691_shrnEntities();
        }
        private void Cb_Collecting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Cb_Collecting.SelectedItem == null)
            {
                if(db.TRPO_Basket.Where(w => w.id_status == 2).Count() == 0)
                {
                    Cb_Collecting.SelectedIndex = -1;
                    Cb_Collecting.ItemsSource = null;
                    Table_Collecting.ItemsSource = null;
                    return;
                }
                var query = (from basket in db.TRPO_Basket
                             join order in db.TRPO_Order on basket.id_order equals order.id_order
                             where basket.id_status == 2
                             select new
                             {
                                 order = order.id_order
                             }).Distinct();
                Cb_Collecting.ItemsSource = query.ToList();
                Cb_Collecting.SelectedIndex = 0;
            }
            int.TryParse(string.Join("", Cb_Collecting.SelectedItem.ToString().Where(c => char.IsDigit(c))), out int cb);
            Table_Collecting.ItemsSource = db.TRPO_Basket.Where(w => w.id_order == cb && w.id_status == 2).ToList();
        }
        private void Collect(object sender, RoutedEventArgs e)
        {
            db = new gr691_shrnEntities();
            if (Table_Collecting.ItemsSource == null)
            {
                MessageBox.Show("Вы не выбрали заказ", "Сотрудник", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int cb = Convert.ToInt32(Cb_Collecting.Text.ToString());
            db.Database.ExecuteSqlCommand("update TRPO_Basket set id_status = 3 where id_order =" + cb + "");
            var id = db.TRPO_Employee.Where(i => i.id_user == Meth_Auth.global_id_user).FirstOrDefault();
            db.Database.ExecuteSqlCommand("update TRPO_Order set id_employee =" + id.id_employee + ", date = '" + DateTime.Today.ToShortDateString() + "' where id_order =" + cb + "");
            var form = MessageBox.Show("Вы уверены, что заказ собран верно?", "Сотрудник", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (form == MessageBoxResult.Yes)
            {
                if (db.TRPO_Basket.Where(w => w.id_status == 2).Count() == 0)
                {
                    Cb_Collecting.ItemsSource = null;
                    Table_Collecting.ItemsSource = null;
                    return;
                }
                var query = (from basket in db.TRPO_Basket
                             join order in db.TRPO_Order on basket.id_order equals order.id_order
                             where basket.id_status == 2
                             select new
                             {
                                 order = order.id_order
                             }).Distinct();
                Cb_Collecting.ItemsSource = query.ToList();
            }
        }
        private void Update(object sender, RoutedEventArgs e)
        {
            db = new gr691_shrnEntities();
            if (db.TRPO_Basket.Where(w => w.id_status == 2).Count() == 0)
            {
                Cb_Collecting.SelectedIndex = -1;
                Cb_Collecting.ItemsSource = null;
                Table_Collecting.ItemsSource = null;
                return;
            }
            var query = (from basket in db.TRPO_Basket
                         join order in db.TRPO_Order on basket.id_order equals order.id_order
                         where basket.id_status == 2
                         select new
                         {
                             order = order.id_order
                         }).Distinct();
            Cb_Collecting.ItemsSource = query.ToList();
            Cb_Collecting.SelectedIndex = 0;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new gr691_shrnEntities();
            if (db.TRPO_Basket.Where(w => w.id_status == 2).Count() == 0)
            {
                Cb_Collecting.SelectedIndex = -1;
                return;
            }
            var query = (from basket in db.TRPO_Basket
                         join order in db.TRPO_Order on basket.id_order equals order.id_order
                         where basket.id_status == 2
                         select new
                         {
                             order = order.id_order
                         }).Distinct();
            Cb_Collecting.ItemsSource = query.ToList();
            Cb_Collecting.SelectedIndex = 0;
        }
    }
}
