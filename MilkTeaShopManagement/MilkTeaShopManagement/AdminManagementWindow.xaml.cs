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
    /// Interaction logic for AdminManagementWindow.xaml
    /// </summary>
    public partial class AdminManagementWindow : Window
    {
        public AdminManagementWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
           AccountManagerWindow accountManagerWindow = new AccountManagerWindow();
            accountManagerWindow.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CategoryManagerWindow categoryManagerWindow = new CategoryManagerWindow();
            categoryManagerWindow.Show();
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ReportWindow reportWindow = new ReportWindow();
            reportWindow.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TableDrinkManagement tableDrinkManagement = new TableDrinkManagement();
            tableDrinkManagement.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DrinkManagerWindow drinkManagement = new DrinkManagerWindow();
            drinkManagement.Show();
        }
    }
}
