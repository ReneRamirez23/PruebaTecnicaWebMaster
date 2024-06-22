using Microsoft.EntityFrameworkCore;
using PruebaTecnicaWebMaster.Controllers;
using PruebaTecnicaWebMaster.Models;

namespace PruebaTecnicaWebMaster.Repositories
{
    public interface ISaleRepository
    {
        Sale GetById(int id);
        IEnumerable<Sale> GetAll();

        Task<Sale> GetByIdAsync(int id);
        IEnumerable<Product> GetAllProducts();
        Task AddAsync(Sale sale);
        Task UpdateAsync(int id);
        Task SaveChangesAsync();
    }
    public interface ISalesProductRepository
    {
        Task AddAsync(SalesProduct salesProduct);
        Task SaveChangesAsync();
        Task<List<Product>> GetProductsBySaleIdAsync(int saleId);
    }
    public interface IProductRepository2
    {
        Task<Product> GetByIdAsync(int productId);
        Task UpdateAsync(Product product);
        Task SaveChangesAsync();
    }
    public class SaleRepository : ISaleRepository
    {
        private readonly BD_ControlVentasContext _bdContext;
        public SaleRepository(BD_ControlVentasContext context)
        {
            _bdContext = context;
        }

        public async Task AddAsync(Sale sale)
        {
            await _bdContext.Sales.AddAsync(sale);
        }

        public IEnumerable<Sale> GetAll()
        {
            return _bdContext.Sales.ToList();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _bdContext.Products.ToList();
        }
        public async Task<Sale> GetByIdAsync(int id)
        {
            return await _bdContext.Sales.FindAsync(id);
        }
        public async Task UpdateAsync(int id)
        {
            var sale = _bdContext.Sales.Where(x => x.IdSale == id).FirstOrDefault();

            if (sale != null)
            {
                sale.PaidDate = DateTime.Now;
                sale.IsPaid = true;

                _bdContext.Sales.Update(sale);
                await _bdContext.SaveChangesAsync();
            }
        }

        public Sale GetById(int id)
        {
            return _bdContext.Sales.Find(id);
        }

        public async Task SaveChangesAsync()
        {
            await _bdContext.SaveChangesAsync();
        }
    }

    public class SalesProductRepository : ISalesProductRepository
    {
        private readonly BD_ControlVentasContext _bdContext;
        public SalesProductRepository(BD_ControlVentasContext context)
        {
            _bdContext = context;
        }

        public async Task AddAsync(SalesProduct salesProduct)
        {
            await _bdContext.SalesProducts.AddAsync(salesProduct);
        }

        public async Task SaveChangesAsync()
        {
            await _bdContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProductsBySaleIdAsync(int saleId)
        {
            return await _bdContext.SalesProducts
                .Where(x => x.SalesId == saleId)
                .Include(c => c.Products)
                .Select(j => new Product
                {
                    NameProducts = j.Products.NameProducts,
                    Quantity = j.Quantity ?? 0,
                    UnitPrice = (decimal)(j.Products.UnitPrice * j.Quantity),
                }).ToListAsync();
        }
    }

    public class ProductRepository2 : IProductRepository2
    {
        private readonly BD_ControlVentasContext _bdContext;
        public ProductRepository2(BD_ControlVentasContext context)
        {
            _bdContext = context;
        }

        public async Task<Product> GetByIdAsync(int productId)
        {
            return await _bdContext.Products.FindAsync(productId);
        }

        public async Task UpdateAsync(Product product)
        {
            _bdContext.Products.Update(product);
        }

        public async Task SaveChangesAsync()
        {
            await _bdContext.SaveChangesAsync();
        }
    }
}
