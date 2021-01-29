using IFood.Produtos.Application.Infra.Data;
using IFood.Produtos.Domain.Dto;
using IFood.Produtos.Infra.CrossCutting.Common;
using IFood.Produtos.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace IFood.Produtos.Application.Controllers
{
    /// <summary>
    /// Controller de Operações do Produto
    /// </summary>
    [Route("api/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        /// <summary>
        /// Proxy do Serviço
        /// </summary>
        private readonly ProxyService _proxy;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="proxy"></param>
        public ProdutoController(ProxyService proxy)
        {
            _proxy = proxy;
        }

        /// <summary>
        /// Listagem de Produtos
        /// </summary>
        /// <param name="paginacao"></param>
        /// <returns></returns>
        [HttpGet]
        public object Get([FromQuery] Paging paginacao)
        {
            object response;

            try
            {
                var data = _proxy.Produto.Listar(
                    paginacao.PageIndex, paginacao.PageLimit, out var total, out _);

                response = paginacao.PageLimit.HasValue ?
                    new Response<IList<ListagemProdutoDto>>(paginacao.PageLimit.Value, total, data) as object :
                    new Response(data);
            }
            catch (Exception ex)
            {
                response = new Response(new Error(ex));
            }

            return response;
        }

        /// <summary>
        /// Detalha o Produto pelo Identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Response Get(Guid id)
        {
            Response response;

            try
            {
                response = new Response(_proxy.Produto.Detalhar(id));
            }
            catch (Exception ex)
            {
                response = new Response(new Error(ex));
            }

            return response;
        }

        /// <summary>
        /// Insere um Produto
        /// </summary>
        /// <param name="inclusao"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public Response Post([FromForm] InclusaoProdutoRequestDto inclusao, IFormFile file)
        {
            Response response;

            try
            {
                using var stream = file.OpenReadStream();
                response = new Response(_proxy.Produto.Adicionar(
                    new InclusaoProdutoDto
                    {
                        NomeArquivo = file.FileName,
                        Imagem = stream,
                        Descricao = inclusao.Descricao,
                        Valor = inclusao.Valor.ToDecimal()
                    })
                );
            }
            catch (Exception ex)
            {
                response = new Response(new Error(ex));
            }

            return response;
        }

        /// <summary>
        /// Altera dados do Produto
        /// </summary>
        /// <param name="alteracao"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPut]
        public Response Put([FromForm] AlteracaoProdutoRequestDto alteracao, IFormFile file)
        {
            Response response;

            try
            {
                using var stream = file?.OpenReadStream();
                response = new Response(_proxy.Produto.Alterar(
                    new AlteracaoProdutoDto
                    {
                        IdProduto = alteracao.IdProduto,
                        NomeArquivo = file?.FileName,
                        Imagem = stream,
                        Descricao = alteracao.Descricao,
                        Valor = alteracao.Valor.ToDecimal()
                    })
                );
            }
            catch (Exception ex)
            {
                response = new Response(new Error(ex));
            }

            return response;
        }

        /// <summary>
        /// Deleta um Produto pelo Identificador
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public Response Delete([FromQuery] params Guid[] ids)
        {
            Response response;

            try
            {
                response = new Response(_proxy.Produto.Excluir(ids));
            }
            catch (Exception ex)
            {
                response = new Response(new Error(ex));
            }

            return response;
        }
    }
}
