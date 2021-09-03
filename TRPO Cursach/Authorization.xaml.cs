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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        gr691_shrnEntities db;
        public Authorization()
        {
            InitializeComponent();
            db = new gr691_shrnEntities();
        }
        private void Auth_Enter(object sender, RoutedEventArgs e)
        {
            var authorization = db.TRPO_User.FirstOrDefault(l => l.login == Auth_Login.Text && l.password == Auth_Password.Password);
            Meth_Auth meth_auth = new Meth_Auth();
            if (meth_auth.Enter(Auth_Login.Text, Auth_Password.Password) == true)
            {
                switch (authorization.id_role)
                {
                    case 1:
                        Hide();
                        new Admin_Window().ShowDialog();
                        Application.Current.Shutdown();
                        break;
                    case 2:
                        Hide();
                        new Employee_Window().ShowDialog();
                        Application.Current.Shutdown();
                        break;
                    case 3:
                        Hide();
                        new Client_Window().ShowDialog();
                        Application.Current.Shutdown();
                        break;
                }
            }
        }
        private void Registration(object sender, RoutedEventArgs e)
        {
            new Registration().ShowDialog();
        }
    }
}
