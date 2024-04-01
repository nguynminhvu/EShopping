using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public IMongoCollection<ProductBrand> Brands { get; }

        public IMongoCollection<ProductType> Types { get; }

        public CatalogContext(IConfiguration configuration)
        {
            //must install package configuration.binder to use

            // Init MongoClient from configuration "ConnectionString"
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            //get object database from MongoClient
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Console.WriteLine($"connection string nek: {configuration.GetValue<string>("DatabaseSettings:ConnectionString")}");

            Products = database.GetCollection<Product>("DatabaseSettings:CollectionName");
            Brands = database.GetCollection<ProductBrand>("DatabaseSetting:BrandsCollection");
            Types = database.GetCollection<ProductType>("DatabaseSettings:TypesCollection");

            BrandContextSeed.SeedData(Brands);
            TypeContextSeed.SeedData(Types);
            CatalogContextSeed.SeedData(Products);
        }
    }
}
