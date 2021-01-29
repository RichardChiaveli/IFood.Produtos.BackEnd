namespace IFood.Produtos.Application.Infra.Data
{
    /// <summary>
    /// Classe de Paginação
    /// </summary>
    public class Paging
    {
        /// <summary>
        /// Indíce da Página
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// Limite por Página
        /// </summary>
        public int? PageLimit { get; set; } = null;
    }
}
