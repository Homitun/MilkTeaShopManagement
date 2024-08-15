using System;
using System.Collections.Generic;

namespace MilkTeaShop.DAL.Models;

public partial class DrinkCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Drink> Drinks { get; set; } = new List<Drink>();
}
