using System.Collections.Generic;
using System.Linq;

namespace IFood.Produtos.Infra.CrossCutting.Common
{
    /// <summary>
    /// Classe para Paginação de Listas no LINQ
    /// </summary>
    public static class Pagination
    {
        /// <summary>
        /// Realiza a paginação de uma consulta e retorna as variavéis de paginação
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalPages"></param>
        /// <param name="totalItems"></param>
        /// <returns></returns>
        public static IEnumerable<T> PagedIndex<T>(this IQueryable<T> query, int pageIndex, int pageSize,
            out int totalPages, out int totalItems)
        {
            totalItems = query.Count();

            totalPages = totalItems / pageSize;
            if (totalItems % pageSize != 0)
            {
                totalPages++;
            }
            
            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    }
}
