using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;

namespace _03_ExecuteRawSql
{

    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            IDbConnection db = new SqlConnection(configuration.GetSection("constr").Value); //1 

            var sqlQuery = "Select * From Wallets "; //2

            // Dapper return Result as Dynamic or static
            {
                Console.WriteLine("———————————————— using Dynamic Query —-----------------");
                var dynamicResult = db.Query(sqlQuery);

                foreach (dynamic item in dynamicResult)
                {
                    Console.WriteLine(item);
                }

                //{ DapperRow, Id = '2', Holder = 'Reem', Balance = '4000'}
                //{ DapperRow, Id = '3', Holder = 'Sammeh', Balance = '6000'}
                //{ DapperRow, Id = '1002', Holder = 'Awaad', Balance = '5500'}
                //{ DapperRow, Id = '1003', Holder = 'KHALED', Balance = '6598'}
                //{ DapperRow, Id = '1004', Holder = 'KHALED2', Balance = '6598'}
                //{ DapperRow, Id = '1005', Holder = 'Jack', Balance = '5675'}
                //{ DapperRow, Id = '1006', Holder = 'Jack', Balance = '5675'}
                //{ DapperRow, Id = '1007', Holder = 'Jack', Balance = '5675'}

            }
            Console.WriteLine("———————————————— using Typed Query —-----------------");

            var resultAsTyped = db.Query<Wallet>(sqlQuery);

            foreach(Wallet wallet in resultAsTyped)
            {
                Console.WriteLine(wallet);
            }

            //[2] Reem(4, 000 ر.س.?)
            //[3] Sammeh(6, 000 ر.س.?)
            //[1002] Awaad(5, 500 ر.س.?)
            //[1003] KHALED(6, 598 ر.س.?)
            //[1004] KHALED2(6, 598 ر.س.?)
            //[1005] Jack(5, 675 ر.س.?)
            //[1006] Jack(5, 675 ر.س.?)
            //[1007] Jack(5, 675 ر.س.?)
            Console.ReadKey();
        }
    }
}