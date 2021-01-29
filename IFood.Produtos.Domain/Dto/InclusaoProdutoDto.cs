using System.IO;

namespace IFood.Produtos.Domain.Dto
{
    /// <summary>
    /// Propriedades da Inclusão do Produto
    /// </summary>
    public class InclusaoProdutoDto
    {
        /// <summary>
        /// Nome do Arquivo
        /// </summary>
        public string NomeArquivo { get; set; }
        /// <summary>
        /// Imagem do Produto
        /// </summary>
        public Stream Imagem { get; set; }

        /// <summary>
        /// Descrição do Produto
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Valor do Produto
        /// </summary>
        public decimal Valor { get; set; }
    }
}
