using Dapper;
using Oracle.ManagedDataAccess.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Common.Database.Conexion
{
    public class OracleDatabaseConnector //: IDatabaseConnector
    {
        private DatabaseManager databaseManager;
        private string databaseId;

        public OracleDatabaseConnector(DatabaseManager databaseManager, string databaseId)
        {
            this.databaseManager = databaseManager;
            this.databaseId = databaseId;
        }

        private void safeOpen(OracleConnection connection)
        {
            if (connection == null)
            {
                Log.Logger.Debug("connection is null. Reconfiguring");
                connection = (OracleConnection)this.databaseManager.GetNewOracleConnection(databaseId);
            }
            if (connection.State == ConnectionState.Closed)
            {
                Log.Logger.Debug("connection is closed. Opening");
                connection.Open();
            }
        }

        private void safeClose(OracleConnection connection)
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
            OracleCommand cmd = null;
            OracleDataReader rdr = null;
            OracleConnection connection = null;
            try
            {
                connection = (OracleConnection)this.databaseManager.GetNewOracleConnection(databaseId);
                cmd = connection.CreateCommand();
                safeOpen(connection);
                cmd.CommandText = "SELECT 1 FROM DUAL";
                decimal result = (Decimal)cmd.ExecuteScalar();
                databaseMetrics.Add("stabilizedConnection", (((int)result) == 1));
                after = (long)(DateTime.UtcNow).Millisecond;
            }
            catch (OracleException ex)
            {
                string errorMessage = "Code: " + ex.Number + "\n" + "Message: " + ex.Message;
                databaseMetrics.Add("stabilizedConnection", false);
                after = (long)(DateTime.UtcNow).Millisecond;
                Log.Logger.Error(ex, "An exception occurred. Please contact your system administrator.");
                databaseMetrics.Add("codeOracleError", ex.Number);

            }
            catch (Exception ex)
            {
                databaseMetrics.Add("stabilizedConnection", false);
                after = (long)(DateTime.UtcNow).Millisecond;
                Log.Logger.Error(ex, "Failed to validate connection using: select 1 from dual");
            }
            finally
            {
                safeClose(connection);
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (rdr != null)
                {
                    rdr.Dispose();
                }
            }
            databaseMetrics.Add("responseTime", (after - before) / 1000);
            return databaseMetrics;
        }

        public List<T> Query<T>(string query, object parameters = null, bool isProc = true)
        {
            OracleConnection connection = null;
            try
            {
                connection = (OracleConnection)this.databaseManager.GetNewOracleConnection(databaseId);
                safeOpen(connection);

                var collection = isProc
                    ? connection.Query<T>(query, parameters, commandType: CommandType.StoredProcedure).ToList()
                    : connection.Query<T>(query, parameters).ToList();

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

        public List<Dictionary<string, object>> SimpleQuery(String table)
        {
            var regexItem = new Regex("^[a-zA-Z0-9_\\.]*$");

            if (!regexItem.IsMatch(table))
            {
                throw new Exception("Table name has not allowed characters:" + table);
            }

            var rows = new List<Dictionary<string, object>>();
            OracleConnection connection = null;
            try
            {
                connection = (OracleConnection)this.databaseManager.GetNewOracleConnection(databaseId);
                OracleCommand cmd = connection.CreateCommand();
                safeOpen(connection);
                cmd.CommandText = "SELECT * FROM " + table;
                OracleDataReader rdr = cmd.ExecuteReader();
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

        public string Query(string query, object parameters = null, bool isProc = true)//Ejecuta stored o sentencias que no devuelven un resultado
        {

            OracleConnection connection = null;
            try
            {
                connection = (OracleConnection)this.databaseManager.GetNewOracleConnection(databaseId);
                safeOpen(connection);
                if (isProc)
                {
                    connection.Query(query, parameters, commandType: CommandType.StoredProcedure);
                }
                else
                {
                    connection.Query(query, parameters);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to execute query:" + query, e);
            }
            finally
            {
                safeClose(connection);
            }
            return "OK";
        }

        public List<T> Function<T>(string query, object parameters = null, bool isProc = true)
        {
            OracleConnection connection = null;
            try
            {
                connection = (OracleConnection)this.databaseManager.GetNewOracleConnection(databaseId);
                safeOpen(connection);

                var collection = isProc
                    ? connection.Query<T>(query, parameters, commandType: CommandType.Text).ToList()
                    : connection.Query<T>(query, parameters).ToList();
                return collection;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to execute function:" + query, e);
            }
            finally
            {
                safeClose(connection);
            }
        }

        public T FirstOrDefault<T>(string query, object parameters = null, bool isProc = true)
        {
            OracleConnection connection = null;
            try
            {
                connection = (OracleConnection)this.databaseManager.GetNewOracleConnection(databaseId);
                safeOpen(connection);

                var entity = isProc
                    ? connection.QueryFirstOrDefault<T>(query, parameters, commandType: CommandType.StoredProcedure)
                    : connection.QueryFirstOrDefault<T>(query, parameters);

                return entity;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to execute query with one result:" + query, e);
            }
            finally
            {
                safeClose(connection);
            }
        }

        public void FirstObjectSecondList<TFirst, TSecond>(string query, ref TFirst param1, ref List<TSecond> param2, object parameters = null, bool isProc = true)
        {
            OracleConnection connection = null;
            try
            {
                connection = (OracleConnection)this.databaseManager.GetNewOracleConnection(databaseId);
                safeOpen(connection);

                var collection = isProc
                    ? connection.QueryMultiple(query, parameters, commandType: CommandType.StoredProcedure)
                    : connection.QueryMultiple(query, parameters);

                param1 = collection.Read<TFirst>().First();
                param2 = collection.Read<TSecond>().ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to execute query with one result:" + query, e);
            }
            finally
            {
                safeClose(connection);
            }
        }

        public int Execute(string query, object parameters = null, bool isProc = true)
        {
            OracleConnection connection = null;
            try
            {
                connection = (OracleConnection)this.databaseManager.GetNewOracleConnection(databaseId);
                safeOpen(connection);

                var affectedRows = isProc
                    ? connection.Execute(query, parameters, commandType: CommandType.StoredProcedure)
                    : connection.Execute(query, parameters);

                return affectedRows;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to execute query :" + query, e);
            }
            finally
            {
                safeClose(connection);
            }
        }

        public DataTable ExecuteDT(string query, List<OracleParameter> parameters, bool isProc = true)
        {
            OracleConnection connection = null;
            try
            {
                connection = (OracleConnection)this.databaseManager.GetNewOracleConnection(databaseId);
                safeOpen(connection);

                OracleDataAdapter da = new OracleDataAdapter();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = connection;

                cmd.CommandType = isProc ? CommandType.StoredProcedure : CommandType.Text;
                cmd.InitialLONGFetchSize = 1000;
                cmd.CommandText = query;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (OracleParameter parm in parameters)
                {
                    cmd.Parameters.Add(parm);
                }
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to execute DT :" + query, e);
            }
            finally
            {
                safeClose(connection);
            }
        }

        public void ExecuteVoid(string query, object parameters = null, bool isProc = true)
        {
            OracleConnection connection = null;
            try
            {
                connection = (OracleConnection)this.databaseManager.GetNewOracleConnection(databaseId);
                safeOpen(connection);

                var affectedRows = isProc
                     ? connection.Execute(query, parameters, commandType: CommandType.StoredProcedure)
                     : connection.Execute(query, parameters);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to execute void query :" + query, e);
            }
            finally
            {
                safeClose(connection);
            }
        }

        public int QueryRowAffected(string query, object parameters = null, bool isProc = true)
        {
            OracleConnection connection = null;
            try
            {
                connection = (OracleConnection)this.databaseManager.GetNewOracleConnection(databaseId);
                safeOpen(connection);
                int affectedRows = 0;
                if (isProc)
                {
                    affectedRows = connection.Query<int>(query, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                }
                else
                {
                    affectedRows = connection.Query<int>(query, parameters).SingleOrDefault();
                }

                return affectedRows;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to execute query :" + query, e);
            }
            finally
            {
                safeClose(connection);
            }
        }
        //YR
        public List<Dictionary<string, object>> AdvancedQuery(string cadena)
        {
            var rows = new List<Dictionary<string, object>>();
            return rows;
        }
    }
}
