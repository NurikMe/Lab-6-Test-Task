using Lab6TestTask.Data;
using Lab6TestTask.Models;
using Lab6TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab6TestTask.Services.Implementations;

/// <summary>
/// WarehouseService.
/// Implement methods here.
/// </summary>
public class WarehouseService : IWarehouseService
{
    private readonly ApplicationDbContext _dbContext;

    public WarehouseService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Warehouse> GetWarehouseAsync()
    {
        return _dbContext.Products
            .Include(p => p.Warehouse)
            .GroupBy(p => p.Warehouse)
            .Select(group => new
            {
                Warehouse = group.Key,
                Value = group.Sum(el => el.Quantity * el.Price)
            })
            .OrderByDescending(obj => obj.Value)
            .FirstOrDefault()!.Warehouse;
    }

    public async Task<IEnumerable<Warehouse>> GetWarehousesAsync()
    {
        return _dbContext.Products
            .Include(p => p.Warehouse)
            .Where(p => p.ReceivedDate.Month > 3 && p.ReceivedDate.Month < 7 && p.ReceivedDate.Year == 2025)
            .Select(p => p.Warehouse)
            .Distinct();
    }
}
