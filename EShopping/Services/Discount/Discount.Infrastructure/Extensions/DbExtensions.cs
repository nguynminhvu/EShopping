using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Infrastructure.Extensions
{
    public static class DbExtension
    {
        // Can't inject constructor in static class, so we use this way to get service
        public static IHost MigrateDatabases<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var config = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    logger.LogInformation("Discount DB Migration Started");
                    ApplyMigration(config);
                    logger.LogInformation("Discount DB Migration Finished");

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error at MigrateDatabases: " + e.Message);
                }
            }
            return host;
        }

        private static void ApplyMigration(IConfiguration config)
         {
            using (var connection = new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString")))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand
                {
                    Connection = connection
                })
                {
                    cmd.CommandText = "DROP TABLE IF EXISTS Coupon";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, ProductName VARCHAR(500) NOT NULL,Description TEXT, Amount INT)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Coupon(ProductName,Description,Amount) VALUES('ltkl','description 123 ds',10)";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
