using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq.Expressions;
using static System.Collections.Specialized.BitVector32;
using System.Runtime.InteropServices;
using System.Transactions;

namespace _03ExcuteDelete
{
    internal class Program
    {

        static void Main(string[] args)
        {
            // Create a new ConfigurationBuilder object and add the appsettings.json file to it
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Create a new SqlConnection object using the connection string stored in the appsettings.json file
            IDbConnection db = new SqlConnection(configuration.GetSection("constr").Value);

            // Create a new Wallet object with the specified values for Id
            var walletToUpdate = new Wallet() { Id = 1006 };

            // Define an SQL query to Delete a row from the WALLETS table with the specified value for Id
            var sqlQuery = "Delete From WALLETS Where Id = @Id ;";

            // Create an anonymous object with properties for Id that match the values in the walletToUpdate object
            var parameters =
                new
                {
                    Id = walletToUpdate.Id,
                };

            // Execute the SQL query using the db connection and check if any rows were affected
            if (db.Execute(sqlQuery, parameters) > 0)
            {
                // If any rows were affected, print "Done" to the console
                Console.WriteLine("Done");
            }
        }
    }
}
