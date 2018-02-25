using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace CrudEntity
{
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
    public class CrudEntity<T, Cmd, Cnn> where T : new() where Cmd : DbCommand, ICloneable, new() where Cnn : DbConnection, ICloneable, new()
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
    {
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public CrudEntity()
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        {
                this._connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Default;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public CrudEntity(string connectionString = null)
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        {

            if (connectionString == null)
            {
                this._connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Default;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }
            else
            {
                this._connectionString = connectionString;
            }
        }
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public CrudEntity(string connectionString , string tableName)
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        {
            if (connectionString == null)
            {
                this._connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Default;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }
            else
            {
                this._connectionString = connectionString;
            }
            _tableName = tableName;
        }
        private string _tableName = "Table1";
        private Cnn _sqlconnection;
        private Cmd command;
        private string _connectionString;
        private StringBuilder kayit, kayit1, kayit2;

        private void Connect()
        {
            _sqlconnection = new Cnn();
            _sqlconnection.ConnectionString = this._connectionString;
            _sqlconnection.Open();
        }
        private void Disconnect()
        {
            _sqlconnection.Close();
        }
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public bool Add(T TModel)
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        {
            try
            {
                Connect();
                kayit = new StringBuilder();
                kayit1 = new StringBuilder();
                kayit2 = new StringBuilder();
                var keys = TModel.GetType().GetProperties();
                int i = 0;
                foreach (var key in keys)
                {
                    i++;
                    if (i == 1)
                    {
                        //id
                    }
                    else if (i == 2)
                    {
                        kayit1.Append(key.Name);
                        kayit2.Append("@" + key.Name);
                    }
                    else
                    {
                        kayit1.Append(", " + key.Name);
                        kayit2.Append(",@" + key.Name);
                    }
                }
                kayit.Append("insert into " + _tableName + " (");
                kayit.Append(kayit1.ToString());
                kayit.Append(" ) values (");
                kayit.Append(kayit2.ToString());
                kayit.Append(")");
                command = new Cmd()
                {
                    CommandText = kayit.ToString(),
                    Connection = _sqlconnection
                };
                i = 0;
                foreach (var key in keys)
                {
                    if (i == 0)
                        i++;
                    else
                    {
                        AddWithValue("@" + key.Name, key.GetValue(TModel, null));
                    }

                }
                command.ExecuteNonQuery();
                Disconnect();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public bool Edit(T TModel)
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        {
            try
            {
                Connect();
                kayit = new StringBuilder();
                kayit1 = new StringBuilder();
                kayit2 = new StringBuilder();
                var keys = TModel.GetType().GetProperties();
                int i = 0;
                foreach (var key in keys)
                {
                    i++;
                    if (i == 1)
                    {
                        kayit1.Append(" where " + key.Name + "=@" + key.Name + ";");
                    }
                    else if (i == 2)
                    {
                        kayit2.Append(key.Name + "=@" + key.Name);
                    }
                    else
                    {
                        kayit2.Append(", " + key.Name + "=@" + key.Name);
                    }
                }
                kayit.Append("update " + _tableName + " set ");
                kayit.Append(kayit2.ToString());
                kayit.Append(kayit1.ToString());
                command = new Cmd()
                {
                    CommandText = kayit.ToString(),
                    Connection = _sqlconnection
                };
                foreach (var key in keys)
                {
                    AddWithValue("@" + key.Name, key.GetValue(TModel, null));
                }
                command.ExecuteNonQuery();
                Disconnect();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public List<T> getLists()
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        {
            Connect();
            List<T> TModels = new List<T>();
            T TModel = new T();
            command = new Cmd()
            {
                CommandText = "select * from " + _tableName,
                Connection = _sqlconnection
            };
            using (var reader = command.ExecuteReader())
            {
                var keys = TModel.GetType().GetProperties();
                while (reader.Read())
                {
                    foreach (var key in keys)
                    {
                        key.SetValue(TModel, Convert.ChangeType(reader[key.Name], key.PropertyType));
                    }
                    TModels.Add(TModel);
                    TModel = new T();
                }
            }
            Disconnect();
            return TModels;
        }



#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public bool Remove(int Id)
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        {
            T l = new T();
            var keys = l.GetType().GetProperties();
            Connect();
            command = new Cmd()
            {
                CommandText = "delete from " + _tableName + " where " + keys[0].Name + "=@" + keys[0].Name,
                Connection = _sqlconnection
            };
            AddWithValue("@" + keys[0].Name, Id);
            command.ExecuteNonQuery();
            Disconnect();
            return true;
        }
        private void AddWithValue(string parameterName, object parameterValue)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            command.Parameters.Add(parameter);
        }
    }
}
