using IFood.Produtos.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IFood.Produtos.Service
{
    /// <summary>
    /// Proxy do Serviço
    /// </summary>
    public class ProxyService
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Obtêm a Instância de um Serviço
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private static TService Instance<TService>(IServiceProvider serviceProvider)
            where TService : class => serviceProvider.GetRequiredService<TService>();

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ProxyService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Instância do Serviço de Produto
        /// </summary>
        public ProdutoService Produto => Instance<ProdutoService>(_serviceProvider);
    }
}
