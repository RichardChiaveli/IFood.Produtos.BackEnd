using FluentValidation;
using System.Collections.Generic;

namespace IFood.Produtos.Domain.Entities.Base
{
    /// <summary>
    /// Classe Abstrata que define as propriedades básicas de uma Entidade
    /// </summary>
    public abstract class BaseEntity<TEntity>
    {
        /// <summary>
        /// Método de Validação da Entidade (Definição de Válido e Inválido)
        /// </summary>
        /// <param name="validator">Validador da Classe</param>
        /// <param name="errors">Erros de Validação encontrados</param>
        /// <returns></returns>
        public abstract bool IsValid(AbstractValidator<TEntity> validator, out List<string> errors);
    }
}
