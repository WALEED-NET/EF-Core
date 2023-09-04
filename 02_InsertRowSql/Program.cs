using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;

namespace _02_InsertRowSql
{

    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // read from user input
            var walletToInsert = new Wallet()
            {
                Holder = "Awaad",
                Balance = 5500,
            };

            SqlConnection conn = new SqlConnection(configuration.GetSection("constr").Value); //1 

            // be careful of using concationation like $"{}"

            var sqlQuery = "INSERT INTO WALLETS (Holder , Balance) VALUES " +
                            "(@Holder , @Balance)"; //2

            SqlParameter holderParameter = new SqlParameter()
            {
                ParameterName = "Holder",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = walletToInsert.Holder
            };

            SqlParameter BalanceParameter = new SqlParameter()
            {
                ParameterName = "Balance",
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input,
                Value = walletToInsert.Balance
            };

            SqlCommand command = new SqlCommand(sqlQuery , conn); // 3

            command.Parameters.Add(holderParameter);
            command.Parameters.Add(BalanceParameter);
            command.CommandType = CommandType.Text;// 4


            conn.Open();    // 5

            if (command.ExecuteNonQuery() > 0 )
            {
                Console.WriteLine($"Wallet For {walletToInsert.Holder} Has Added Successfuly");
            }
            else
            {
                Console.WriteLine($"ERROR : Wallet For {walletToInsert.Holder} Was not Added Successfuly");
            }

            conn.Close();
            
            Console.ReadKey();
        }
    }
}