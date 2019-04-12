using System;
using System.Data.SqlClient;

namespace HelperLibrary
{
    public class ADOReader : IDisposable
    {
        private SqlDataReader dataReader = null;

        public SqlDataReader Data => dataReader;

        public bool HasRows => dataReader.HasRows;

        public object this[string index] => dataReader[index];

        public bool IsDBNull(int index)
        {
            return dataReader.IsDBNull(index);
        }

        public int GetOrdinal(string indexName)
        {
            return dataReader.GetOrdinal(indexName);
        }

        public string GetFieldNullOrString(string fieldName)
        {
            if (dataReader.IsDBNull(dataReader.GetOrdinal(fieldName)))
            {
                return null;
            }

            return dataReader[fieldName].ToString();
        }

        public void Close()
        {
            dataReader.Close();
            dataReader.Dispose();
            dataReader = null;
        }

        public ADOReader(SqlCommand sqlCommand)
        {
            dataReader = sqlCommand.ExecuteReader();
        }

        public bool Read()
        {
            return dataReader.Read();
        }

        public void Dispose()
        {
            Dispose(true);
        }

         ~ADOReader()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (dataReader == null) return;

            dataReader.Close();
            dataReader.Dispose();
            dataReader = null;
        }
    }
}
