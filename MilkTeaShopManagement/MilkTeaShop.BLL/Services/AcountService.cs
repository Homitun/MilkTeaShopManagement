using MilkTeaShop.DAL.Models;
using MilkTeaShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaShop.BLL.Services
{
    public class AcountService
    {
        private AccountRepo _repo = new();
        public List<Account> GetAllAccount()
        {
            return _repo.GetAll();
        }
        public void AddAccount(Account account)
        {
            _repo.Add(account);
        }
        public void UpdateAccount(Account account)
        {
            _repo.Update(account);
        }
        public void RemoveAccount(Account account)
        {
            _repo.Delete(account);
        }

    }
}
