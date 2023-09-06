using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq.Expressions;
using static System.Collections.Specialized.BitVector32;
using System.Runtime.InteropServices;
using System.Transactions;

namespace _03_InsertRawReturnIdentity
{
    internal class Program
    {
        /// <summary>
        /// This code demonstrates how to use Dapper, a simple object mapper for .NET, to insert a new row into a database table and
        /// retrieve the ID of the newly inserted row. It uses a ConfigurationBuilder object to read a connection string from an appsettings.json
        /// file and creates a new SqlConnection object using that connection string.The code then defines an SQL query to insert a new row into the
        /// WALLETS table with specified values for Holder and Balance. The query also returns the ID of the newly inserted row. The code creates
        /// an anonymous object with properties for Holder and Balance that match the values in a Wallet object, then executes the SQL query using Dapper
        /// and stores the returned ID in the Id property of the Wallet object. Finally, it writes the contents of the Wallet object to the console.
        /// </summary>
        static void Main(string[] args)
        {
            // Create a new ConfigurationBuilder object and add the appsettings.json file to it
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();



            /// <summary>
            ///  Create a new SqlConnection object using the connection string stored in the appsettings.json file
            /// IDbConnection db: This is declaring a variable db of type IDbConnection.IDbConnection is an interface provided by.NET
            /// that represents a connection to a database.This interface defines methods for opening and closing connections to a database,
            /// and also for beginning transactions.
            /// 
            /// new SqlConnection(...) : This is creating a new instance of the SqlConnection class, which is a concrete implementation
            /// /of the IDbConnection interface specifically for SQL Server databases.
            /// 
            /// configuration.GetSection("constr").Value: This is accessing the configuration object that was built using the ConfigurationBuilder
            /// earlier in the code.It’s retrieving the value of the “constr” section from the configuration (typically from an appsettings.json file).
            /// 
            /// This value is expected to be the connection string for your database.
            /// 
            /// So, in summary, this line of code is creating a new database connection to a SQL Server database using the connection string
            /// specified in your configuration under the key “constr”.
            /// 
            /// The connection is represented by an instance of the SqlConnection class,
            /// but it’s stored in a variable of type IDbConnection, which means you could easily switch to a different type of database by just
            /// changing this line of code.
            /// 
            /// The actual opening of the connection to the database doesn’t happen until you call the Open() method on
            /// the db object.
            /// </summary>

            ///  Create a new SqlConnection object using the connection string stored in the appsettings.json file
            IDbConnection db = new SqlConnection(configuration.GetSection("constr").Value);

            // Create a new Wallet object with the specified values for Id, Balance, and Holder
            var walletToInsert = new Wallet() { Id = 1009, Balance = 454, Holder = "Dapper Identity" };

            // Define an SQL query to insert a new row into the WALLETS table with the specified values for Holder and Balance
            // The query also returns the ID of the newly inserted row
            var sqlQuery = "INSERT INTO WALLETS (Holder , Balance) VALUES (@Holder , @Balance)" +
                            "Select CAST(Scope_Identity() AS INT )";

            // Create an anonymous object with properties for Holder and Balance that match the values in the walletToInsert object
            var parameters =
                new
                {
                    Holder = walletToInsert.Holder,
                    Balance = walletToInsert.Balance
                };

            // Execute the SQL query using the Dapper ORM and store the returned ID in the Id property of the walletToInsert object
            walletToInsert.Id = db.Query<int>(sqlQuery, parameters).Single();

            // Write the contents of the walletToInsert object to the console
            Console.WriteLine(walletToInsert);
        }
    }
}
