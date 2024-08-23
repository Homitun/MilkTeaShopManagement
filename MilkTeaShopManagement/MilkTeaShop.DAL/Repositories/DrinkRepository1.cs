using Microsoft.EntityFrameworkCore;
using MilkTeaShop.DAL.Models;

namespace MilkTeaShop.DAL.Repository
{
    public class DrinkRepository1
    {
        private MilkTeaShopManagementContext _context;

        public List<Drink> GetAll()
        {
            _context = new();
            return _context.Drinks.Include("DrinkCategory").ToList();
        }

        public Drink? GetById(int id)
        {
            _context = new();
            return _context.Drinks.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Drink drink
            )
        {
            _context.Drinks.Add(drink);
            _context.SaveChanges();
        }

        public void Update(Drink drink)
        {
            var existingDrink = _context.Drinks.FirstOrDefault(c => c.Id == drink.Id);
            if (existingDrink != null)
            {
                existingDrink.Name = drink.Name;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var drink = _context.Drinks.FirstOrDefault(c => c.Id == id);
            if (drink != null)
            {
                _context.Drinks.Remove(drink);
                _context.SaveChanges();
            }
        }
    }
}
