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

            
         

            SqlCommand command = conn.CreateCommand();

            
            command.CommandType = CommandType.Text;// 4


            conn.Open();    // 5

            // Begin Transaction
            SqlTransaction transaction = conn.BeginTransaction();
            command.Transaction = transaction;

            try
            {
                command.CommandText = "UPDATE WALLETS SET Balance = Balance - 1000 Where Id = 2";
                command.ExecuteNonQuery();

                command.CommandText = "UPDATE WALLETS SET Balance = Balance + 1000 Where Id = 3";
                command.ExecuteNonQuery();

                transaction.Commit();
                Console.WriteLine("Transaction Of transfer Completed Sucsessfully");
            }
            catch 
            {
                try
                {
                    transaction.Rollback();

                }
                catch 
                {
                    // log errors

                }                
            }
            finally
            {
                try
                {
                    conn.Close();

                }
                catch 
                {
                    
                }
            }

            
            Console.ReadKey();
        }
    }
}