using Microsoft.Extensions.Configuration;

namespace _02ConnectionString
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Console.WriteLine(configuration.GetSection("constr").Value);
            Console.ReadKey();
        }
    }
}