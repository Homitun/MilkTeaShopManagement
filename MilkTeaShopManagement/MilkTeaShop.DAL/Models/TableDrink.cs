using System;
using System.Collections.Generic;

namespace MilkTeaShop.DAL.Models;

public partial class TableDrink
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
