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
        public Account EditedAccount { get; set; } = null;  // flag 
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

            if(EditedAccount == null)
                _service.AddAccount(account);
            else
                _service.UpdateAccount(account);
                    this.Close();
        }
        private void FillElement(Account account)
        {
            if (account == null)
                return;
            userNameTexbox.Text = account.UserName;
            userNameTexbox.IsEnabled = false;
            DisplayNameTexbox.Text = account.DisplayName;
            PasswordTexbox.Text = account.PassWord;
            TypeTexbox.Text = account.Type.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillElement(EditedAccount);
            if(EditedAccount == null)
            {
                LabelDetailMode.Content = "Create a new member";
            }
            else
            {
                LabelDetailMode.Content = "Update member's infomation";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
