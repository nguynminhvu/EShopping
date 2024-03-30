using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class BrandContextSeed
    {
        public static async Task SeedData(IMongoCollection<ProductBrand> brandCollection)
        {
            bool checkBrands = brandCollection.Find(x => true).Any();
            if (!checkBrands)
            {
                string path = Path.Combine("Catalog.Infrastructure\\Data", "SeedData", "brands.json");
                path = Directory.GetParent(path)!.FullName + "\\brands.json";

                // i don't no why i get current path contain "Catalog.API"??
                // seed data 1 time, hard code a little bit

                var brandsData = File.ReadAllText("C:\\Users\\ACER\\Desktop\\General\\C#\\EShopping\\EShopping\\Services\\Catalog\\Catalog.Infrastructure\\Data\\SeedData\\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands != null)
                {
                    foreach (var brand in brands)
                    {
                        await brandCollection.InsertOneAsync(brand);
                    }
                }
            }
        }
    }
}
