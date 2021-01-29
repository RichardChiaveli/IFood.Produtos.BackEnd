using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace IFood.Produtos.Domain.Validations
{
    /// <summary>
    /// Classe que implementa a validação da Entidade via FluentValidation
    /// </summary>
    public static class FluentValidator<T> where T : class
    {
        /// <summary>
        /// Validações da Classe
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="validator"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static bool Validate(T obj, AbstractValidator<T> validator, out List<string> errors)
        {
            var validationResult = validator.Validate(obj);
            var isValid = validationResult.IsValid;

            errors = isValid ? null :
                validationResult.Errors.Select(s => s.ErrorMessage).ToList();

            return isValid;
        }
    }
}
