using System;
using FluentValidation;
using IFood.Produtos.Domain.Entities;

namespace IFood.Produtos.Domain.Validations.Validator
{
    /// <summary>
    /// Classe que define as regras de válida para a Entidade Produto
    /// </summary>
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(i => i.IdProduto)
                .NotEqual(Guid.Empty)
                .WithMessage("Identificador do Produto Inválido");

            RuleFor(i => i.Descricao)
                .NotEmpty()
                .NotNull()
                .WithMessage("Descrição do Produto Inválido");

            RuleFor(i => i.TipoImagem)
                .NotEmpty()
                .NotNull()
                .WithMessage("Tipo da Imagem do Produto Inválido");
            
            RuleFor(i => i.Imagem)
                .NotNull()
                .WithMessage("Imagem do Produto Inválido");

            RuleFor(i => i.Valor)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Valor do Produto Inválido");
        }
    }
}
