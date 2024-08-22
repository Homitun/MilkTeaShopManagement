using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MilkTeaShop.DAL.DTO;
using MilkTeaShop.DAL.Models;
using MilkTeaShop.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaShop.BLL.Services
{
    public class TableService
    {
        private TableDrinkRepository _tableDrinkRepository = new();
        private BillRepository _billRepository = new();
        private BillInfoRepository _billInfoRepository = new();
        private DrinkRepository _drinkRepository = new();

        public List<Drink> GetAllDrinks() 
        {
            return _drinkRepository.GetAll();
        }

        public BillInforView GetBillWithId(int billId)
        {
            return _billInfoRepository.GetBillInfoWithId(billId);
        }
      

        public void AddDrink (int billId, int drinkId, int quantity)
        {
            var billInfo = _billInfoRepository.GetWithCondition(x => x.IdBill == billId);
            if (billInfo == null)
            {
                _billInfoRepository.Add(new BillInfo
                {
                    IdBill = billId,
                    Count = quantity,
                    IdDrink = drinkId,
                });
            }
            else 
            {
                _billInfoRepository.UpdateQuantity(billId, drinkId, quantity);
            }
        }

        public int AddTable()
        {
           
           var tabledrinkid = _tableDrinkRepository.Add(new TableDrink
            {
               Name = "Table " + _tableDrinkRepository.GetAll().Count,
               Status = "active"
           });

             return _billRepository.Add(new Bill
            {
                IdTable = tabledrinkid,
                DateCheckIn = DateOnly.FromDateTime(DateTime.Now),
                Status = 0
            });

        }
        public void SubtractDrink(int billId, int drinkId, int quantity)
        {
            _billInfoRepository.UpdateQuantity(billId, drinkId, -quantity);
        }
        public void RemoveDrink (int billId, int drinkId)
        {
           var billInfos = _billInfoRepository.GetWithCondition(x => x.IdBill == billId && x.IdDrink == drinkId);
            if(billInfos != null)
            {
                _billInfoRepository.Delete(billInfos.First());
            }
        }
        public void UpdateTableName(int billId, string tableName)
        {
            var bill = _billRepository.GetWithCondition(x => x.Id == billId);
            var table = _tableDrinkRepository.GetById(bill.First().IdTable);
            if(table != null)
            {
                table.Name = tableName;
            _tableDrinkRepository.Update(table);
            }
        }
        public List<Bill> GetAllBillActive()
        {
            return _billRepository.GetAllWithstatus0();
        }
        public void PaymentBill(int billId)
        {
            _billRepository.UpdateStatus(billId, 1);
            var bill = _billRepository.GetWithCondition(x => x.Id == billId);
            var table = _tableDrinkRepository.GetById(bill.First().IdTable);
            table.Status = "Finished";
            _tableDrinkRepository.Update(table);
        }
        public void DeleteBill(int billId)
        {
            var bill = _billRepository.GetWithCondition(x => x.Id == billId);
            _billRepository.Delete(bill.First());
            var table = _tableDrinkRepository.GetById(bill.First().IdTable);
            _tableDrinkRepository.Delete(table);
        }
        public List<Drink> SearchWithCate(int cateId)
        {
            return _drinkRepository.SortWithCate(cateId);
        }   
        public List<Drink> SearchDrink(string keywork)
        {
            return _drinkRepository.Search(keywork);
        }
    }
}
