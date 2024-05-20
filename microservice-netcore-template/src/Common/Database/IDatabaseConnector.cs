using System;
using System.Collections.Generic;

namespace Common.Database
{
    public interface IDatabaseConnector
    {
        public List<T> Query<T>(string query, object parameters = null, bool isProc = false); //true
        Dictionary<string, object> ConnectionValidationQuery();

        public List<Dictionary<string, object>> SimpleQuery(String table);

        //public string Query(string query, object parameters = null, bool isProc = true);

        //public List<T> Function<T>(string query, object parameters = null, bool isProc = true);
        //public T FirstOrDefault<T>(string query, object parameters = null, bool isProc = true);

        //public void FirstObjectSecondList<TFirst, TSecond>(string query, ref TFirst param1, ref List<TSecond> param2, object parameters = null, bool isProc = true);        

        //public int Execute(string query, object parameters = null, bool isProc = true);

        //public void ExecuteVoid(string query, object parameters = null, bool isProc = true);
        //public int QueryRowAffected(string query, object parameters = null, bool isProc = true);
        //YR new
        public List<Dictionary<string, object>> AdvancedQuery(string cadena);
        public int InsertData(string tableName, Dictionary<string, object> data);
        public int UpdateData(string tableName, Dictionary<string, object> data, string condition);
        int InsertDataId(string tableName, Dictionary<string, object> data);
    }
}
