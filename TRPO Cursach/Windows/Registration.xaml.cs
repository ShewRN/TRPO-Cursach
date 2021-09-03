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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        Meth_Reg meth_reg = new Meth_Reg();
        gr691_shrnEntities db;
        public Registration()
        {
            InitializeComponent();
            db = new gr691_shrnEntities();
        }
        //сортировка полей на ввод только кириллицы
        private void Word_Check(object sender, TextCompositionEventArgs e)
        {
            if (meth_reg.Word_Check(e.Text) == false)
            {
                e.Handled = true;
            }
        }
        //сортировка полей на ввод только чисел
        private void Digit_Check(object sender, TextCompositionEventArgs e)
        {
            if (meth_reg.Digit_Check(e.Text) == false)
            {
                e.Handled = true;
            }
        }
        private void Registr(object sender, RoutedEventArgs e)
        {
            Meth_Reg meth_reg = new Meth_Reg();
            if (meth_reg.Add(Registr_last.Text, Registr_first.Text, Registr_middle.Text, Registr_address.Text, Registr_number.Text, Registr_login.Text, Registr_password.Password, Registr_password_again.Password) == true)
            {
                Close();
            }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            var form = MessageBox.Show("Вы точно хотите вернуться в окно авторизации?", "Регистрация", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (form == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}
