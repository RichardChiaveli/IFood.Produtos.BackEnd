using IFood.Produtos.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace IFood.Produtos.Infra.Data.Context
{
    /// <summary>
    /// Contexto do Repositório
    /// </summary>
    public class IFoodContext : DbContext
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="options"></param>
        public IFoodContext(DbContextOptions<IFoodContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RemovePluralizingTableNameConvention(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Operações do EF no Produto
        /// </summary>
        internal DbSet<Produto> Produtos { get; set; }

        /// <summary>
        /// Realizar uma Consulta no Banco via SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="storedProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<DataRow> ListarSql(string sql, bool storedProcedure = false, params Tuple<string, DbType, object>[] parameters)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            DataTable dt;

            try
            {
                using (connection = (SqlConnection) Database.GetDbConnection())
                {
                    connection.Open();

                    using (command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = sql,
                        CommandTimeout = 30000
                    })
                    {
                        if (storedProcedure)
                        {
                            command.CommandType = CommandType.StoredProcedure;
                        }

                        if (parameters != null && parameters.Length > 0)
                        {
                            parameters.ToList().ForEach(i =>
                            {
                                var (parameter, type, value) = i;
                                command.Parameters.Add(new SqlParameter(parameter, type)).Value = value;
                            });
                        }

                        reader = command.ExecuteReader();
                        dt = new DataTable();
                        dt.Load(reader);
                    }
                }
            }
            finally
            {
                Dispose(reader);
                Dispose(command);
                Dispose(connection);
            }

            return (from DataRow row in dt.Rows select row).ToList();
        }

        /// <summary>
        /// Realiza uma Inclusão, Alteração ou Exclusão no Banco via SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="transaction"></param>
        /// <param name="storedProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool ExecutarSql(string sql, IDbTransaction transaction = null, bool storedProcedure = false, 
            params Tuple<string, DbType, object>[] parameters)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            var rows = 0;

            try
            {
                using (connection = (SqlConnection)Database.GetDbConnection())
                {
                    connection.Open();

                    using var beginTransaction = transaction ?? connection.BeginTransaction();
                    using (command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = sql,
                        CommandTimeout = 30000,
                        Transaction = (SqlTransaction)beginTransaction
                    })
                    {
                        if (storedProcedure)
                            command.CommandType = CommandType.StoredProcedure;

                        if (parameters != null && parameters.Length > 0)
                        {
                            parameters.ToList().ForEach(i =>
                            {
                                var (parameter, type, value) = i;
                                command.Parameters.Add(new SqlParameter(parameter, type)).Value = value;
                            });
                        }

                        try
                        {
                            rows = command.ExecuteNonQuery();
                            command.Transaction.Commit();
                        }
                        catch (Exception)
                        {
                            command.Transaction.Rollback();
                        }
                    }
                }
            }
            finally
            {
                Dispose(command);
                Dispose(transaction);
                Dispose(connection);
            }

            return rows > 0;
        }

        /// <summary>
        /// Limpa os Objetos da Memória
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objeto"></param>
        private static void Dispose<T>(T objeto)
        {
            switch (objeto)
            {
                case null:
                    return;
                case SqlConnection connection:
                    connection.Close();
                    connection.Dispose();
                    break;
                case SqlDataReader dataReader:
                    dataReader.Close();
                    dataReader.Dispose();
                    break;
                case SqlCommand command:
                    command.Dispose();
                    break;
                case SqlTransaction transaction:
                    transaction.Dispose();
                    break;
            }
        }

        public static void RemovePluralizingTableNameConvention(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.DisplayName());
            }
        }
    }
}
