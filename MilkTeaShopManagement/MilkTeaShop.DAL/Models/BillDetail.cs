using MilkTeaShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaShop.DAL.Models
{
    public partial class BillDetail
    {
        public int BillId { get; set; }
        public int TableId { get; set; }
        public string TableName { get; set; }
        public string DrinkName { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public double TotalPrice { get; set; }
    }
}
