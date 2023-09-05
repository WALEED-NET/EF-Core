using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;

namespace _02DeleteRowSql
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

            var sqlQuery = "Delete From WALLETS Where Id = @Id "; //2
            SqlParameter idParameter = new SqlParameter()
            {
                ParameterName = "Id",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = 1
            };


            SqlCommand command = new SqlCommand(sqlQuery , conn); // 3

            command.Parameters.Add(idParameter);
            command.CommandType = CommandType.Text;// 4


            conn.Open();    // 5

            if (command.ExecuteNonQuery() > 0 )
            {
                Console.WriteLine($"Wallet Has Deleted Successfuly");
            }
            else
            {
                Console.WriteLine($"ERROR : Wallet  Was not Deleted Successfuly");
            }

            conn.Close();
            
            Console.ReadKey();
        }
    }
}