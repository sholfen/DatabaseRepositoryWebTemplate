﻿using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBLib.Repositories
{
    public class BaseRepository<T>
    {
        protected SqliteConnection _sqlConnection { get; set; }
        //protected System.Data.SqlClient.SqlConnection sqlConnection { get; set; }

        public BaseRepository(string connectionString)
        {
            _sqlConnection = new SqliteConnection(connectionString);
            _sqlConnection.Open();
        }

        public int Insert(T parameter)
        {
            string[] parameterArray = typeof(T).GetProperties().Select(p => p.Name).ToArray();
            string parameterString = string.Join(",", parameterArray);
            string query =
                $"INSERT INTO {typeof(T).Name} ({parameterString}) VALUES ({string.Join(",", parameterArray.Select(n => "@" + n).ToArray())})";
            return _sqlConnection.Execute(query, parameter);
        }

        public List<T> Query()
        {
            string query = $"SELECT * FROM {typeof(T).Name}";
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