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
                string path = Path.Combine("Catalog.Infrastructure\\Data", "SeedData", "products.json");
                path = Directory.GetParent(path)!.FullName + "\\products.json";

                // i don't no why i get current path contain "Catalog.API"??
                // seed data 1 time, hard code a little bit

                var productData = File.ReadAllText("Data/SeedData/products.json");
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
