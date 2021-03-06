﻿using System;

namespace IFood.Produtos.Domain.Dto
{
    /// <summary>
    /// Propriedades da Alteração do Produto
    /// </summary>
    public class AlteracaoProdutoRequestDto : InclusaoProdutoRequestDto
    {
        /// <summary>
        /// Identificador do Produto
        /// </summary>
        public Guid IdProduto { get; set; }
    }
}
