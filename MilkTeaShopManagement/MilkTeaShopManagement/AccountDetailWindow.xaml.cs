using MilkTeaShop.BLL.Services;
using MilkTeaShop.DAL.Models;
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

namespace MilkTeaShopManagement
{
    /// <summary>
    /// Interaction logic for AccountDetai_Window.xaml
    /// </summary>
    public partial class AccountDetai_Window : Window
    {
        private AcountService _service = new();
        public AccountDetai_Window()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Account account = new();
            account.UserName = userNameTexbox.Text;
            account.DisplayName = DisplayNameTexbox.Text;
            account.PassWord = PasswordTexbox.Text;
            account.Type = int.Parse(TypeTexbox.Text);
            _service.AddAccount(account);
            this.Close();
        }
    }
}
