using _03_ExecuteRawSql;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq.Expressions;

namespace _03_SimpleInsert
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // store connection string in code
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            IDbConnection db = new SqlConnection(configuration.GetSection("constr").Value);

            var walletToInsert = new Wallet() { Id = 1008, Balance = 4356, Holder = "DapperSimpleInsert" };

            var sqlQuery = "INSERT INTO WALLETS (Holder , Balance) VALUES (@Holder , @Balance)";

            db.Execute(sqlQuery,
                new {
                    Holder = walletToInsert.Holder ,
                    Balance = walletToInsert.Balance
                });


        }
    }
}