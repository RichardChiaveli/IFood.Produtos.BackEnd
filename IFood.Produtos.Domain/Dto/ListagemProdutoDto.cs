using System;

namespace IFood.Produtos.Domain.Dto
{
    /// <summary>
    /// Propriedades da Listagem do Produto
    /// </summary>
    public class ListagemProdutoDto
    {
        /// <summary>
        /// Identificador do Produto
        /// </summary>
        public Guid IdProduto { get; set; }
        /// <summary>
        /// Descrição do Produto
        /// </summary>
        public string Descricao { get; set; }
        /// <summary>
        /// Imagem do Produto em Base64
        /// </summary>
        public string Imagem { get; set; }
        /// <summary>
        /// Valor do Produto
        /// </summary>
        public decimal Valor { get; set; }
    }
}
