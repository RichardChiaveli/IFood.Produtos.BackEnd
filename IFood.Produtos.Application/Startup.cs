using IFood.Produtos.Application.Infra.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace IFood.Produtos.Application
{
    /// <summary>
    /// Classe de configuração e inicialização da API
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuração da API
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Versão do Assembly da API
        /// </summary>
        private readonly string _apiVersion = (Assembly.GetEntryAssembly() ?? throw new Exception("Problema ao encontrar a Versão da Api"))
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

        /// <summary>
        /// Inicialização da Configuração da API
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath($"{env.ContentRootPath}/Infra/Environment/")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);

            Configuration = builder.Build();
        }

        /// <summary>
        /// Configurações das Injeções de Dependências dos Serviços
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAppSettingsDi(Configuration, out var appSettings);

            services.AddServiceDi(appSettings);
            
            services.AddSwaggerDi(_apiVersion);

            services.AddCors();
        }

        /// <summary>
        /// Configuração do Ambiente e Aplicação da API
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();

            app.UseStaticFiles();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(host => true)
                .AllowCredentials()
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{_apiVersion}/swagger.json",
                    "Documentação do IFood.Produtos.Application");

                c.DocumentTitle = "IFood.Produtos.Application";

                c.DefaultModelsExpandDepth(-1);

                c.InjectStylesheet("/css/custom-swagger-ui.css");
            });
        }
    }
}
