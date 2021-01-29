using IFood.Produtos.Application.Infra.Data;
using IFood.Produtos.Infra.Data.Context;
using IFood.Produtos.Infra.Data.Repository;
using IFood.Produtos.Service;
using IFood.Produtos.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IFood.Produtos.Application.Infra.DI
{
    /// <summary>
    /// Injeção de dependência do Serviço
    /// </summary>
    public static class ServiceDi
    {
        /// <summary>
        /// Adiciona o Serviço na injeção de dependência
        /// </summary>
        /// <param name="services"></param>
        /// <param name="appSettings"></param>
        public static void AddServiceDi(this IServiceCollection services, AppSettings appSettings)
        {
            // Injeção do Contexto da Aplicação
            services.AddDbContext<IFoodContext>(options =>
                options.UseSqlServer(appSettings.ConnectionString)
            );

            // Injeção do Repositório da Aplicação
            services.AddScoped<ProdutoRepository>();

            // Injeção do Serviço da Aplicação
            services.AddScoped<ProdutoService>();

            // Injeção do Proxy do Serviço da Aplicação
            services.AddScoped(serviceProvider => new ProxyService(serviceProvider));
        }
    }
}
