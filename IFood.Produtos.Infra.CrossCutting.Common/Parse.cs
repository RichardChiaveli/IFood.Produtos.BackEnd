using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Data;
using System.Globalization;
using System.IO;

namespace IFood.Produtos.Infra.CrossCutting.Common
{
    /// <summary>
    /// Classe que realiza a Conversão de um Tipo para Outro
    /// </summary>
    public static class Parse
    {
        /// <summary>
        /// Converte o Tipo do C# no Tipo de variavél no Banco de Dados
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DbType ToDbType(this Type type)
        {
            var typeCode = Type.GetTypeCode(type);

            return typeCode switch
            {
                TypeCode.Boolean => DbType.Boolean,
                TypeCode.Byte => DbType.Byte,
                TypeCode.Char => DbType.StringFixedLength,
                TypeCode.DateTime => DbType.DateTime,
                TypeCode.Decimal => DbType.Decimal,
                TypeCode.Double => DbType.Double,
                TypeCode.Int16 => DbType.Int16,
                TypeCode.Int32 => DbType.Int32,
                TypeCode.Int64 => DbType.Int64,
                TypeCode.SByte => DbType.SByte,
                TypeCode.Single => DbType.Single,
                TypeCode.String => DbType.String,
                TypeCode.UInt16 => DbType.UInt16,
                TypeCode.UInt32 => DbType.UInt32,
                TypeCode.UInt64 => DbType.UInt64,
                TypeCode.DBNull => DbType.Object,
                TypeCode.Empty => DbType.Object,
                TypeCode.Object => DbType.Object,
                _ => DbType.Object
            };
        }

        /// <summary>
        /// Converte Arquivo Stream para Byte Array
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this Stream stream)
        {
            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            return ms.ToArray();
        }

        /// <summary>
        /// Converte Arquivo Byte Array para Download em Base64
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string ToDownloadBase64(this byte[] data, string contentType, string charset = "UTF-8")
            => $"data:{contentType};charset={charset};base64,{System.Convert.ToBase64String(data)}";

        /// <summary>
        /// Obtêm o Tipo do Arquivo pelo nome do Arquivo
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string ToContentType(this string filename)
        {
            new FileExtensionContentTypeProvider().
                TryGetContentType(filename, out var contentType);

            return contentType;
        }

        /// <summary>
        /// Converte Dinheiro em String para Decimal
        /// </summary>
        /// <param name="value"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string value, string language = "pt-BR")
        {
            return System.Convert.ToDecimal(
                value.Replace(".", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator),
                new CultureInfo(language));
        }
    }
}
