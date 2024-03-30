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
                string path = Path.Combine("Catalog.Infrastructure\\Data", "SeedData", "types.json");
                path += "\\types.json";

                // i don't no why i get current path contain "Catalog.API"??
                // seed data 1 time, hard code a little bit

                var typesData = File.ReadAllText("C:\\Users\\ACER\\Desktop\\General\\C#\\EShopping\\EShopping\\Services\\Catalog\\Catalog.Infrastructure\\Data\\SeedData\\types.json");
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
