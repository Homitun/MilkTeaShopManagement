using Microsoft.IdentityModel.Tokens;
using MilkTeaShop.BLL.Service;
using MilkTeaShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MilkTeaShopManagement
{
    /// <summary>
    /// Interaction logic for DrinkManagerWindow.xaml
    /// </summary>
    public partial class DrinkManagerWindow : Window
    {
        private DrinkService _service = new();
        private DrinkCategoryService _categoryService = new();
        private List<DrinkCategory> _drinkCategories;

        public DrinkManagerWindow()
        {
            InitializeComponent();
            _drinkCategories = _categoryService.GetAllDrinkCategory();
            CBCategory.ItemsSource = _drinkCategories;
            CBCategory.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            DrinkDataGrid.ItemsSource = null;
            DrinkDataGrid.ItemsSource = _service.GetAllDrink();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (TbPrice.Text.IsNullOrEmpty() || TbPrice.Text.Equals("-") )
            {
                MessageBox.Show("Price must be valid!.");
                return;
            }

            float price = 0f;
            float.TryParse(TbPrice.Text, out price);

            if (price < 0f)
            {
                MessageBox.Show("Price must be valid!.");
                return;
            }

            if (TbName2.Text.IsNullOrEmpty() || TbName2.Text.Equals("-"))
            {
                MessageBox.Show("Name field can not be null.");
                return;
            }

            Drink? existedDrink = _service.GetAllDrink().FirstOrDefault(c => c.Name == TbName2.Text && c.IdCategory == (int)CBCategory.SelectedValue);
            if (existedDrink != null)
            {
                MessageBox.Show("This drink already existed.");
                return;
            }

            Drink newDrink = new Drink { Name = TbName2.Text, IdCategory = (int)CBCategory.SelectedValue, Price = price};

            _service.AddDrink(newDrink);

            FillDataGrid();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (LbID2.Content.ToString().IsNullOrEmpty() || LbID2.Content.Equals("-"))
            {
                MessageBox.Show("You must select a drink from a table beside to update.");
                return;
            }

            if (TbName2.Text.IsNullOrEmpty() || TbName2.Text.Equals("-"))
            {
                MessageBox.Show("Name field can not be null.");
                return;
            }

            if (TbPrice.Text.IsNullOrEmpty() || TbPrice.Text.Equals("-"))
            {
                MessageBox.Show("Price must be valid!.");
                return;
            }

            float price = 0f;
            float.TryParse(TbPrice.Text, out price);

            if (price < 0f)
            {
                MessageBox.Show("Price must be valid!.");
                return;
            }

#pragma warning disable CS8604 // Possible null reference argument.
            Drink? drink = _service.GetAllDrink().FirstOrDefault(c => c.Id == int.Parse(LbID2.Content.ToString()));
#pragma warning restore CS8604 // Possible null reference argument.

            if (drink == null)
            {
                MessageBox.Show($"Drink with id {LbID2.Content} not found.");
                return;
            }

            Drink newDrink = new Drink { Name = TbName2.Text, IdCategory = (int)CBCategory.SelectedValue, Price = price };

            if (drink.Compare(newDrink))
            {
                MessageBox.Show($"The drink is existed.");
                return;
            }

            Drink? existedDrink = _service.GetAllDrink().FirstOrDefault(c => c.Name == TbName2.Text && c.IdCategory == (int)CBCategory.SelectedValue);

            if (existedDrink != null)
            {
                MessageBox.Show($"The drink is existed.");
                return;
            }

            drink.Name = TbName2.Text;
            drink.Price = price;
            drink.IdCategory = (int)CBCategory.SelectedValue;

            _service.UpdateDrink(drink);
            FillDataGrid();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DrinkDataGrid.SelectedItem is Drink selectedDrink)
            {
                try
                {
                    _service.DeleteDrink(selectedDrink.Id);
                } catch (Exception ex)
                {
                    MessageBox.Show("There are bill buyed this drink!");
                }
                LbID2.Content = "-";
                TbName2.Text = "-";
                TbPrice.Text = "-";
                FillDataGrid();
            }
            else
            {
                MessageBox.Show("Please select a drink to delete.");
            }
        }

        private void DrinkDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (DrinkDataGrid.SelectedItem is Drink selectedDrink)
            {
                int id = selectedDrink.DrinkCategory.Id;
                LbID2.Content = selectedDrink.Id;
                TbName2.Text = selectedDrink.Name;
                CBCategory.SelectedIndex = _drinkCategories.FindIndex(cate => (cate.Id == id));
                TbPrice.Text = selectedDrink.Price.ToString();
            }
        }

        private void TbName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TbName2.Text == "-")
                TbName2.Text = "";
        }

        private void CategoryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TbPrice_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TbPrice_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TbPrice.Text == "-")
                TbPrice.Text = "";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
