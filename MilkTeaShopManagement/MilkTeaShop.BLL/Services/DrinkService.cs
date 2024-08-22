using MilkTeaShop.DAL.Models;
using MilkTeaShop.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaShop.BLL.Service
{
    public class DrinkService
    {
        private DrinkRepository _repo = new();
        public List<Drink> GetAllDrink()
        {
            return _repo.GetAll();
        }

        public void AddDrink(Drink drink)
        {
            _repo.Add(drink);
        }

        public void UpdateDrink(Drink drink)
        {
            var existingDrink = _repo.GetById(drink.Id);
            if (existingDrink != null && drink.Name != existingDrink.Name)
            {
                _repo.Update(drink);
            }
        }

        public void DeleteDrink(int id)
        {
            var category = _repo.GetById(id);
            if (category != null)
            {
                _repo.Delete(id);
            }
        }
    }
}
