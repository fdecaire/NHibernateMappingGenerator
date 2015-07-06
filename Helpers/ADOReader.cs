using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Helpers
{
    public class ADOReader : IDisposable
    {
        private SqlDataReader dataReader = null;

        public SqlDataReader Data
        {
            get
            {
                return dataReader;
            }
        }

        public bool HasRows
        {
            get
            {
                return dataReader.HasRows;
            }
        }

        public object this[string index]
        {
            get
            {
                return dataReader[index];
            }
        }

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
            else
            {
                return dataReader[fieldName].ToString();
            }
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
            if (disposing)
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                    dataReader = null;
                }
            }
        }
    }
}
