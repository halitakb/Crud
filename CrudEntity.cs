using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                CrudStatic.command.Connection = CrudStatic._sqlconnection;
                CrudStatic.Connect();
                kayit = new StringBuilder();
                kayit1 = new StringBuilder();
                kayit2 = new StringBuilder();
                int i = 0;
                foreach (var key in typeof(T).GetProperties())
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
                CrudStatic.command.CommandText = kayit.ToString();
                CrudStatic.command.CommandType = System.Data.CommandType.Text;
                i = 0;
                foreach (var key in TModel.GetType().GetProperties())
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
                CrudStatic.command.Connection = CrudStatic._sqlconnection;
                CrudStatic.Connect();
                kayit = new StringBuilder();
                kayit1 = new StringBuilder();
                kayit2 = new StringBuilder();
                int i = 0;
                foreach (var key in typeof(T).GetProperties())
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
                CrudStatic.command.CommandType = CommandType.Text;
                CrudStatic.command.CommandText = kayit.ToString();
                foreach (var key in TModel.GetType().GetProperties())
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
        public List<T> GetJoin<X>(Dictionary<string, object> whereIs = null, List<string> fields = null, bool IsTop = false, int Top = 100) where X : class, new()
        {
            CrudStatic.command.Connection = CrudStatic._sqlconnection;
            CrudStatic.Connect();
            CrudStatic.command.CommandType = CommandType.Text;
            selectCommand(whereIs, fields, IsTop, Top);
            kayit.Append(" inner join " + typeof(X).Name + " on ");
            kayit.Append(typeof(T).Name + "." + typeof(X).Name + "ID = " + typeof(X).Name + ".Id");
            if (whereIs != null)
            {
                kayit.Append(" where ");
                foreach (var item in whereIs)
                {
                    kayit.Append(" " + item.Key + "=@" + item.Key);
                }
                CrudStatic.command.CommandText = kayit.ToString();
                foreach (var item in whereIs)
                {
                    AddWithValue("@" + item.Key, item.Value);
                }
            }
            else
            {
                CrudStatic.command.CommandText = kayit.ToString();
            }
            List<T> TModels = new List<T>();
            T TModel = new T();
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
        public List<T> GetLists(bool IsTop = false, int Top = 100)
        {
            CrudStatic.command.Connection = CrudStatic._sqlconnection;
            CrudStatic.Connect();
            CrudStatic.command.CommandType = CommandType.Text;
            if (IsTop)
                CrudStatic.command.CommandText = "select top " + Top.ToString() + " * from " + _tableName;
            else
                CrudStatic.command.CommandText = "select * from " + _tableName;
            List<T> TModels = new List<T>();
            T TModel = new T();
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
        public List<T> Where(Dictionary<string, object> whereIs, int Top = 100, bool IsTop = false)
        {
            CrudStatic.command.Connection = CrudStatic._sqlconnection;
            CrudStatic.Connect();
            CrudStatic.command.CommandType = CommandType.Text;
            selectCommand(whereIs, null, IsTop, Top);
            if (whereIs.Count > 0)
            {
                kayit.Append("where ");
            }
            foreach (var item in whereIs)
            {
                kayit.Append(" " + item.Key + "=@" + item.Key);
            }
            CrudStatic.command.CommandText = kayit.ToString();
            foreach (var item in whereIs)
            {
                AddWithValue("@" + item.Key, item.Value);
            }
            List<T> TModels = new List<T>();
            T TModel = new T();
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
        public List<X> Where<X>(Dictionary<string, object> whereIs, List<string> fields = null, int Top = 100, bool IsTop = false) where X : class, new()
        {
            CrudStatic.command.Connection = CrudStatic._sqlconnection;
            CrudStatic.Connect();
            CrudStatic.command.CommandType = CommandType.Text;
            selectCommand(whereIs, fields, IsTop, Top);
            if (whereIs.Count > 0)
            {
                kayit.Append("where ");
            }
            foreach (var item in whereIs)
            {
                kayit.Append(" " + item.Key + "=@" + item.Key);
            }
            CrudStatic.command.CommandText = kayit.ToString();
            foreach (var item in whereIs)
            {
                AddWithValue("@" + item.Key, item.Value);
            }
            List<X> TModels = new List<X>();
            X XModel = new X();
            using (var reader = CrudStatic.command.ExecuteReader())
            {
                var keys = XModel.GetType().GetProperties();
                while (reader.Read())
                {
                    foreach (var key in keys)
                    {
                        key.SetValue(XModel, Convert.ChangeType(reader[key.Name], key.PropertyType));
                    }
                    TModels.Add(XModel);
                    XModel = new X();
                }
            }
            CrudStatic.Disconnect();
            return TModels;
        }
        private void selectCommand(Dictionary<string, object> whereIs, List<string> fields, bool IsTop, int Top)
        {
            kayit = new StringBuilder();
            kayit1 = new StringBuilder();
            kayit1.Append("*");
            if (IsTop)
            {
                if (fields != null)
                {
                    kayit1 = new StringBuilder();
                    int countfield = 0;
                    foreach (var item in fields)
                    {
                        countfield++;
                        if (countfield == 1 && fields.Count == countfield)
                            kayit1.Append(item);
                        else if (countfield <= fields.Count)
                        {
                            kayit1.Append("," + item);
                        }
                    }
                }
                kayit.Append("select TOP " + Top.ToString() + " PERCENT " + kayit1.ToString() + " from " + _tableName + " ");
            }
            else
            {
                if (fields != null)
                {
                    kayit1 = new StringBuilder();
                    int countfield = 0;
                    foreach (var item in fields)
                    {
                        countfield++;
                        if (countfield == 1 && fields.Count == countfield)
                            kayit1.Append(item);
                        else if (countfield <= fields.Count)
                        {
                            kayit1.Append("," + item);
                        }
                    }
                }
                kayit.Append("select  " + kayit1.ToString() + " from " + _tableName + " ");
            }
        }
        public bool Remove(int Id)
        {
            CrudStatic.command.Connection = CrudStatic._sqlconnection;
            CrudStatic.Connect();
            CrudStatic.command.CommandType = CommandType.Text;
            var Name = typeof(T).GetProperties()[0].Name;
            CrudStatic.command.CommandText = "delete from " + _tableName + " where " + Name + "=@" + Name;
            AddWithValue("@" + Name, Id);
            CrudStatic.command.ExecuteNonQuery();
            CrudStatic.Disconnect();
            return true;
        }
        private void AddWithValue(string parameterName, object parameterValue, ParameterDirection direction = ParameterDirection.Input, DbType dbType = DbType.String)
        {
            var parameter = CrudStatic.command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            parameter.Direction = direction;
            parameter.DbType = dbType;
            CrudStatic.command.Parameters.Add(parameter);
        }
        public List<ProcedureModel> procedureT<ProcedureModel>(string commandText, Dictionary<string, string> parameters, ParameterDirection direction = ParameterDirection.Input, DbType dbType = DbType.String) where ProcedureModel : class, new()
        {
            CrudStatic.command.Connection = CrudStatic._sqlconnection;
            CrudStatic.Connect();
            CrudStatic.command.CommandType = CommandType.StoredProcedure;
            CrudStatic.command.CommandText = commandText;
            var Name = typeof(ProcedureModel).GetProperties()[0].Name;
            foreach (var item in parameters)
            {
                AddWithValue(item.Key, item.Value, direction, dbType);
            }

            List<ProcedureModel> TModels = new List<ProcedureModel>();
            ProcedureModel TModel = new ProcedureModel();
            using (var reader = CrudStatic.command.ExecuteReader())
            {
                while (reader.Read())
                {
                    foreach (var key in TModel.GetType().GetProperties())
                    {
                        key.SetValue(TModel, Convert.ChangeType(reader[key.Name], key.PropertyType));
                    }
                    TModels.Add(TModel);
                    TModel = new ProcedureModel();
                }
            }
            return TModels;
        }
    }
}
