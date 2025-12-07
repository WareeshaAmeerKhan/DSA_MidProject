using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace SocialMediaFeedSystem
{
    internal class DatabaseHelper
    {
        private String serverName = "127.0.0.1";
        private String port = "3306";
        private String databaseName = "temp";
        private String databaseUser = "root";
        private String databasePassword = "1234567890-=1234567890-=";
        private DatabaseHelper() { }
        private static DatabaseHelper _instance;
        public static DatabaseHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DatabaseHelper();
                return _instance;
            }
        }
        public MySqlConnection getConnection()
        {
            string connectionString = $"server={serverName};port={port};user={databaseUser};database ={databaseName}; password ={databasePassword}; SslMode = Required; ";
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        public MySqlDataReader getData(string query)
        {
            using (var connection = getConnection())
            {
                using (var command = new MySqlCommand(query, getConnection()))
                {
                    return command.ExecuteReader();
                }
            }
        }
        public int Update(string query)
        {
            using (var connection = getConnection())
            {
                using (var command = new MySqlCommand(query, getConnection()))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetDataTable(string query)
        {
            using (var connection = getConnection())
            {
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public (MySqlConnection, MySqlTransaction) BeginTransaction()
        {
            var connection = getConnection();
            var transaction = connection.BeginTransaction();
            return (connection, transaction);
        }

        public int ExecuteNonQueryInTransaction(string query, Dictionary<string, object> parameters, MySqlConnection connection, MySqlTransaction transaction)
        {
            using (var command = new MySqlCommand(query, connection, transaction))
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
                return command.ExecuteNonQuery();
            }
        }

        public int ExecuteNonQuery(string query)
        {
            string connectionString = $"server={serverName};port={port};user={databaseUser};database ={databaseName}; password ={databasePassword}; SslMode = Required; ";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }

    }
}