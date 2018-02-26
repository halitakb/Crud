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
            this.command = dbCommand;
            this.cmd2 = dbCommand;
            _sqlconnection = dbConnection;
            _sqlconnection.ConnectionString = connection;
        }
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
#pragma warning disable IDE1006 // Adlandırma Stilleri
        public void newDbCommand()
#pragma warning restore IDE1006 // Adlandırma Stilleri
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        {
            this.command = this.cmd2;
        }
        internal DbCommand command { get; set; }
        private DbCommand cmd2 { get; set; }
        internal DbConnection _sqlconnection { get; set; }

#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public void Connect()
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        {
            _sqlconnection.Open();
        }
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public void Disconnect()
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        {
            _sqlconnection.Close();
        }
    }
}