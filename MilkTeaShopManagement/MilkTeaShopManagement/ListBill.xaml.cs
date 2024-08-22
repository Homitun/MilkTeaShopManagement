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
    /// Interaction logic for ListBill.xaml
    /// </summary>
    public partial class ListBill : Window
    {
        public ListBill(List<TableBill> tableBill)
        {
            InitializeComponent();
            ResultsDataGrid.ItemsSource = tableBill;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var tableBill = button?.DataContext as TableBill;

            if (tableBill != null)
            {
                var listDetailsWindow = new ListBillDetails(tableBill.BillDetails);
                listDetailsWindow.ShowDialog();
            }
        }
    }
}
