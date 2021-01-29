using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace IFood.Produtos.Application
{
    /// <summary>
    /// Inicialização da API
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Inicialização da API
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Criação do Host
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
