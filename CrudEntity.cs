using System;
using System.Collections.Generic;
using System.Text;

namespace CrudEntity
{
    public class CrudEntity<T> where T : new()
    {
        public CrudEntity(string tableName)
        {
            _tableName = tableName;
        }
        public CrudEntity()
        {
            _tableName = typeof(T).Name;
        }
        private string _tableName { get; set; }
        private StringBuilder kayit, kayit1, kayit2;
        public bool Add(T TModel)
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

        public bool Edit(T TModel)
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

        public List<T> 
            getLists<T>() where T : class, new()
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
        public bool Remove<T>(int Id) where T : class, new()
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
