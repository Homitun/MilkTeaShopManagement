using MilkTeaShop.DAL.Models;
using MilkTeaShop.DAL.Repositories;


namespace MilkTeaShop.BLL.Services
{
    
    public class TableDrinkService
    {

        private TableDrinkRepository1 _repo = new();
        public List<TableDrink> GetAllTableDrink()
        {
            return _repo.GetAll();
        }

        public void AddTableDrink(TableDrink table)
        {
            _repo.Add(table);
        }

        public void UpdateTableDrink(TableDrink table)
        {
            var existingtable = _repo.GetById(table.Id);
            if (existingtable != null && table.Name != existingtable.Name)
            {
                _repo.Update(table);
            }
        }

        public void DeleteTableDrink(int id)
        {
            var table = _repo.GetById(id);
            if (table != null)
            {
                _repo.Delete(id);
            }
        }

        public TableDrink? GetById(int id)
        {
            return _repo.GetById(id);
        }
    }
}
