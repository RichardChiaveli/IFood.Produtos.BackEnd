using FluentValidation;
using IFood.Produtos.Domain.Entities.Base;
using IFood.Produtos.Domain.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IFood.Produtos.Domain.Entities
{
    /// <summary>
    /// Classe representando a Entidade Produto
    /// </summary>
    public class Produto : BaseEntity<Produto>
    {
        /// <summary>
        /// Identificador do Produto
        /// </summary>
        [Key]
        public Guid IdProduto { get; set; }
        /// <summary>
        /// Descrição do Produto
        /// </summary>
        [Required]
        [StringLength(250)]
        public string Descricao { get; set; }
        /// <summary>
        /// Imagem do Produto
        /// </summary>
        public byte[] Imagem { get; set; }
        /// <summary>
        /// Tipo da Imagem (Content-Type)
        /// </summary>
        [Required]
        [StringLength(50)]
        public string TipoImagem { get; set; }
        /// <summary>
        /// Valor do Produto
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }

        public override bool IsValid(AbstractValidator<Produto> validator, out List<string> errors)
            => FluentValidator<Produto>.Validate(this, validator, out errors);
    }
}
