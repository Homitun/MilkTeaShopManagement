using Microsoft.IdentityModel.Tokens;
using MilkTeaShop.BLL.Service;
using MilkTeaShop.DAL.Models;
using System.Windows;
using System.Windows.Controls;

namespace MilkTeaShopManagement
{
    /// <summary>
    /// Interaction logic for CategoryManagerWindow.xaml
    /// </summary>
    public partial class CategoryManagerWindow : Window
    {
        private DrinkCategoryService _service = new();
        public CategoryManagerWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            CategoryDataGrid.ItemsSource = null;
            CategoryDataGrid.ItemsSource = _service.GetAllDrinkCategory();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (TbName.Text.IsNullOrEmpty() || TbName.Text.Equals("-"))
            {
                MessageBox.Show("Name field can not be null.");
                return;
            }

            DrinkCategory? existedName = _service.GetAllDrinkCategory().FirstOrDefault(c => c.Name == TbName.Text);
            if (existedName != null)
            {
                MessageBox.Show("This category already existed.");
                return;
            }
            DrinkCategory newCategory = new DrinkCategory { Name = TbName.Text };
            _service.AddDrinkCategory(newCategory);
            FillDataGrid();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (LbID.Content.ToString().IsNullOrEmpty() || LbID.Content.Equals("-"))
            {
                MessageBox.Show("You must select a category from a table beside to update.");
                return;
            }

            if (TbName.Text.IsNullOrEmpty() || TbName.Text.Equals("-"))
            {
                MessageBox.Show("Name field can not be null.");
                return;
            }

#pragma warning disable CS8604 // Possible null reference argument.
            DrinkCategory? category = _service.GetAllDrinkCategory().FirstOrDefault(c => c.Id == int.Parse(LbID.Content.ToString()));
#pragma warning restore CS8604 // Possible null reference argument.

            if (category == null)
            {
                MessageBox.Show($"Category with id {LbID.Content} not found.");
                return;
            }

            if (category.Name == TbName.Text)
            {
                MessageBox.Show($"Category's name is not changed.");
                return;
            }

            category.Name = TbName.Text;
            _service.UpdateDrinkCategory(category);
            FillDataGrid();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LbID.Content.ToString().IsNullOrEmpty() || LbID.Content.Equals("-"))
            {
                MessageBox.Show("You must select a category from a table beside to delete.");
                return;
            }

            DrinkCategory? drinkCategory = null;
            try
            {
                drinkCategory = _service.GetById(int.Parse(LbID.Content.ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (drinkCategory == null)
            {
                MessageBox.Show($"Category with id {LbID} not found.");
                return;
            }

            try
            {
                _service.DeleteDrinkCategory(drinkCategory.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There are some drinks got this category.");
                return;
            }
            LbID.Content = "-";
            TbName.Text = "-";
            FillDataGrid();
        }

        private void CategoryDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (CategoryDataGrid.SelectedItem is DrinkCategory selectedCategory)
            {
                LbID.Content = selectedCategory.Id;
                TbName.Text = selectedCategory.Name;
            }
        }

        private void TbName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TbName.Text == "-")
                TbName.Text = "";
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}