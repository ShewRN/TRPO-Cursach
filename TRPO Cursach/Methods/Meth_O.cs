using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace TRPO_Cursach
{
    public class Meth_O
    {
        public bool Add()
        {
            gr691_shrnEntities db = new gr691_shrnEntities();
            var client = db.TRPO_Client.Where(i => i.TRPO_User.id_user == Meth_Auth.global_id_user).FirstOrDefault();
            try
            {
                TRPO_Order trpo_order = new TRPO_Order();
                trpo_order.id_client = client.id_client;
                db.TRPO_Order.Add(trpo_order);
                db.SaveChanges();                
            }
            catch
            {
                MessageBox.Show("Внесите корректные значения", "Магазин", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            db.Database.ExecuteSqlCommand("update TRPO_Basket set id_order=" + db.TRPO_Order.Max(i => i.id_order) + "where id_user =" + Meth_Auth.global_id_user + "and id_status = 1");
            var order = db.TRPO_Order.Where(i => i.id_order == db.TRPO_Order.Max(b => b.id_order)).FirstOrDefault();
            order.price_order = db.TRPO_Basket.Where(i => i.id_user == Meth_Auth.global_id_user && i.id_status == 1 && i.id_order == db.TRPO_Order.Max(b => b.id_order)).Sum(s => s.total);
            db.Database.ExecuteSqlCommand("update TRPO_Basket set id_status = 2 where id_user =" + Meth_Auth.global_id_user + "and id_status = 1");
            db.SaveChanges();
            return true;
        }
    }
}
