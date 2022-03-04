using System;
using System.Data;
using System.Threading.Tasks;
using AdoNetLib.Configurations;
using Microsoft.Data.SqlClient;

namespace AdoNetLib
{
    public class MainConnector
    {
        private SqlConnection connection;

        public async Task<bool> ConnectAsync()
        {
            bool result;
            
            try
            {
                connection = new SqlConnection(ConnectionString.MsSqlConnection);
                await connection.OpenAsync();
                result = true;
            }
            catch
            {
                result = false;
            }
            
            return result;
        }

        public async void DisconnectAsync()
        {
            if (connection.State == ConnectionState.Open)
                await connection.CloseAsync();
        }

        public SqlConnection GetConnection()
        {
            if (connection.State == ConnectionState.Open)
                return connection;
            else
                throw new Exception("Подключение уже закрыто");
        }
    }
}