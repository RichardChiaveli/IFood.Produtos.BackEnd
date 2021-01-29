using IFood.Produtos.Domain.Entities;
using IFood.Produtos.Infra.CrossCutting.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace IFood.Produtos.Infra.Data.Repository
{
    /// <summary>
    /// Repositório de Produto
    /// </summary>
    public class ProdutoRepository
    {
        /// <summary>
        /// Contexto do Repositório
        /// </summary>
        public Context.IFoodContext Context { get; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        public ProdutoRepository(Context.IFoodContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Inclusão de Produtos
        /// </summary>
        /// <param name="produtos"></param>
        /// <returns></returns>
        public bool Adicionar(params Produto[] produtos)
        {
            Context.Produtos.AddRange(produtos);
            return Context.SaveChanges() > 0;
        }

        /// <summary>
        /// Alteração de Produtos
        /// </summary>
        /// <param name="produtos"></param>
        /// <returns></returns>
        public bool Alterar(params Produto[] produtos)
        {
            Context.Produtos.UpdateRange(produtos);
            return Context.SaveChanges() > 0;
        }

        /// <summary>
        /// Exclusão de Produtos
        /// </summary>
        /// <param name="produtos"></param>
        /// <returns></returns>
        public bool Excluir(params Produto[] produtos)
        {
            Context.Produtos.RemoveRange(produtos);
            return Context.SaveChanges() > 0;
        }

        /// <summary>
        /// Verifica se o Produto já Cadastrado
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        public bool VerificarDescricaoJaCadastrado(string descricao) =>
            Context.Produtos.AsNoTracking().Any(i =>
                i.Descricao.ToLower().Trim() == descricao.ToLower().Trim());

        /// <summary>
        /// Verifica se o Produto existe
        /// </summary>
        /// <param name="idProduto"></param>
        /// <returns></returns>
        public bool VerificarProdutoExiste(Guid idProduto) =>
            Context.Produtos.AsNoTracking().Any(i => i.IdProduto == idProduto);

        /// <summary>
        /// Detalhe do Produto
        /// </summary>
        /// <param name="idProduto"></param>
        /// <returns></returns>
        public Produto Detalhar(Guid idProduto) =>
            Context.Produtos.AsNoTracking().First(i => i.IdProduto == idProduto);

        /// <summary>
        /// Lista todos os Produtos por Filtros de forma paginada
        /// </summary>
        /// <param name="totalRegistros"></param>
        /// <param name="totalPaginas"></param>
        /// <param name="indice"></param>
        /// <param name="limite"></param>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public IList<Produto> ListarPaginado(out int totalRegistros, out int totalPaginas,
            int indice = 1, int? limite = null, Expression<Func<Produto, bool>> filtros = null)
        {
            totalPaginas = 0;
            totalRegistros = 0;

            var query = filtros.HasFilter() ? Context.Produtos.Where(filtros) :
                Context.Produtos.AsNoTracking().AsQueryable();

            return !limite.HasValue ? query.AsNoTracking().ToList() :
                query.PagedIndex(indice, limite.Value, out totalPaginas, out totalRegistros).ToList();
        }
    }
}
