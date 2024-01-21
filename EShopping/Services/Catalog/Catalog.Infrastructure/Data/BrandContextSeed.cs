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
                string path = Path.Combine("Data", "SeedData", "brands.json");
                var brandsData = File.ReadAllText(path);
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
