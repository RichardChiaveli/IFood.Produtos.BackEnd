using AutoMapper;
using IFood.Produtos.Domain.Mappings.Map;

namespace IFood.Produtos.Domain.Mappings
{
    /// <summary>
    /// Classe que implementa o mapeamento entre os objetos via AutoMapper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class AutoMapper<T> where  T : class
    {
        /// <summary>
        /// Inicializa o AutoMapper com os objetos de transferência
        /// </summary>
        /// <returns></returns>
        private static IMapper Initialize()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.MapearProduto();
            }).CreateMapper();
        }

        /// <summary>
        /// Valida o AutoMapper
        /// </summary>
        public static bool IsValid
        {
            get
            {
                try
                {
                    Initialize().ConfigurationProvider.AssertConfigurationIsValid();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Realiza a Transferência de Valores de um Objeto para outro
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static TValue CopyTo<TValue>(T value) => Initialize().Map<TValue>(value);
    }
}
