using MilkTeaShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaShop.DAL.Repositories
{
   
    public class TableDrinkRepository1
    {
        private MilkTeaShopManagementContext _context;

        public List<TableDrink> GetAll()
        {
            _context = new();
            return _context.TableDrinks.ToList();
        }

        public TableDrink? GetById(int id)
        {
            _context = new();
            return _context.TableDrinks.FirstOrDefault(c => c.Id == id);
        }

        public void Add(TableDrink table)
        {
            _context.TableDrinks.Add(table);
            _context.SaveChanges();
        }

        public void Update(TableDrink table)
        {
            var existngTable = _context.TableDrinks.FirstOrDefault(c => c.Id == table.Id);
            if (existngTable != null)
            {
                existngTable.Name = table.Name;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var table = _context.TableDrinks.FirstOrDefault(c => c.Id == id);
            if (table != null)
            {
                _context.TableDrinks.Remove(table);
                _context.SaveChanges();
            }
        }
    }
}
