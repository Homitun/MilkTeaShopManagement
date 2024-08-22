using Microsoft.EntityFrameworkCore.Query.Internal;
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
    /// Interaction logic for TableManagerWindow.xaml
    /// </summary>
    public partial class TableManagerWindow : Window
    {
        private TableService tableService = new();
        private List<Drink> drinks;
        private List<Bill> bills;
        private List<(int,string)> categories;
        private int billId = 0;
        public TableManagerWindow()
        {
            InitializeComponent();
            GetInitValue();
            LoadBill();
            LoadCategory();
            LoadDrink();
        }
        private void Refresh()
        {
            bills = tableService.GetAllBillActive();
            LoadBill();
            BillDetailName.Text = "";
            DateCheckIn.Content = "";
            BillItems.Children.Clear();
            TotalBill.Content = "";
        }

        private void GetInitValue()
        {
            if (drinks == null)
            {
                 drinks = tableService.GetAllDrinks();
                if(drinks != null)
                {
                  var tempcategories = drinks.GroupBy(x => x.IdCategory).Select(y => new
                    {
                        y.Key,
                        Name = y.FirstOrDefault().IdCategoryNavigation.Name
                    }).ToList();

                    categories = tempcategories.Select(x => (x.Key,x.Name)).ToList();
                }
            }
            if(bills == null)
            {
                bills = tableService.GetAllBillActive();
            }
        }

        private void LoadBill()
        {
            ListBillActive.Children.Clear();
            bills = tableService.GetAllBillActive();
            foreach (var bill in bills)
            {
                var Button = new Button
                {
                    Content = bill.IdTableNavigation.Name,
                    Width = 70,
                    Margin = new Thickness(4),
                };
                Button.Click += (s,e) => ViewOrderDetail_btn(bill.Id);
                ListBillActive.Children.Add(Button);
            }
        }
        private void LoadCategory()
        {
            foreach (var item in categories)
            {
                var Button = new Button
                {
                    Content = item.Item2,
                    Width = 120,
                    Height = 30,
                    Margin = new Thickness(4),
                };
                Button.Click += (s, e) => category_Click(item.Item1);
                Categories.Children.Add(Button);
            }
        }
        private void LoadDrink()
        {
            Drinks.Children.Clear();
            int index = 0;
            var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
            foreach (var drink in drinks)
            {
                index++;
                
                var drinkbtn = new Button
                {
                    Width = 150,
                    Height = 150,
                    Margin = new Thickness(13),
                    FontSize = 20,
                    FontWeight = FontWeights.Bold,
                };
                drinkbtn.Click += (s, e) => AddDrink(drink.Id);
                drinkbtn.Content = new StackPanel
                {
                    Children =
                     {
                         new TextBlock { Text = drink.Name,  },
                         new TextBlock { Text = drink.Price.ToString() + "$",  TextAlignment = TextAlignment.Center }

                     }
                };

                stackPanel.Children.Add(drinkbtn);
                if (index % 5 == 0 || index == drinks.Count )
                {
                    Drinks.Children.Add(stackPanel);
                    stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
                }
            }
        }
      
        private void category_Click(int cateid)
        {
            drinks = tableService.SearchWithCate(cateid);
            LoadDrink();
        }
        private void AddDrink(int idDrink)
        {
            if(billId != 0)
            {
            tableService.AddDrink(billId, idDrink,1);
            ViewOrderDetail_btn(billId);
            }
        }
        private void subtractDrink(int idDrink)
        {
            tableService.SubtractDrink(billId, idDrink, 1);
            ViewOrderDetail_btn(billId);
        }

        private void ViewOrderDetail_btn(int idOrder)
        {
            BillItems.Children.Clear();
            TotalBill.Content = "";
            billId = idOrder;
            var detailofbill = tableService.GetBillWithId(idOrder);
            BillDetailName.Text = detailofbill.TableName;
            DateCheckIn.Content = detailofbill.DateCheckIn.ToString();
            if (detailofbill.drinkInTables != null)
            {
                foreach (var item in detailofbill.drinkInTables)
                {
                    var stackpanel = new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        Background = new SolidColorBrush(Colors.LightGray),
                        Margin = new Thickness(0, 0, 0, 5)
                    };

                    var addBtn = new Button
                    {
                        Width = 40,
                        Content = "+",
                        FontSize = 18,
                        FontWeight = FontWeights.Bold,
                    };

                    var subtractBtn = new Button
                    {
                        Width = 40,
                        Content = "-",
                        FontSize = 18,
                        FontWeight = FontWeights.Bold,
                    };

                    addBtn.Click += (s, e) => AddDrink(item.DrinkID);
                    subtractBtn.Click += (s, e) => subtractDrink(item.DrinkID);
                    stackpanel.Children.Add(new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                        Children =
                    {
                        new Label
                        {
                            Width = 325,
                            Content = item.DrinkName,
                            Margin = new Thickness(5,0,0,0)
                        },
                        addBtn
                    }
                    });
                    stackpanel.Children.Add(new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                        Children =
                    {
                        new Label
                        {
                            Width = 180,
                            Content = item.Quanity + " x",
                            Margin = new Thickness(5,0,0,0),
                            HorizontalContentAlignment = HorizontalAlignment.Left,
                        },
                        new Label
                        {
                            Width = 135,
                            Content = item.Quanity * item.Price,
                            Margin = new Thickness(0,0,10,0),
                            HorizontalAlignment = HorizontalAlignment.Right,
                        },
                        subtractBtn
                    }
                    });
                    BillItems.Children.Add(stackpanel);
                }
                TotalBill.Content = detailofbill.drinkInTables.Sum(x => x.Quanity * x.Price);
            }
        }


        private void PaymentBill_Click(object sender, RoutedEventArgs e)
        {
            if(billId != 0)
            {
                tableService.PaymentBill(billId);
                billId = 0;
                Refresh();
                MessageBox.Show("Pay Successfully!");
            }
        }

        private void DeletBill_Click(object sender, RoutedEventArgs e)
        {
            if (billId != 0)
            {
                tableService.PaymentBill(billId);
                billId = 0;
                Refresh();
                MessageBox.Show("Delete Successfully!");
            }
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            drinks = tableService.SearchDrink(KeySearch.Text);
            LoadDrink();
        }

        private void AddTable_Click(object sender, RoutedEventArgs e)
        {
            billId = tableService.AddTable();
            ViewOrderDetail_btn(billId);
        }

        private void BillDetailName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (BillDetailName.Text.Length > 0) 
            { 
                tableService.UpdateTableName(billId, BillDetailName.Text);
                LoadBill();
            }
        }
    }


}
