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
    /// Interaction logic for AccountManagerWindow.xaml
    /// </summary>
    public partial class AccountManagerWindow : Window
    {
        private AcountService _service = new();
        public AccountManagerWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        private void FillDataGrid()
        {
            AccountGrid.ItemsSource = null;
            AccountGrid.ItemsSource = _service.GetAllAccount();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AccountDetai_Window detail = new AccountDetai_Window();
            detail.ShowDialog();
            FillDataGrid();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Account? selected = AccountGrid.SelectedItem as Account;
            if (selected == null)
            {
                MessageBox.Show("Please seclect 1 account before editing","Select an Account", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            AccountDetai_Window accountDetai_Window = new AccountDetai_Window();
            accountDetai_Window.EditedAccount = selected;
            accountDetai_Window.ShowDialog();
            FillDataGrid();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Account? selected = AccountGrid.SelectedItem as Account;
            if (selected == null)
            {
                MessageBox.Show("Please seclect 1 account before deleting", "Select an Account", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            MessageBoxResult confirm = MessageBox.Show("Do you really wanna remove this account ?", "Confirm ?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm == MessageBoxResult.No)
                return;
            else
                _service.RemoveAccount(selected);
            FillDataGrid();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
