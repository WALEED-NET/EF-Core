using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq.Expressions;
using static System.Collections.Specialized.BitVector32;
using System.Runtime.InteropServices;
using System.Transactions;

namespace _03ExecuteUpdate
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            // Create a new ConfigurationBuilder object and add the appsettings.json file to it
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();



            

            ///  Create a new SqlConnection object using the connection string stored in the appsettings.json file
            IDbConnection db = new SqlConnection(configuration.GetSection("constr").Value);

            // Create a new Wallet object with the specified values for Id, Balance, and Holder
            var walletToUpdate = new Wallet() { Id = 1007, Balance = 5655, Holder = "Itachi" };

            // Define an SQL query to Update a new row into the WALLETS table with the specified values for Id , Holder and Balance 
            var sqlQuery = "UPDATE WALLETS SET Holder = @Holder , Balance = @Balance " +
                            "Where Id = @Id";

            // Create an anonymous object with properties for Holder and Balance that match the values in the walletToInsert object
            var parameters =
                new
                {
                    Id = walletToUpdate.Id,
                    Holder = walletToUpdate.Holder,
                    Balance = walletToUpdate.Balance
                };

                db.Execute(sqlQuery,parameters);
        }
    }
}
