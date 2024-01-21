using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public static class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> mongoCollection)
        {
            bool productCheck = mongoCollection.Find(x => true).Any();
            if (!productCheck)
            {
                var path = Path.Combine("Data", "SeedData", "products.json");
                var productData = File.ReadAllText(path);
                var products = JsonSerializer.Deserialize<List<Product>>(productData);
                if (products != null)
                {
                    foreach (var item in products)
                    {
                        mongoCollection.InsertOne(item);
                    }
                }
            }
        }
    }
}
