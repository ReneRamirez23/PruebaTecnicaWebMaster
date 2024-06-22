using Microsoft.EntityFrameworkCore;
using PruebaTecnicaWebMaster.Models;

namespace PruebaTecnicaWebMaster.Repositories
{
    public interface IProductRepository
    {
        Product GetById(int id);
        IEnumerable<Product> GetAll();
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly BD_ControlVentasContext _dbContext;

        public ProductRepository(BD_ControlVentasContext context)
        {
            _dbContext = context;
        }
        public void Add(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Product> GetAll()
        {
            return _dbContext.Products.ToList();
        }

        public Product GetById(int id)
        {
            return _dbContext.Products.Find(id);
        }

        public void Update(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
