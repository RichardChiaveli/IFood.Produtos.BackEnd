using System;

namespace IFood.Produtos.Application.Infra.Data
{
    /// <summary>
    /// Classe de Reposta Padrão da API
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="error"></param>
        public Response(Error error)
        {
            Success = false;
            Message = error.Message;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="success"></param>
        public Response(bool success)
        {
            Success = success;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data"></param>
        public Response(object data) 
        {
            Data = data;
        }

        /// <summary>
        /// Indica se a Operação deu certo
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Mensagem de Erro, Alerta ou Aviso
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Dados a serem Retornados
        /// </summary>
        public object Data { get; set; }
    }
    
    /// <summary>
    /// Classe de Reposta Padrão da API
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class Response<TData>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="error"></param>
        public Response(Error error)
        {
            Success = false;
            Message = error.Message;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="pageLimit"></param>
        /// <param name="total"></param>
        /// <param name="data"></param>
        public Response(int pageLimit, int total, TData data)
        {
            Data = data;
            Total = total;
            PageCount = (int)Math.Ceiling(total / (double)pageLimit);
        }

        /// <summary>
        /// Indica se a Operação deu certo
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Mensagem de Erro, Alerta ou Aviso
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Quantidades de Páginas
        /// </summary>
        public int PageCount { get; set; } = 1;

        /// <summary>
        /// Total de Registros
        /// </summary>
        public int Total { get; set; } = 1;

        /// <summary>
        /// Dados a serem Retornados
        /// </summary>
        public TData Data { get; set; }
    }
}
