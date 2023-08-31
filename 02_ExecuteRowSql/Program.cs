using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;

namespace _02ConnectionString
{

    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            SqlConnection conn = new SqlConnection(configuration.GetSection("constr").Value); //1 

            var sqlQuery = "Select * From Wallets "; //2

            SqlCommand command = new SqlCommand(sqlQuery , conn); // 3
            command.CommandType = CommandType.Text;// 4

            conn.Open();    // 5

            SqlDataReader reader = command.ExecuteReader(); //6

            Wallet wallet;
            while (reader.Read())   // 7 how to get data into object
            {
                wallet = new Wallet()
                {
                    Id = reader.GetInt32("Id"),
                    Holder = reader.GetString("Holder"),
                    Balance = reader.GetDecimal("Balance")
                };
                Console.WriteLine(wallet);
            }

            conn.Close();
            
            Console.ReadKey();
        }
    }
}