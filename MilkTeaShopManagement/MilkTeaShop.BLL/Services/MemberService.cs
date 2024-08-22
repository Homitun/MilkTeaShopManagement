using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MilkTeaShop.DAL.Models;
using MilkTeaShop.DAL.Repositories;

namespace MilkTeaShop.BLL.Services
{
    public class MemberService
    {
        private MemberRepository _repo = new();

        public Account Authenticate(String username, String password)
        {
            return _repo.GetOne(username, password);

        }
    }
}
