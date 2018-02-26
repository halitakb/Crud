using System.Data.Common;

namespace CrudEntity
{
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
    public class DbCrudConnection
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
    {
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public DbCrudConnection()
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        {
        }
#pragma warning disable IDE1006 // Adlandırma Stilleri
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public void setConnection<U, V>(string connection, U dbCommand, V dbConnection) where U : DbCommand, new() where V : DbConnection
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
#pragma warning restore IDE1006 // Adlandırma Stilleri
        {
            DbCrudConnection.command = dbCommand;
            DbCrudConnection.cmd2 = dbCommand;
            DbCrudConnection._sqlconnection = dbConnection;
            DbCrudConnection._sqlconnection.ConnectionString = connection;
        }
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
#pragma warning disable IDE1006 // Adlandırma Stilleri
        public void newDbCommand()
#pragma warning restore IDE1006 // Adlandırma Stilleri
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        {
            DbCrudConnection.command = DbCrudConnection.cmd2;
        }
        internal static DbCommand command { get; set; }
        private static DbCommand cmd2 { get; set; }
        internal static DbConnection _sqlconnection { get; set; }

#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public void Connect()
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        {
            DbCrudConnection._sqlconnection.Open();
        }
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public void Disconnect()
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        {
            DbCrudConnection._sqlconnection.Close();
        }
    }
}