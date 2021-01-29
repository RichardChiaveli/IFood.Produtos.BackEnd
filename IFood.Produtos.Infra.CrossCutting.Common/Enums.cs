using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace IFood.Produtos.Infra.CrossCutting.Common
{
    /// <summary>
    /// Classe de Operações com o Enum
    /// </summary>
    public static class Enums
    {
        /// <summary>
        /// Obtêm a Descrição do Enum
        /// </summary>
        /// <param name="enumerator"></param>
        /// <returns></returns>
        public static string GetDescription(this System.Enum enumerator)
        {
            var fi = enumerator.GetType().GetField(enumerator.ToString());

            if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
            {
                return attributes.FirstOrDefault()?.Description;
            }

            return enumerator.ToString();
        }

        /// <summary>
        /// Obtêm uma Lista do Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> ToList<T>()
        {
            return System.Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        /// <summary>
        /// Obtêm uma Lista do Enum pegando a propriedade Description
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> ToDescriptionList<T>()
        {
            var attributes =
                typeof(T).GetMembers().SelectMany(i =>
                    i.GetCustomAttributes(typeof(DescriptionAttribute), true).Cast<DescriptionAttribute>());

            return attributes.Select(x => x.Description);
        }

        /// <summary>
        /// Valida a Range do Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumInt"></param>
        /// <returns></returns>
        public static bool IsValid<T>(this int enumInt)
        {
            return System.Enum.IsDefined(typeof(T), enumInt);
        }
    }
}
