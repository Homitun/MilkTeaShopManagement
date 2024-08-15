using System;
using System.Collections.Generic;

namespace MilkTeaShop.DAL.Models;

public partial class Bill
{
    public int Id { get; set; }

    public DateOnly DateCheckIn { get; set; }

    public DateOnly DateCheckOut { get; set; }

    public int IdTable { get; set; }

    public int Status { get; set; }

    public virtual ICollection<BillInfo> BillInfos { get; set; } = new List<BillInfo>();

    public virtual TableDrink IdTableNavigation { get; set; } = null!;
}
