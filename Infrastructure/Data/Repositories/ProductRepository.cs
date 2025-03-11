using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ProductRepository(StoreContext _storeContext) : IProductRepository
    {
        public void AddProduct(Product product)
        {
            _storeContext.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            _storeContext.Products.Remove(product);

        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _storeContext.Products.FindAsync(id);

        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort)
        {
            var query = _storeContext.Products.AsQueryable();
            if(brand != null)
            {
                query = query.Where(b => b.Brand == brand);
            }
            if(type != null)
            {
                query = query.Where(b=> b.Type == type);
            }

            switch(sort)
            {
                case "priceAsc": query = query.OrderBy(x=>x.Price); break;
                case "priceDesc": query = query.OrderByDescending(x => x.Price); break;
                default : query = query.OrderBy(x => x.Name); break;
            }

            return await query.ToListAsync();
        }

        public bool ProductExists(int id)
        {
            return _storeContext.Products.Any(x => x.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _storeContext.SaveChangesAsync() > 0;
        }

        public void UpdateProduct(Product product)
        {
            _storeContext.Entry(product).State = EntityState.Modified;
        }

        public async Task<IReadOnlyList<string>> GetBrandsAsync()
        {
            return await _storeContext.Products.Select(x => x.Brand).Distinct().ToListAsync();
        }
        public async Task<IReadOnlyList<string>> GetTypesAsync()
        {
            return await _storeContext.Products.Select(x => x.Type).Distinct().ToListAsync();
        }
    }
}
