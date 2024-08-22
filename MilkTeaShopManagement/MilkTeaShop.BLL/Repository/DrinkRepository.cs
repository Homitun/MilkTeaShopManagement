using Microsoft.EntityFrameworkCore;
using MilkTeaShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaShop.DAL.Repository
{
    public class DrinkRepository
    {
        private MilkTeaShopManagementContext _context;

        public List<Drink> GetAll()
        {
            _context = new MilkTeaShopManagementContext();
            return _context.Drinks.Include(x => x.IdCategoryNavigation).ToList();
        }
        
        public List<Drink> Search(string keyword)
        {
            _context = new MilkTeaShopManagementContext();
            return _context.Drinks.Include(x => x.IdCategoryNavigation).Where(y => y.Name.Contains(keyword)).ToList();
        }
        public List<Drink> SortWithCate(int cateId)
        {
            _context = new MilkTeaShopManagementContext();
            return _context.Drinks.Where(x => x.IdCategory ==  cateId).Include(y => y.IdCategoryNavigation).ToList();
        }
    }
}
