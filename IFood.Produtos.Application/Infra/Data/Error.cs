using System;

namespace IFood.Produtos.Application.Infra.Data
{
    /// <summary>
    /// Classe de Erro
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public Error()
        {
            Message = "Ops! Algo deu errado...";
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="ex"></param>
        public Error(Exception ex)
        {
            Message = ex.Message;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="message"></param>
        public Error(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public string Message { get; set; }
    }
}
