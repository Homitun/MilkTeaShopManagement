using MilkTeaShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaShop.DAL.Repository
{
    public class DrinkCategoryRepository
    {
        private MilkTeaShopManagementContext _context;

        public List<DrinkCategory> GetAll()
        {
            _context = new();
            return _context.DrinkCategories.ToList();
        }

        public DrinkCategory? GetById(int id)
        {
            _context = new();
            return _context.DrinkCategories.FirstOrDefault(c => c.Id == id);
        }

        public void Add(DrinkCategory category)
        {
            _context.DrinkCategories.Add(category);
            _context.SaveChanges();
        }

        public void Update(DrinkCategory category)
        {
            var existingCategory = _context.DrinkCategories.FirstOrDefault(c => c.Id == category.Id);
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var category = _context.DrinkCategories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _context.DrinkCategories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}
