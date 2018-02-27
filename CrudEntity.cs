using System;
using System.Collections.Generic;
using System.Text;

namespace CrudEntity
{
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
    public class CrudEntity<T> where T : new()
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
    {
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public CrudEntity(string tableName)
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        {
            _tableName = tableName;
        }
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public CrudEntity()
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        {
            _tableName = "Table1";
        }
        private string _tableName { get; set; }
        private StringBuilder kayit, kayit1, kayit2;
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public bool Add(T TModel)
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        {
            try
            {
                CrudStatic.Connect();
                kayit = kayit1 = kayit2 = new StringBuilder();
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
                CrudStatic.newDbCommand();
                CrudStatic.command.CommandText = kayit.ToString();
                CrudStatic.command.Connection = CrudStatic._sqlconnection;
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
                CrudStatic.command.ExecuteNonQuery();
                CrudStatic.Disconnect();
                return true;
            }
            catch (Exception)
            {
                CrudStatic.Disconnect();
                return false;
            }
        }

#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public bool Edit(T TModel)
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        {
            try
            {
                CrudStatic.Connect();
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
                CrudStatic.newDbCommand();
                CrudStatic.command.CommandText = kayit.ToString();
                CrudStatic.command.Connection = CrudStatic._sqlconnection;
                foreach (var key in keys)
                {
                    AddWithValue("@" + key.Name, key.GetValue(TModel, null));
                }
                CrudStatic.command.ExecuteNonQuery();
                CrudStatic.Disconnect();
                return true;
            }
            catch (Exception)
            {
                CrudStatic.Disconnect();
                return false;
            }
        }

#pragma warning disable CS0693 // Tür parametresi dış türden tür parametresi ile aynı ada sahip
#pragma warning disable IDE1006 // Adlandırma Stilleri
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public List<T> 
            getLists<T>() where T : class, new()
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
#pragma warning restore IDE1006 // Adlandırma Stilleri
#pragma warning restore CS0693 // Tür parametresi dış türden tür parametresi ile aynı ada sahip
        {
            CrudStatic.Connect();
            List<T> TModels = new List<T>();
            T TModel = new T();
            CrudStatic.newDbCommand();
            CrudStatic.command.CommandText = "select * from " + _tableName;
            CrudStatic.command.Connection = CrudStatic._sqlconnection;
            using (var reader = CrudStatic.command.ExecuteReader())
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
            CrudStatic.Disconnect();
            return TModels;
        }
#pragma warning disable CS0693 // Tür parametresi dış türden tür parametresi ile aynı ada sahip
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public bool Remove<T>(int Id) where T : class, new()
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
#pragma warning restore CS0693 // Tür parametresi dış türden tür parametresi ile aynı ada sahip
        {
            T l = new T();
            var keys = l.GetType().GetProperties();
            CrudStatic.Connect();
            CrudStatic.newDbCommand();
            CrudStatic.command.CommandText = "delete from " + _tableName + " where " + keys[0].Name + "=@" + keys[0].Name;
            CrudStatic.command.Connection = CrudStatic._sqlconnection;
            AddWithValue("@" + keys[0].Name, Id);
            CrudStatic.command.ExecuteNonQuery();
            CrudStatic.Disconnect();
            return true;
        }
        private void AddWithValue(string parameterName, object parameterValue)
        {
            var parameter = CrudStatic.command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            CrudStatic.command.Parameters.Add(parameter);
        }
    }
}