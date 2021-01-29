using IFood.Produtos.Domain.Dto;
using IFood.Produtos.Domain.Entities;
using IFood.Produtos.Domain.Mappings;
using IFood.Produtos.Domain.Validations.Validator;
using IFood.Produtos.Infra.Data.Repository;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IFood.Produtos.Service.Services
{
    /// <summary>
    /// Serviço do Produto
    /// </summary>
    public class ProdutoService
    {
        private readonly ProdutoRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository"></param>
        public ProdutoService(ProdutoRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Inclusão de Produtos
        /// </summary>
        /// <param name="inclusao"></param>
        /// <returns></returns>
        public bool Adicionar(InclusaoProdutoDto inclusao)
        {
            if (inclusao is null)
                throw new Exception("Produto inválido!");

            var produto = AutoMapper<InclusaoProdutoDto>.CopyTo<Produto>(inclusao);

            if (!produto.IsValid(new ProdutoValidator(), out var errors))
                throw new Exception(string.Join(",", errors));

            var jaCadastrado = _repository.VerificarDescricaoJaCadastrado(produto.Descricao);

            if (jaCadastrado)
                throw new Exception("Produto já cadastrado!");

            return _repository.Adicionar(produto);
        }

        /// <summary>
        /// Exclusão de Produtos
        /// </summary>
        /// <param name="idProduto"></param>
        /// <returns></returns>
        public bool Excluir(params Guid[] idProduto)
        {
            var registros = _repository.ListarPaginado(out _, out _, 1, idProduto.Length,
                i => idProduto.Contains(i.IdProduto));

            if (!registros.Any())
                throw new Exception("Produtos não encontrados");

            return _repository.Excluir(registros.ToArray());
        }

        /// <summary>
        /// Alteração de Produtos
        /// </summary>
        /// <param name="alteracao"></param>
        /// <returns></returns>
        public bool Alterar(AlteracaoProdutoDto alteracao)
        {
            if (alteracao is null)
                throw new Exception("Produto inválido!");

            var produto = AutoMapper<AlteracaoProdutoDto>.CopyTo<Produto>(alteracao);

            var produtoBd = Obter(produto.IdProduto);

            if (!string.IsNullOrWhiteSpace(produto.Descricao))
                produtoBd.Descricao = produto.Descricao.Trim();

            if (produto.Valor > 0)
                produtoBd.Valor = produto.Valor;

            if (produto.Imagem != null && produto.Imagem.Length > 0)
                produtoBd.Imagem = produto.Imagem;

            if (!string.IsNullOrWhiteSpace(produto.TipoImagem))
                produtoBd.TipoImagem = produto.TipoImagem;
            
            if (!produtoBd.IsValid(new ProdutoValidator(), out var errors))
                throw new Exception(string.Join(",", errors));

            if (_repository.VerificarProdutoExiste(produto.IdProduto))
                return _repository.Alterar(produtoBd);

            throw new Exception("Produto não encontrado!");
        }

        /// <summary>
        /// Listagem de Produtos
        /// </summary>
        /// <param name="indice"></param>
        /// <param name="limite"></param>
        /// <param name="totalRegistros"></param>
        /// <param name="totalPaginas"></param>
        /// <param name="descricao"></param>
        /// <returns></returns>
        public IList<ListagemProdutoDto> Listar(int indice, int? limite, out int totalRegistros, out int totalPaginas, string descricao = null)
        {
            var filtros = PredicateBuilder.New<Produto>();

            if (!string.IsNullOrWhiteSpace(descricao))
                filtros.And(i => i.Descricao.ToLower().Trim().Contains(descricao.ToLower().Trim()));

            var produtos =
                _repository.ListarPaginado(out totalRegistros, out totalPaginas, indice, limite, filtros);

            var retorno = AutoMapper<IList<Produto>>.CopyTo<IList<ListagemProdutoDto>>(produtos);

            return retorno;
        }

        /// <summary>
        /// Detalhe de um Produto
        /// </summary>
        /// <param name="idProduto"></param>
        /// <returns></returns>
        public ListagemProdutoDto Detalhar(Guid idProduto)
        {
            var produto = Obter(idProduto);
            var retorno = AutoMapper<Produto>.CopyTo<ListagemProdutoDto>(produto);

            return retorno;
        }

        /// <summary>
        /// Obtêm um Produto pelo Identificador
        /// </summary>
        /// <param name="idProduto"></param>
        /// <returns></returns>
        private Produto Obter(Guid idProduto)
        {
            if (idProduto == Guid.Empty)
                throw new Exception("Produto inválido!");

            if (!_repository.VerificarProdutoExiste(idProduto))
                throw new Exception("Produto não encontrado!");

            return _repository.Detalhar(idProduto);
        }
    }
}
