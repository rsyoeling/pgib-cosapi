using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using Dapper;
using Serilog;

namespace Common.Database.Conexion
{
    public class SqlDatabaseConnector : IDatabaseConnector
    {
        private DatabaseManager databaseManager;
        private string databaseId;

        public SqlDatabaseConnector(DatabaseManager databaseManager, string databaseId)
        {
            this.databaseManager = databaseManager;
            this.databaseId = databaseId;
        }
        private void safeOpen(SqlConnection connection)
        {
            if (connection == null)
            {
                Log.Logger.Debug("connection is null. Reconfiguring");
                connection = (SqlConnection)this.databaseManager.GetNewSqlConnection(databaseId);
            }
            if (connection.State == ConnectionState.Closed)
            {
                Log.Logger.Debug("connection is closed. Opening");
                connection.Open();
            }
        }

        private void safeClose(SqlConnection connection)
        {
            if (connection == null)
            {
                Log.Logger.Debug("connection is null. Close will not be executed");
            }
            else
            {
                Log.Logger.Debug("current state before its close: " + connection.State);
                connection.Close();
            }
        }

        public Dictionary<string, object> ConnectionValidationQuery()
        {
            Dictionary<string, object> databaseMetrics = new Dictionary<string, object>();
            long before = (long)(DateTime.UtcNow).Millisecond;
            long after = 0;
            SqlCommand cmd = null;
            SqlConnection connection = null;
            try
            {
                connection = (SqlConnection)this.databaseManager.GetNewSqlConnection(databaseId);
                cmd = connection.CreateCommand();
                safeOpen(connection);
                cmd.CommandText = "SELECT 1";
                int result = (int)cmd.ExecuteScalar();
                databaseMetrics.Add("stabilizedConnection", (result == 1));
                after = (long)(DateTime.UtcNow).Millisecond;
            }
            catch (SqlException ex)
            {
                string errorMessage = "Code: " + ex.Number + "\n" + "Message: " + ex.Message;
                databaseMetrics.Add("stabilizedConnection", false);
                after = (long)(DateTime.UtcNow).Millisecond;
                Log.Logger.Error(ex, "An exception occurred. Please contact your system administrator.");
                databaseMetrics.Add("codeSqlError", ex.Number);

            }
            catch (Exception ex)
            {
                databaseMetrics.Add("stabilizedConnection", false);
                after = (long)(DateTime.UtcNow).Millisecond;
                Log.Logger.Error(ex, "Failed to validate connection using: SELECT 1");
            }
            finally
            {
                safeClose(connection);
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            databaseMetrics.Add("responseTime", (after - before) / 1000);
            return databaseMetrics;
        }


        public List<T> Query<T>(string query, object parameters, bool isProc = false) //true
        {
            SqlConnection connection = null;
            try
            {
                connection = (SqlConnection)this.databaseManager.GetNewSqlConnection(databaseId);
                safeOpen(connection);
                
                var dynamicParameters = new DynamicParameters(parameters);

                var collection = isProc
                    ? connection.Query<T>(query, parameters, commandType: CommandType.StoredProcedure).ToList()
                    : connection.Query<T>(query, dynamicParameters, commandType: CommandType.Text).ToList();
                    
                return collection;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to execute query:" + query, e);
            }
            finally
            {
                safeClose(connection);
            }
        }

        public List<Dictionary<string, object>> SimpleQuery(string table)
        {
            var regexItem = new Regex("^[a-zA-Z0-9_\\.]*$");

            if (!regexItem.IsMatch(table))
            {
                throw new Exception("Table name has not allowed characters:" + table);
            }

            var rows = new List<Dictionary<string, object>>();
            SqlConnection connection = null;
            try
            {
                connection = (SqlConnection)this.databaseManager.GetNewSqlConnection(databaseId);
                SqlCommand cmd = connection.CreateCommand();
                safeOpen(connection);
                cmd.CommandText = "SELECT * FROM " + table;
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        row.Add(rdr.GetName(i), rdr.GetValue(i));
                    }
                    rows.Add(row);
                }

                return rows;
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to fetch data from table:" + table, exception);
            }
            finally
            {
                safeClose(connection);
            }
        }

        public List<Dictionary<string, object>> AdvancedQuery(string cadena)
        {
            var rows = new List<Dictionary<string, object>>();
            SqlConnection connection = null;
            try
            {
                connection = (SqlConnection)this.databaseManager.GetNewSqlConnection(databaseId);
                SqlCommand cmd = connection.CreateCommand();
                safeOpen(connection);
                cmd.CommandText = cadena;
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        row.Add(rdr.GetName(i), rdr.GetValue(i));
                    }
                    rows.Add(row);
                }

                return rows;
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to fetch data from query:", exception);
            }
            finally
            {
                safeClose(connection);
            }
        }

        public int InsertData(string tableName, Dictionary<string, object> data)
        {
            using (var connection = (SqlConnection)this.databaseManager.GetNewSqlConnection(databaseId))
            {
                try
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        // Construir la consulta de inserción
                        string columns = string.Join(",", data.Keys);
                        string values = string.Join(",", data.Values.Select(value =>  value.ToString())); //"@" +

                        cmd.CommandText = $"INSERT INTO {tableName} ({columns}) VALUES ({values});";

                        // Agregar los parámetros
                        foreach (var entry in data)
                        {
                            cmd.Parameters.AddWithValue("@" + entry.Key, entry.Value);
                        }

                        // Abrir la conexión y ejecutar la consulta
                        safeOpen(connection);
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception("Failed to insert data:", exception);
                }
            }
        }
        public int UpdateData(string tableName, Dictionary<string, object> data, string condition)
        {
            using (var connection = (SqlConnection)this.databaseManager.GetNewSqlConnection(databaseId))
            {
                try
                {
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        // Construir la consulta de actualización
                        string setClause = string.Join(",", data.Select(kv => $"{kv.Key} = @{kv.Key}"));
                        cmd.CommandText = $"UPDATE {tableName} SET {setClause} WHERE {condition};";

                        // Agregar los parámetros
                        foreach (var entry in data)
                        {
                            cmd.Parameters.AddWithValue("@" + entry.Key, entry.Value);
                        }

                        // Abrir la conexión y ejecutar la consulta
                        safeOpen(connection);
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception("Failed to update data:", exception);
                }
            }
        }
        public int InsertDataId(string tableName, Dictionary<string, object> data)
        {
            using (var connection = (SqlConnection)this.databaseManager.GetNewSqlConnection(databaseId))
            {
                if (connection == null)
            {
                throw new Exception("Failed to get a valid database connection.");
            }

            try
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    // Construir la consulta de inserción
                    string columns = string.Join(",", data.Keys);
                    string values = string.Join(",", data.Values.Select(value => value.ToString()));

                    cmd.CommandText = $"INSERT INTO {tableName} ({columns}) VALUES ({values}); SELECT SCOPE_IDENTITY();";

                    // Agregar los parámetros
                    foreach (var entry in data)
                    {
                        cmd.Parameters.AddWithValue("@" + entry.Key, entry.Value);
                    }

                    // Abrir la conexión y ejecutar la consulta
                    safeOpen(connection);
                    return Convert.ToInt32(cmd.ExecuteScalar()); // Devuelve el ID generado
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to insert data:", exception);
            }
            }
        }
    }
}
