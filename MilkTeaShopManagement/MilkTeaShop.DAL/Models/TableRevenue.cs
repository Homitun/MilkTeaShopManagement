using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaShop.DAL.Models
{
    public partial class TableRevenue
    {
        public int TableId { get; set; }
        public string TableName { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public double Revenue { get; set; }
        public List<TableBill> TableBills { get; set; } = new();
    }
}
