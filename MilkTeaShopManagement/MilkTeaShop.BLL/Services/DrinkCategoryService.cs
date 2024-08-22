using MilkTeaShop.DAL.Models;
using MilkTeaShop.DAL.Repository;

namespace MilkTeaShop.BLL.Service
{
    public class DrinkCategoryService
    {
        private DrinkCategoryRepository _repo = new();
        public List<DrinkCategory> GetAllDrinkCategory()
        {
            return _repo.GetAll();
        }

        public void AddDrinkCategory(DrinkCategory category)
        {
            _repo.Add(category);
        }

        public void UpdateDrinkCategory(DrinkCategory category)
        {
            var existingCategory = _repo.GetById(category.Id);
            if (existingCategory != null && category.Name != existingCategory.Name)
            {
                _repo.Update(category);
            }
        }

        public void DeleteDrinkCategory(int id)
        {
            var category = _repo.GetById(id);
            if (category != null)
            {
                _repo.Delete(id);
            }
        }

        public DrinkCategory? GetById(int id)
        {
            return _repo.GetById(id);
        }
    }
}
