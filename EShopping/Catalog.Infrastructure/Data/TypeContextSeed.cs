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
    public static class TypeContextSeed
    {
        public static async Task SeedData(IMongoCollection<ProductType> mongoCollection)
        {
            bool checkType = await (await mongoCollection.FindAsync(x => true)).AnyAsync();
            if (!checkType)
            {
                var path = Path.Combine("Data", "SeedData", "types.json");
                var typesData = File.ReadAllText(path);
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types != null)
                {
                    foreach (var item in types)
                    {
                        await mongoCollection.InsertOneAsync(item);
                    }
                }
            }
        }
    }
}
