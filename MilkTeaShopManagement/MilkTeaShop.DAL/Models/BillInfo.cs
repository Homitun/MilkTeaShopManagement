using System;
using System.Collections.Generic;

namespace MilkTeaShop.DAL.Models;

public partial class BillInfo
{
    public int Id { get; set; }

    public int IdBill { get; set; }

    public int IdDrink { get; set; }

    public int Count { get; set; }

    public virtual Bill IdBillNavigation { get; set; } = null!;

    public virtual Drink IdDrinkNavigation { get; set; } = null!;
}
