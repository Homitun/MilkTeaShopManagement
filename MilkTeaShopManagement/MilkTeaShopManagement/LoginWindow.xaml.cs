using Microsoft.Identity.Client.NativeInterop;
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
using MilkTeaShop.BLL.Services;
using MilkTeaShop.DAL.Models;
using Microsoft.IdentityModel.Tokens;
namespace MilkTeaShopManagement
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    { private MemberService _service = new();
        public LoginWindow()
        {
            InitializeComponent();
        }



        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {//login authenticate thanh công thì mới gọi màn hình Main
            MilkTeaShop.DAL.Models.Account account = _service.Authenticate(UserNameTextBox.Text,PasswordTextBox.Text);
            // account co the la null hoac khac null (login thanh cong)

            if (UserNameTextBox.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Invalid UserName","Wrong credentials", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (PasswordTextBox.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Invalid Password", "Wrong credentials", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (account == null)
            {
                MessageBox.Show("Invalid username or wrong password", "Wrong credentials", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }// phai thong bao dc sai email hay pass
            // account luc nay laf 1 record naof do thuoc role 1,2
            //
            //  => authorization
            if(account.Type == 1)// admin
            {
                AdminManagementWindow adminManagementWindow = new AdminManagementWindow();
                adminManagementWindow.Show();
            }
            if (account.Type == 0)// staff
            {
                TableManagerWindow tableManagerWindow = new TableManagerWindow();
                tableManagerWindow.Show();
            }
            
            this.Hide();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
