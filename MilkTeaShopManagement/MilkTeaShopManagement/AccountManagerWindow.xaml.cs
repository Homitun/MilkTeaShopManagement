using MilkTeaShop.BLL.Services;
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
    }
}
