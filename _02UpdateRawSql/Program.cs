using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;

namespace _02UpdateRowSql
{

    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

           

            SqlConnection conn = new SqlConnection(configuration.GetSection("constr").Value); //1 

            // be careful of using concationation like $"{}"

            var sqlQuery = "UPDATE WALLETS SET Holder = @Holder, Balance = @Balance  " +
                           "Where Id = @Id "; //2
            SqlParameter idParameter = new SqlParameter()
            {
                ParameterName = "Id",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = 1
            };

            SqlParameter holderParameter = new SqlParameter()
            {
                ParameterName = "Holder",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = "Ahmed"
            };

            SqlParameter BalanceParameter = new SqlParameter()
            {
                ParameterName = "Balance",
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input,
                Value = 9000
            };

            SqlCommand command = new SqlCommand(sqlQuery , conn); // 3

            command.Parameters.Add(idParameter);
            command.Parameters.Add(holderParameter);
            command.Parameters.Add(BalanceParameter);
            
            command.CommandType = CommandType.Text;// 4


            conn.Open();    // 5

            if (command.ExecuteNonQuery() > 0 )
            {
                Console.WriteLine($"Wallet Has Added Successfuly");
            }
            else
            {
                Console.WriteLine($"ERROR : Wallet  Was not Added Successfuly");
            }

            conn.Close();
            
            Console.ReadKey();
        }
    }
}