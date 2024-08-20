using MilkTeaShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaShop.DAL.Repositories
{
    public class AccountRepo
    {
        private MilkTeaShopManagementContext _context;
        public List<Account> GetAll()
        {
            _context = new();
            return _context.Accounts.ToList();// select * from...
            //return _context.Accounts.Include("...").ToList();// select * from... thêm include 
        }
        public void Add (Account account) 
        {
            _context = new();
            _context.Accounts.Add(account);// luu vao ram 
            _context.SaveChanges();// luu thuc su vao database 
        }
        public void Update(Account account)
        {
            _context = new();
            _context.Accounts.Update(account);
            _context.SaveChanges();
        }
        public void Delete(Account account)
        {
            _context = new();
            _context.Accounts.Remove(account); 
            _context.SaveChanges();
        }
    }
}
