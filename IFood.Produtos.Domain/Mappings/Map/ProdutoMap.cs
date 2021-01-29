using AutoMapper;
using IFood.Produtos.Domain.Dto;
using IFood.Produtos.Domain.Entities;
using IFood.Produtos.Infra.CrossCutting.Common;
using System;

namespace IFood.Produtos.Domain.Mappings.Map
{
    public static class ProdutoMap
    {
        public static void MapearProduto(this IProfileExpression mapper)
        {
            mapper.CreateMap<InclusaoProdutoDto, Produto>()
                .ForMember(o => o.IdProduto, o =>
                         o.MapFrom(i => Guid.NewGuid()))
                .ForMember(o => o.TipoImagem, o =>
                         o.MapFrom(i => i.NomeArquivo.ToContentType()))
                .ForMember(o => o.Imagem, o =>
                    o.MapFrom(i => i.Imagem.ToByteArray()));

            mapper.CreateMap<AlteracaoProdutoDto, Produto>()
                .ForMember(o => o.TipoImagem, o =>
                    o.MapFrom(i => i.NomeArquivo.ToContentType()))
                .ForMember(o => o.Imagem, o =>
                    o.MapFrom(i => i.Imagem.ToByteArray()));

            mapper.CreateMap<Produto, ListagemProdutoDto>()
                .ForMember(o => o.Imagem, o =>
                     o.MapFrom(i => i.Imagem.ToDownloadBase64(i.TipoImagem, "UTF-8")));
        }
    }
}
