using IFood.Produtos.Application.Infra.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IFood.Produtos.Application.Infra.DI
{
    /// <summary>
    /// Injeção de dependência do AppSettings
    /// </summary>
    public static class AppSettingsDi
    {
        /// <summary>
        /// Adiciona o AppSettings na injeção de dependência
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="appSettings"></param>
        public static void AddAppSettingsDi(this IServiceCollection services,
            IConfiguration configuration, out AppSettings appSettings)
        {
            appSettings = new AppSettings();
            new ConfigureFromConfigurationOptions<AppSettings>(configuration.GetSection("AppSettings")).Configure(appSettings);
            services.AddSingleton(appSettings);
        }
    }
}
