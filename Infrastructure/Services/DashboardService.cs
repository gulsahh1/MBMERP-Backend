using Application.DTOS;
using Application.Interfaces;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ERPDbContext _context;

        public DashboardService(ERPDbContext context)
        {
            _context = context;
        }


        public async Task<DashboardDto> GetDashboardDataAsync()
        {
            var today = DateTime.UtcNow;
            var last30Days = today.AddDays(-30);

            // base query
            var ordersQuery = _context.Orders
                .Where(x => x.IsActive && x.OrderDate >= last30Days);

            var orders = await ordersQuery.ToListAsync();

            // KPI
            var totalProducts = await _context.Products.CountAsync(x => x.isActive);
            var totalCustomers = await _context.Customers.CountAsync(x => x.isActive);
            var totalOrders = orders.Count;

            //  DAILY SALES
            var dailySales = await ordersQuery
                .GroupBy(x => x.OrderDate.Date)
                .Select(g => new DailySalesDto
                {
                    Date = g.Key,
                    TotalSales = g.Sum(x => x.TotalAmount),
                    OrderCount = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            //  ORDER TREND
            var orderTrends = await ordersQuery
                .GroupBy(x => x.OrderDate.Date)
                .Select(g => new OrderTrendDto
                {
                    Date = g.Key,
                    ThisWeekOrders = g.Count(x => x.OrderDate >= today.AddDays(-7)),
                    LastWeekOrders = g.Count(x => x.OrderDate < today.AddDays(-7))
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            //  STOCK MOVEMENTS
            var stockMovements = await _context.StockTransactions
                .Include(x => x.Product)
                .OrderByDescending(x => x.Date)
                .Take(30)
                .Select(x => new StockMovmentDto
                {
                    ProductId = x.ProductID,
                    ProductName = x.Product != null ? x.Product.ProductName : "",
                    Quantity = x.Quantity,
                    TransactionType = x.TransactionType.ToString(),
                    Date = x.Date,
                    CurrentStock = (int)_context.Stocks
                        .Where(s => s.ProductID == x.ProductID)
                        .Select(s => s.Quantity)
                        .FirstOrDefault()
                })
                .ToListAsync();

            // CATEGORY SALES
            var categorySales = await _context.SaleDetails
                .Include(x => x.Product)
                .ThenInclude(x => x.Category)
                .GroupBy(x => new
                {
                    x.Product.CategoryID,
                    x.Product.Category.CategoryName
                })
                .Select(g => new CategorySalesDto
                {
                    CategoryId = g.Key.CategoryID,
                    CategoryName = g.Key.CategoryName,
                    TotalQuantity = g.Sum(x => x.Quantity),
                    TotalRevenue = g.Sum(x => x.Quantity * x.UnitPrice)
                })
                .ToListAsync();

            var totalRevenue = categorySales.Sum(x => x.TotalRevenue);

            foreach (var item in categorySales)
            {
                item.Percentage = totalRevenue == 0
                    ? 0
                    : (item.TotalRevenue / totalRevenue) * 100;
            }

            //  TOP PRODUCTS
            var topProducts = await _context.SaleDetails
                .Include(x => x.Product)
                .GroupBy(x => new
                {
                    x.ProductID,
                    x.Product.ProductName
                })
                .Select(g => new TopProductDto
                {
                    ProductId = g.Key.ProductID,
                    ProductName = g.Key.ProductName,
                    TotalSoldQuantity = g.Sum(x => x.Quantity),
                    TotalRevenue = g.Sum(x => x.Quantity * x.UnitPrice),
                    CategoryName = g.First().Product.Category.CategoryName
                })
                .OrderByDescending(x => x.TotalRevenue)
                .Take(10)
                .ToListAsync();

            return new DashboardDto
            {
                TotalProducts = totalProducts,
                TotalCustomers = totalCustomers,
                TotalOrders = totalOrders,

                DailySales = dailySales,
                OrderTrends = orderTrends,
                StockMovements = stockMovements,
                CategorySales = categorySales,
                TopProducts = topProducts
            };
        }
    }
}
