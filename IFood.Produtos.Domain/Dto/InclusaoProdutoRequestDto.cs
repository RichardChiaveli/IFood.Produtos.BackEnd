namespace IFood.Produtos.Domain.Dto
{
    /// <summary>
    /// Propriedades da Inclusão do Produto
    /// </summary>
    public class InclusaoProdutoRequestDto
    {
        /// <summary>
        /// Descrição do Produto
        /// </summary>
        public string Descricao { get; set; }
        /// <summary>
        /// Valor do Produto
        /// </summary>
        public string Valor { get; set; }
    }
}
