using IFood.Produtos.Infra.CrossCutting.Common;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IFood.Produtos.Application.Infra.DI
{
    /// <summary>
    /// Injeção de dependência do Swagger
    /// </summary>
    public static class SwaggerDi
    {
        /// <summary>
        /// Adicionar o Swagger na injeção de dependência
        /// </summary>
        /// <param name="services"></param>
        /// <param name="apiVersion"></param>
        public static void AddSwaggerDi(
            this IServiceCollection services, string apiVersion)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(apiVersion,
                    new OpenApiInfo
                    {
                        Title = "IFood.Produtos.Application",
                        Version = apiVersion,
                        Description = "Métodos e operações do IFood.Produtos.Application",
                        Contact = new OpenApiContact
                        {
                            Name = "Richard Cleyton Chiaveli",
                            Email = "richard.chiaveli@outlook.com.br",
                            Url = new Uri("https://richardchiaveli.github.io/")
                        }
                    });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "\"Authorization: {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                c.DocumentFilter<SwaggerAddEnumDescriptions>();

                c.OperationFilter<SwaggerAddHeaderParams>();

                var caminhoAplicacao = PlatformServices.Default.Application.ApplicationBasePath;
                var nomeAplicacao =
                    PlatformServices.Default.Application.ApplicationName;
                var caminhoXmlDoc =
                    Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                if (File.Exists(caminhoXmlDoc))
                {
                    c.IncludeXmlComments(caminhoXmlDoc);
                }
            });
        }

        /// <summary>
        /// Documentação de Enumeradores no Swagger
        /// </summary>
        public class SwaggerAddEnumDescriptions : IDocumentFilter
        {
            /// <summary>
            /// Aplicação da documentação do Enum
            /// </summary>
            /// <param name="swaggerDoc"></param>
            /// <param name="context"></param>
            public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
            {
                foreach (var (key, value) in swaggerDoc.Components.Schemas.Where(x => x.Value?.Enum?.Count > 0))
                {
                    var propertyEnums = value.Enum;
                    if (propertyEnums != null && propertyEnums.Count > 0)
                    {
                        value.Description += DescribeEnum(propertyEnums, key);
                    }
                }

                foreach (var pathItem in swaggerDoc.Paths.Values)
                {
                    DescribeEnumParameters(pathItem.Operations, swaggerDoc);
                }
            }

            private static void DescribeEnumParameters(IDictionary<OperationType, OpenApiOperation> operations, OpenApiDocument swaggerDoc)
            {
                if (operations == null) return;
                foreach (var oper in operations)
                {
                    foreach (var param in oper.Value.Parameters)
                    {
                        var (key, value) = swaggerDoc.Components.Schemas.FirstOrDefault(x => x.Key == param.Name);
                        if (value != null)
                        {
                            param.Description += DescribeEnum(value.Enum, key);
                        }
                    }
                }
            }

            private static Type GetEnumTypeByName(string enumTypeName)
            {
                return AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(x => x.GetTypes())
                    .FirstOrDefault(x => x.Name == enumTypeName);
            }

            private static string DescribeEnum(IEnumerable<IOpenApiAny> enums, string proprtyTypeName)
            {
                var enumDescriptions = new List<string>();
                var enumType = GetEnumTypeByName(proprtyTypeName);
                if (enumType == null)
                    return null;

                enumDescriptions.AddRange(from OpenApiInteger enumOption in enums
                                          let enumInt = enumOption.Value
                                          select $"{enumInt} = {((Enum)Enum.ToObject(enumType, enumInt)).GetDescription()}");

                return string.Join(", ", enumDescriptions.ToArray());
            }
        }

        /// <summary>
        /// Parâmetros Adicionais no Cabeçalho do Swagger
        /// </summary>
        public class SwaggerAddHeaderParams : IOperationFilter
        {
            /// <summary>
            /// Aplicação dos parâmetros adicionais
            /// </summary>
            /// <param name="operation"></param>
            /// <param name="context"></param>
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();
            }

            /// <summary>
            /// Remoção de Parâmetros do Cabeçalho para uma determinada Ação do Controller informado
            /// </summary>
            /// <param name="context"></param>
            /// <param name="ignore"></param>
            /// <returns></returns>
            public bool RemoveParametersFromActionName(OperationFilterContext context, params Tuple<string, string[]>[] ignore)
            {
                return !(context.ApiDescription.ActionDescriptor is ControllerActionDescriptor descriptor) ||
                       ignore != null && ignore.Any() &&
                       ignore.Any(i => descriptor.ControllerName.StartsWith(i.Item1)) &&
                       ignore.Any(i => i.Item2.Contains(descriptor.ActionName));
            }
        }
    }
}
