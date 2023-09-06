using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;
using System.Reflection.PortableExecutable;

namespace _02ExecuteRawSqlDataAdapter
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

            conn.Open();    // 5

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlQuery,conn);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            conn.Close();

            foreach (DataRow dataRow in dt.Rows)    // 
            {
                var wallet = new Wallet()
                {
                    Id = Convert.ToInt32( dataRow["Id"] ),
                    Holder = dataRow["Holder"].ToString() ,
                    Balance = Convert.ToDecimal(dataRow["Balance"])
                };
                Console.WriteLine(wallet);
            }
            
            Console.ReadKey();
        }
    }
}