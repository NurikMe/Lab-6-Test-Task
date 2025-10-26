using Lab6TestTask.Data;
using Lab6TestTask.Enums;
using Lab6TestTask.Models;
using Lab6TestTask.Services.Interfaces;

namespace Lab6TestTask.Services.Implementations;

/// <summary>
/// ProductService.
/// Implement methods here.
/// </summary>
public class ProductService : IProductService
{
    private readonly ApplicationDbContext _dbContext;

    public ProductService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product> GetProductAsync()
    {
        return _dbContext.Products
            .Where(p => p.Status == ProductStatus.Reserved)
            .OrderByDescending(p => p.Price)
            .FirstOrDefault()!;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return _dbContext.Products
            .Where(p => p.ReceivedDate.Year == 2025 && p.Quantity > 1000);
    }
}
