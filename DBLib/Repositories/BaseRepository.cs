using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DBLib.Repositories
{
    public class BaseRepository<T>
    {
        //protected SqliteConnection _sqlConnection { get; set; }
        protected SqlConnection _sqlConnection { get; set; }

        public static string ConnectionString { get; set; } = string.Empty;

        public BaseRepository()
        {
            _sqlConnection = new SqlConnection(ConnectionString);
        }

        public BaseRepository(string connectionString)
        {
            //_sqlConnection = new SqliteConnection(connectionString);
            _sqlConnection = new SqlConnection(connectionString);
        }

        public int Insert(T parameter)
        {
            string[] parameterArray = typeof(T).GetProperties().Select(p => p.Name).ToArray();
            string parameterString = string.Join(",", parameterArray);
            string query =
                $"INSERT INTO {typeof(T).Name} ({parameterString}) VALUES ({string.Join(",", parameterArray.Select(n => "@" + n).ToArray())})";
            return _sqlConnection.Execute(query, parameter);
        }

        public List<T> Query(string col = "*")
        {
            if (col.Trim() != "*")
            {
                col = col.Replace("where", string.Empty);
                col = col.Replace("drop", string.Empty);
                var columns = col.Split(',').Select(c => c.Trim());
                col = string.Join(",", columns);
            }

            string query = $"SELECT {col} FROM {typeof(T).Name}";
            return _sqlConnection.Query<T>(query).AsList();
        }

        public List<T> QueryBy(Dictionary<string, object> parameters)
        {
            SqlBuilder builder = new SqlBuilder();
            string query = $"SELECT * FROM {typeof(T).Name} /**where**/ ";
            SqlBuilder.Template template = builder.AddTemplate(query);

            DynamicParameters dynamicParams = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                builder.Where($"{parameter.Key} = @{parameter.Key}");
                dynamicParams.Add(parameter.Key, parameter.Value);
            }
            string rawSql = template.RawSql;

            return _sqlConnection.Query<T>(rawSql, dynamicParams).AsList();
        }
    }
}
