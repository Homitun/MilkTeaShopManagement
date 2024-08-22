using Microsoft.EntityFrameworkCore;
using MilkTeaShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaShop.DAL.Repository
{
    public class TableDrinkRepository
    {
        private MilkTeaShopManagementContext _context;

        public List<TableDrink> GetAll()
        {
            _context = new MilkTeaShopManagementContext();
            return _context.TableDrinks.ToList();
        }
        public List<TableDrink> GetAllActiveTable()
        {
            _context = new MilkTeaShopManagementContext();
            return _context.TableDrinks.Where(x => x.Status == "active").ToList();
        }
        public List<TableDrink> GetAllInactiveTable()
        {
            _context = new MilkTeaShopManagementContext();
            return _context.TableDrinks.Where(x => x.Status == "inactive").ToList();
        }
        public TableDrink GetById(int id)
        {
            _context = new MilkTeaShopManagementContext();
            return _context.TableDrinks.FirstOrDefault(x => x.Id == id);
        }
        public int Add(TableDrink tableDrink)
        {
            _context = new MilkTeaShopManagementContext();
            _context.TableDrinks.Add(tableDrink); 
            _context.SaveChanges();
            return tableDrink.Id;
        }

        public void Update(TableDrink tableDrink)
        {
            _context = new MilkTeaShopManagementContext();
            _context.TableDrinks.Update(tableDrink);
            _context.SaveChanges();
        }

        public void Delete(TableDrink tableDrink)
        {
            _context = new MilkTeaShopManagementContext();
            _context.TableDrinks.Remove(tableDrink);
            _context.SaveChanges();
        }
    }
}
