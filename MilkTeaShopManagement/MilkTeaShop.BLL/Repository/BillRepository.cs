using Microsoft.EntityFrameworkCore;
using MilkTeaShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaShop.DAL.Repository
{
    public class BillRepository
    {
        private MilkTeaShopManagementContext _context;

        public List<Bill> GetAll()
        {
            _context = new MilkTeaShopManagementContext();
            return _context.Bills.ToList();
        }
        public List<Bill> GetWithCondition(Expression<Func<Bill, bool>> exception = null)
        {
            _context = new MilkTeaShopManagementContext();
            return _context.Bills.Where(exception).ToList();
        }
        public List<Bill> GetAllWithstatus1()
        {
            _context = new MilkTeaShopManagementContext();
            return _context.Bills.Where(x => x.Status == 1).ToList();
        }
        public List<Bill> GetAllWithstatus0()
        {
            _context = new MilkTeaShopManagementContext();
            return _context.Bills.Where(x => x.Status == 0).Include(x => x.IdTableNavigation).ToList();
        }
        public void UpdateStatus(int billId, int status)
        {
            _context = new MilkTeaShopManagementContext();
            var bill = _context.Bills.First(x => x.Id == billId);
            bill.Status = status;
            bill.DateCheckOut = DateOnly.FromDateTime(DateTime.Now);
            _context.SaveChanges();
        }
        public int Add(Bill bill)
        {
            _context = new MilkTeaShopManagementContext();
            _context.Bills.Add(bill);
            _context.SaveChanges();
            return bill.Id;
        }

        public void Update(Bill bill)
        {
            _context = new MilkTeaShopManagementContext();
            _context.Bills.Update(bill);
            _context.SaveChanges();
        }

        public void Delete(Bill bill)
        {
            _context = new MilkTeaShopManagementContext();
            _context.Bills.Remove(bill);
            _context.SaveChanges();
        }
    }
}
