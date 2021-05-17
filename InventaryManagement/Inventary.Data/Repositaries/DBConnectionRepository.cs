using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Inventary.Core.Domain.DB;
using Microsoft.Extensions.Options;

namespace Inventary.Data.Repositaries
{
    public class DBConnectionRepository : IDBConnectionRepository
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private readonly IOptions<DbConfiguration> _configs;

        public DBConnectionRepository(IOptions<DbConfiguration> Configs)
        {
            _configs = Configs;
        }

        public IDbConnection GetConnection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(_configs.Value.DbConnectionString);
                }
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                return _connection;
            }
        }
    }
    public interface IDBConnectionRepository
    {
        IDbConnection GetConnection { get; }
    }
}
