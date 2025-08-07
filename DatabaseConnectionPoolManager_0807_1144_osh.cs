// 代码生成时间: 2025-08-07 11:44:33
using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace MauiApp
{
    public class DatabaseConnectionPoolManager
    {
        private readonly ConcurrentBag<DbConnection> _connections;
        private readonly SemaphoreSlim _semaphore;
        private readonly string _connectionString;
        private readonly int _maxConnections;
        private readonly Type _dbConnectionType;

        public DatabaseConnectionPoolManager(IConfiguration configuration)
        {
            // Retrieve the database connection string and maximum connections from the configuration
            _connectionString = configuration["ConnectionString"];
            _maxConnections = int.Parse(configuration["MaxConnections"]);

            // Initialize the concurrent bag and semaphore
            _connections = new ConcurrentBag<DbConnection>();
            _semaphore = new SemaphoreSlim(1, _maxConnections);

            // Determine the type of database connection to use
            _dbConnectionType = Type.GetType(configuration["DbType"]);
        }

        public DbConnection GetConnection()
        {
            DbConnection connection;
            if (_connections.TryTake(out connection))
            {
                // Connection is available in the pool, return it
                return connection;
            }
            else
            {
                // No connection available, create a new one
                return CreateNewConnection();
            }
        }

        public void ReturnConnection(DbConnection connection)
        {
            if (connection != null)
            {
                if (connection.State == ConnectionState.Open)
                {
                    // Return the connection to the pool
                    _connections.Add(connection);
                }
                else
                {
                    // If the connection is not open, dispose of it
                    connection.Dispose();
                }
            }
        }

        private DbConnection CreateNewConnection()
        {
            try
            {
                _semaphore.Wait();
                DbConnection newConnection = (DbConnection)Activator.CreateInstance(_dbConnectionType, _connectionString);
                newConnection.Open();
                return newConnection;
            }
            catch (Exception ex)
            {
                // Handle exceptions during connection creation
                Console.WriteLine($"Error creating a new connection: {ex.Message}");
                throw;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
