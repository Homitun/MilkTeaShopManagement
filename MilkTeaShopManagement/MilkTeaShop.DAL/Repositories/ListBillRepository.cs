using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MilkTeaShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaShop.DAL.Repositories
{
    public class ListBillRepository
    {
        private MilkTeaShopManagementContext _context;
        public List<TableRevenue> GetAllTableRevenue(DateOnly? startDate = null, DateOnly? endDate = null)
        {
            using (var _context = new MilkTeaShopManagementContext())
            {
                var query = _context.TableDrinks
                    .Include(t => t.Bills)
                    .ThenInclude(b => b.BillInfos)
                    .ThenInclude(bi => bi.IdDrinkNavigation)
                    .AsQueryable().ToList();

                // Apply date filter if specified
                if (startDate.HasValue)
                {
                    query = query.Where(x => x.Bills.Any(b => b.DateCheckIn >= startDate.Value)).ToList();                    
                }

                if (endDate.HasValue)
                {
                    query = query.Where(x => x.Bills.Any(b => b.DateCheckOut <= endDate.Value)).ToList();
                }

                foreach (var table in query)
                {
                    table.Bills = table.Bills.Where(b => startDate == null ? true : b.DateCheckIn >= startDate && endDate == null ? true : b.DateCheckOut <= endDate).ToList();
                }
                
                return query.Select(x => new TableRevenue()
                {
                    TableId = x.Id,
                    TableName = x.Name,
                    FromDate = x.Bills.Count() > 0 ? x.Bills.Min(b => b.DateCheckIn) : null,
                    ToDate = x.Bills.Count() > 0 ? x.Bills.Max(b => b.DateCheckOut) : null,
                    Revenue = x.Bills.Sum(b => b.BillInfos.Sum(bi => bi.Count * bi.IdDrinkNavigation.Price)),
                    TableBills = x.Bills.Select(b => new TableBill
                    {
                        TableName = x.Name,
                        BillId = b.Id,
                        TableId = x.Id,
                        Revenue = b.BillInfos.Sum(bi => bi.Count * bi.IdDrinkNavigation.Price),
                        FromDate = b.DateCheckIn,
                        ToDate = b.DateCheckOut,
                        BillDetails = b.BillInfos.Select(bi => new BillDetail
                        {
                            TableName = x.Name,
                            BillId = b.Id,
                            TableId = x.Id,
                            Count = bi.Count,
                            DrinkName = bi.IdDrinkNavigation.Name,
                            Price = bi.IdDrinkNavigation.Price,
                            TotalPrice = bi.IdDrinkNavigation.Price * bi.Count
                        }).ToList(),
                    }).ToList()
                }).ToList();
            }
        }
        public List<TableBill> GetTableBills(DateOnly? startDate = null, DateOnly? endDate = null)
        {
            using (var _context = new MilkTeaShopManagementContext())
            {
                var query = _context.Bills
                    .Include(b => b.BillInfos)
                    .ThenInclude(bi => bi.IdDrinkNavigation)
                    .GroupBy(b => b.IdTable)
                    .SelectMany(g => g.Select(b => new
                    {
                        TableId = b.IdTable,
                        BillId = b.Id,
                        FromDate = g.Min(b => b.DateCheckIn),
                        ToDate = g.Max(b => b.DateCheckIn),
                        Revenue = g.Sum(bi => bi.BillInfos.Sum(b => b.Count * b.IdDrinkNavigation.Price))
                    }))
                    .AsQueryable();

                // Apply date filter if specified
                if (startDate.HasValue)
                {
                    query = query.Where(x => x.FromDate >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    query = query.Where(x => x.ToDate <= endDate.Value);
                }

                return query
                    .Select(x => new TableBill
                    {
                        TableId = x.TableId,
                        BillId = x.BillId,
                        FromDate = x.FromDate,
                        ToDate = x.ToDate,
                        Revenue = x.Revenue
                    })
                    .ToList();
            }
        }
        public List<BillDetail> GetListBillDetail(int billId, DateOnly? startDate = null, DateOnly? endDate = null)
        {
            using (var _context = new MilkTeaShopManagementContext())
            {
                var query = _context.Bills
                    .Include(b => b.BillInfos)
                    .ThenInclude(bi => bi.IdDrinkNavigation)
                    .AsQueryable();

                // Apply Bill ID filter
                if (billId > 0)
                {
                    query = query.Where(b => b.Id == billId);
                }

                // Apply date filter if specified
                if (startDate.HasValue)
                {
                    var startDateTime = startDate.Value.ToDateTime(TimeOnly.MinValue);
                    query = query.Where(b => b.DateCheckIn.ToDateTime(TimeOnly.MinValue) >= startDateTime);
                }

                if (endDate.HasValue)
                {
                    var endDateTime = endDate.Value.ToDateTime(TimeOnly.MaxValue);
                    query = query.Where(b => b.DateCheckOut.ToDateTime(TimeOnly.MaxValue) <= endDateTime);
                }

                //var billDetails = query
                //    .Select(b => new BillDetail
                //    {
                //        BillId = b.Id,
                //        TableId = x.Id,
                //        Count = bi.Count,
                //        DrinkName = bi.IdDrinkNavigation.Name,
                //        Price = bi.IdDrinkNavigation.Price,
                //        TotalPrice = bi.IdDrinkNavigation.Price * bi.Count
                //    })
                //    .ToList();

                //return billDetails;
                return new List<BillDetail>();
            }

        }
    }

}
