using MilkTeaShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaShop.DAL.Repositories
{

    //GUI-UI<-> SERVICE<-> REPO <-> DBCONTEXT <-> TABLE THUWCJ SUWJ OWR SQL SERVER, MY SQL
    //L1         L2        L3
    //UI         BLL       DAL CURD TABLE
    public class MemberRepository
    {
        private MilkTeaShopManagementContext _context;// khoong new mooix laanf xaif 1 laanf new

        public Account? GetOne(string userName, string password)
        {
            _context = new();
            //return _context.Account.ToList(); trar heet
            return _context.Accounts.FirstOrDefault(x => x.UserName.ToLower() == userName.ToLower() && x.PassWord == password);
            // hoac tra ve 1 dong hoac tra ve null, tim khong thay email hoac passs

        }


    }
}
