using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MilkTeaShop.DAL.DTO;
using MilkTeaShop.DAL.Models;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaShop.DAL.Repository
{
    public class BillInfoRepository
    {
        private MilkTeaShopManagementContext _context;

        public BillInforView GetBillInfoWithId(int billId)
        {
            _context = new MilkTeaShopManagementContext();

            var temp = _context.BillInfos.Where(x => x.IdBill == billId).ToList();
            if(temp.IsNullOrEmpty())
            {
                var billinfo = _context.Bills.Include(x => x.IdTableNavigation).Where(y => y.Id == billId).ToList();
                return billinfo.Select(e => new BillInforView 
                {  
                BillStatus = e.Status,
                DateCheckIn = e.DateCheckIn,
                DateCheckOut = e.DateCheckOut,
                TableName = e.IdTableNavigation.Name,
                ID = e.Id,
                }).Last();
            }

            return  _context.BillInfos.Where(x => x.IdBill == billId).
                    Include(a => a.IdDrinkNavigation).
                    ThenInclude(aa => aa.IdCategoryNavigation).
                    Include(b => b.IdBillNavigation).
                    ThenInclude(c => c.IdTableNavigation).
                    GroupBy(d => d.IdBill).
                    Select(e => new BillInforView
                    {
                        ID = e.Key,
                        BillStatus = e.First().IdBillNavigation.Status,
                        DateCheckIn = e.First().IdBillNavigation.DateCheckIn,
                        DateCheckOut = e.First().IdBillNavigation.DateCheckOut,
                        TableName = e.First().IdBillNavigation.IdTableNavigation.Name,
                        drinkInTables = e.Select(f => new DrinkInTable
                        {
                            DrinkID = f.IdDrink,
                            DrinkName = f.IdDrinkNavigation.Name,
                            Price = f.IdDrinkNavigation.Price,
                            Quanity = f.Count
                        }).ToList(),
                    }).First();

        }
        public List<BillInfo> GetWithCondition(Expression<Func<BillInfo, bool>> exception = null)
        {
            _context = new MilkTeaShopManagementContext();
            return _context.BillInfos.Where(exception).ToList();
        }

        public void UpdateQuantity(int billId, int drinkId, int quantity) 
        {
            _context = new MilkTeaShopManagementContext();
            var temp = _context.BillInfos.FirstOrDefault(e => e.IdBill == billId && e.IdDrink == drinkId);
            if (temp != null)
            {
                temp.Count += quantity;
                if(temp.Count == 0)
                {
                    _context.BillInfos.Remove(temp);
                }
            }
            else
            {
                _context.BillInfos.Add(new BillInfo
                {
                    IdBill = billId,
                    IdDrink = drinkId,
                    Count = Math.Abs(quantity)
                });
            }
            _context.SaveChanges();
        }

        public void Add(BillInfo billInfo)
        {
            _context = new MilkTeaShopManagementContext();
            _context.BillInfos.Add(billInfo);
            _context.SaveChanges();
        }

        public void Update(BillInfo billInfo)
        {
            _context = new MilkTeaShopManagementContext();
            _context.BillInfos.Update(billInfo);
            _context.SaveChanges();
        }

        public void Delete(BillInfo billInfo)
        {
            _context = new MilkTeaShopManagementContext();
            _context.BillInfos.Remove(billInfo);
            _context.SaveChanges();
        }
    }
}
