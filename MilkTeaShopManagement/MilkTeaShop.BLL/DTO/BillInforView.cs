using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaShop.DAL.DTO
{
    public class BillInforView
    {
        public int ID { get; set; }
        public string TableName { get; set; }
        public DateOnly DateCheckIn {  get; set; }
        public DateOnly DateCheckOut { get; set; }
        public List<DrinkInTable> drinkInTables { get; set; }
        public int BillStatus { get; set; }
    }
    public class DrinkInTable
    {
        public int DrinkID { get; set; }
        public string DrinkName { get; set; }
        public double Price { get; set; }
        public int Quanity { get; set; }
    }
}
