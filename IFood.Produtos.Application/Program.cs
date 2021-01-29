using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace IFood.Produtos.Application
{
    /// <summary>
    /// Inicializa��o da API
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Inicializa��o da API
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Cria��o do Host
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
