using MilkTeaShop.DAL.Models;
using MilkTeaShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaShop.BLL.GetListBillBus
{
    public class RevenueService
    {
        private ListBillRepository _Repo = new();
        public List< TableRevenue > GetTableRevenues(DateOnly? startDate = null, DateOnly? endDate = null) 
        {
            return _Repo.GetAllTableRevenue(startDate, endDate);
        }
        public List<TableBill> GetTableBills(DateOnly? startDate = null, DateOnly? endDate = null)
        {
            return _Repo.GetTableBills(startDate, endDate);
        }

        public List<BillDetail> GetListBillDetail(int billId, DateOnly? startDate = null, DateOnly? endDate = null)
        {
            return _Repo.GetListBillDetail(billId, startDate, endDate);
        }
    }
}
