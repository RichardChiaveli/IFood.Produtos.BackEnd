using System;
using System.Linq.Expressions;

namespace IFood.Produtos.Infra.CrossCutting.Common
{
    /// <summary>
    /// Classe para validação de expressões e predicados do LINQ
    /// </summary>
    public static class ExpressionValidator
    {
        /// <summary>
        /// Valida se algum argumento foi passada no predicato
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool HasFilter<T>(this Expression<Func<T, bool>> predicate) =>
            predicate != null && !predicate.Parameters[0].Name.Equals("f") &&
            !predicate.ToString().Equals("f => False");

        /// <summary>
        /// Valida se alguma expressão foi passada na expressão
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public static bool HasProperties<T>(this Expression<Func<T, object>>[] includeProperties) =>
            includeProperties != null && includeProperties.Length > 0;
    }
}
