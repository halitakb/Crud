using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudEntity
{
    public abstract class CrudStatic
    {
        public CrudStatic()
        {

        }

        internal static DbCommand command { get; set; }
        internal static DbConnection _sqlconnection { get; set; }
        public static void setConnection<U, V>(string connection) where U : DbCommand, new() where V : DbConnection, new()
        {
            command = new U();
            _sqlconnection = new V();
            _sqlconnection.ConnectionString = connection;
        }

        internal static void Connect()
        {
            _sqlconnection.Open();
        }
        internal static void Disconnect()
        {
            _sqlconnection.Close();
        }

    }
}
