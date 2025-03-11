using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext _context)
        {
            if (!_context.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products != null)
                {
                    _context.Products.AddRange(products);
                    _context.SaveChanges();
                }
            }
        }
    }
}
