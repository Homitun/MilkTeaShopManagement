using MilkTeaShop.BLL.GetListBillBus;
using MilkTeaShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace MilkTeaShopManagement
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        private RevenueService _service = new();
        public ReportWindow()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            DateOnly? startDate = StartDatePicker.SelectedDate.HasValue
                ? DateOnly.FromDateTime(StartDatePicker.SelectedDate.Value)
                : (DateOnly?)null;

            DateOnly? endDate = EndDatePicker.SelectedDate.HasValue
                ? DateOnly.FromDateTime(EndDatePicker.SelectedDate.Value)
                : (DateOnly?)null;

            var tableRevenues = _service.GetTableRevenues(startDate, endDate);

            ResultsDataGrid.ItemsSource = tableRevenues;
        }

        private void ShowBills_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var tableRevenue = button?.DataContext as TableRevenue;

            if (tableRevenue != null)
            {
                var listBillWindow = new ListBill(tableRevenue.TableBills);
                listBillWindow.ShowDialog();
            }
        }

        private DataGridRow GetParentDataGridRow(FrameworkElement element)
        {
            while (element != null && !(element is DataGridRow))
            {
                element = VisualTreeHelper.GetParent(element) as FrameworkElement;
            }
            return element as DataGridRow;
        }

        private DataGrid CreateTableBillsDataGrid(List<TableBill> tableBills)
        {
            var dataGrid = new DataGrid
            {
                AutoGenerateColumns = false,
                ItemsSource = tableBills
            };

            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Bill ID", Binding = new Binding("BillId") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "From Date", Binding = new Binding("FromDate") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "To Date", Binding = new Binding("ToDate") });

            dataGrid.SelectionChanged += (s, e) =>
            {
                var selectedBill = dataGrid.SelectedItem as TableBill;
                if (selectedBill != null)
                {
                    var billDetails = _service.GetListBillDetail(selectedBill.BillId);
                    var billDetailsDataGrid = CreateBillDetailsDataGrid(billDetails);

                    var row = GetParentDataGridRow(dataGrid);
                    var expander = row?.FindVisualChild<Expander>();
                    if (expander != null)
                    {
                        expander.Content = billDetailsDataGrid;
                    }
                }
            };

            return dataGrid;
        }

        private DataGrid CreateBillDetailsDataGrid(List<BillDetail> billDetails)
        {
            var dataGrid = new DataGrid
            {
                AutoGenerateColumns = false,
                ItemsSource = billDetails
            };

            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Drink ID", Binding = new Binding("DrinkId") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Drink Name", Binding = new Binding("DrinkName") });
            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Price",
                Binding = new Binding("Price")
                {
                    StringFormat = "{0:C}"
                }
            });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Quantity", Binding = new Binding("Quantity") });
            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Total Price",
                Binding = new Binding("TotalAmount")
                {
                    StringFormat = "{0:C}"
                }
            });

            return dataGrid;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public static class VisualTreeHelperExtensions
    {
        public static T FindVisualChild<T>(this DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T foundChild)
                {
                    return foundChild;
                }
                var childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
            return null;
        }
    }
}
