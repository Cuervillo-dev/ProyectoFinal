using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Restaurante.Datos
{
    public static class Conexion
    {
        // Primero busca la variable de entorno; si no existe, usa App.config / appsettings.local.config
        private static string Leer(string clave)
        {
            string v = Environment.GetEnvironmentVariable(clave);
            if (string.IsNullOrWhiteSpace(v))
                v = ConfigurationManager.AppSettings[clave];
            return v;
        }

        public static string CadenaConexion
        {
            get
            {
                string host = Leer("DB_HOST");
                if (string.IsNullOrWhiteSpace(host) || host == "TU_HOST")
                    throw new InvalidOperationException(
                        "Falta configurar DB_HOST (variables de entorno DB_HOST, DB_PORT, DB_NAME, DB_USER, DB_PASS).");

                uint puerto;
                if (!uint.TryParse(Leer("DB_PORT"), out puerto)) puerto = 4000;

                var b = new MySqlConnectionStringBuilder
                {
                    Server = host,
                    Port = puerto,
                    Database = Leer("DB_NAME") ?? "proyectoDB",
                    UserID = Leer("DB_USER"),
                    Password = Leer("DB_PASS"),
                    SslMode = MySqlSslMode.Required,
                    ConnectionTimeout = 15
                };
                return b.ConnectionString;
            }
        }

        public static async Task<MySqlConnection> AbrirAsync(CancellationToken ct)
        {
            var con = new MySqlConnection(CadenaConexion);
            try
            {
                await con.OpenAsync(ct);
                return con;
            }
            catch
            {
                con.Dispose();
                throw;
            }
        }

        // Version sincrona: solo para usar dentro de Task.Run (hilo de fondo).
        public static MySqlConnection Abrir()
        {
            var con = new MySqlConnection(CadenaConexion);
            try
            {
                con.Open();
                return con;
            }
            catch
            {
                con.Dispose();
                throw;
            }
        }
    }
}