using Microsoft.IdentityModel.Tokens;
using MilkTeaShop.BLL.Service;
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
    /// Interaction logic for TableDrinkManagement.xaml
    /// </summary>
    public partial class TableDrinkManagement : Window
    {
        private TableDrinkService _service = new();
        public TableDrinkManagement()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            TableDrinkDataGrid.ItemsSource = null;
            TableDrinkDataGrid.ItemsSource = _service.GetAllTableDrink();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (TbName.Text.IsNullOrEmpty() || TbName.Text.Equals("-"))
            {
                MessageBox.Show("Name field can not be null.");
                return;
            }

            TableDrink? existedName = _service.GetAllTableDrink().FirstOrDefault(c => c.Name == TbName.Text);
            if (existedName != null)
            {
                MessageBox.Show("This drink already existed.");
                return;
            }
            TableDrink newTable = new TableDrink { Name = TbName.Text };
            _service.AddTableDrink(newTable);
            FillDataGrid();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (LbID.Content.ToString().IsNullOrEmpty() || LbID.Content.Equals("-"))
            {
                MessageBox.Show("You must select a table drink from a table beside to update.");
                return;
            }

            if (TbName.Text.IsNullOrEmpty() || TbName.Text.Equals("-"))
            {
                MessageBox.Show("Name field can not be null.");
                return;
            }

            TableDrink? table = _service.GetAllTableDrink().FirstOrDefault(c => c.Id == int.Parse(LbID.Content.ToString()));

            if (table == null)
            {
                MessageBox.Show($"Table with id {LbID.Content} not found.");
                return;
            }

            if (table.Name == TbName.Text)
            {
                MessageBox.Show($"Table's name is not changed.");
                return;
            }

            table.Name = TbName.Text;
            _service.UpdateTableDrink(table);
            FillDataGrid();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LbID.Content.ToString().IsNullOrEmpty() || LbID.Content.Equals("-"))
            {
                MessageBox.Show("You must select a table from a table beside to delete.");
                return;
            }

            TableDrink? tableDrink = null;
            try
            {
                tableDrink = _service.GetById(int.Parse(LbID.Content.ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (tableDrink == null)
            {
                MessageBox.Show($"Table with id {LbID} not found.");
                return;
            }

            try
            {
                _service.DeleteTableDrink(tableDrink.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There are some Bill got this table.");
                return;
            }
            LbID.Content = "-";
            TbName.Text = "-";
            FillDataGrid();
        }

        private void TableDrinkDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (TableDrinkDataGrid.SelectedItem is TableDrink SeletedTableDrink)
            {
                LbID.Content = SeletedTableDrink.Id;
                TbName.Text = SeletedTableDrink.Name;
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
