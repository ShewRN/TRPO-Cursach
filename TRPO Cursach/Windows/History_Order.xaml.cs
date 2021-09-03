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
    /// Логика взаимодействия для History_Order.xaml
    /// </summary>
    public partial class History_Order : Window
    {
        gr691_shrnEntities db;
        public History_Order()
        {
            InitializeComponent();
            db = new gr691_shrnEntities();
        }
        private void Cb_History_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Cb_History.SelectedItem == null)
            {
                if (db.TRPO_Basket.Where(w => w.id_status == 4).Count() == 0)
                {
                    Cb_History.SelectedIndex = -1;
                    Cb_History.ItemsSource = null;
                    Table_History.ItemsSource = null;
                    return;
                }
                var query = (from basket in db.TRPO_Basket
                             join order in db.TRPO_Order on basket.id_order equals order.id_order
                             where basket.id_status == 4
                             where basket.id_user == Meth_Auth.global_id_user
                             select new
                             {
                                 order = order.id_order
                             }).Distinct();
                Cb_History.ItemsSource = query.ToList();
                Cb_History.SelectedIndex = 0;
            }
            int.TryParse(string.Join("", Cb_History.SelectedItem.ToString().Where(c => char.IsDigit(c))), out int cb);
            var orde = db.TRPO_Order.Where(i => i.id_order == cb && i.id_client == i.TRPO_Client.id_client).FirstOrDefault();
            string dt = orde.date.ToString();
            DateTime date = Convert.ToDateTime(dt);
            var onlyDate = date.Date.ToShortDateString();
            Date.Text = onlyDate.ToString();
            Result.Text = db.TRPO_Basket.Where(i => i.id_user == Meth_Auth.global_id_user && i.id_status == 4).Sum(s => s.total).ToString() + " руб.";
            Table_History.ItemsSource = db.TRPO_Basket.Where(w => w.id_order == cb && w.id_status == 4).ToList();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new gr691_shrnEntities();
            if (db.TRPO_Basket.Where(w => w.id_status == 4).Count() == 0)
            {
                Cb_History.SelectedIndex = -1;
                return;
            }
            var query = (from basket in db.TRPO_Basket
                         join order in db.TRPO_Order on basket.id_order equals order.id_order
                         where basket.id_status == 4
                         where basket.id_user == Meth_Auth.global_id_user
                         select new
                         {
                             order = order.id_order
                         }).Distinct();
            Cb_History.ItemsSource = query.ToList();
            Cb_History.SelectedIndex = 0;
        }
    }
}
