using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace CrudEntity
{
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
    public class CrudStatic
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
    {
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public CrudStatic()
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        {

        }

        internal static DbCommand command { get; set; }
        private static DbCommand cmd2 { get; set; }
        internal static DbConnection _sqlconnection { get; set; }

#pragma warning disable IDE1006 // Adlandırma Stilleri
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public static void setConnection<U, V>(string connection, U dbCommand, V dbConnection) where U : DbCommand, new() where V : DbConnection
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
#pragma warning restore IDE1006 // Adlandırma Stilleri
        {
            command = dbCommand;
            cmd2 = dbCommand;
            _sqlconnection = dbConnection;
            _sqlconnection.ConnectionString = connection;
        }
#pragma warning disable IDE1006 // Adlandırma Stilleri
#pragma warning disable CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
        public static void newDbCommand()
#pragma warning restore CS1591 // Genel olarak görülebilir tür veya üye için eksik XML açıklaması
#pragma warning restore IDE1006 // Adlandırma Stilleri
        {
            command = cmd2;
        }
        internal static void Connect()
        {
            CrudStatic._sqlconnection.Open();
        }
        internal static void Disconnect()
        {
            CrudStatic._sqlconnection.Close();
        }

    }
}
