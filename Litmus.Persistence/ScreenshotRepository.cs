using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Litmus.Domain;

using MySql;
using MySql.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Dapper;
using System.Configuration;

namespace Litmus.Persistence
{
    public class ScreenshotRepository : IScreenshotRepository
    {
        private string connectionString;
        private MySqlConnection db;
        private Configuration config;

        public ScreenshotRepository()
        {
            config = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap() { ExeConfigFilename = "App.config" }, ConfigurationUserLevel.None);

            connectionString = config.ConnectionStrings.ConnectionStrings["MySql"].ConnectionString;
            db = new MySqlConnection(connectionString);
        }

        public Screenshot Find(string url)
        {
            return db.Query<Screenshot>("SELECT * FROM Screenshots WHERE Url = @Url", new { url }).SingleOrDefault();
        }

        public List<Screenshot> GetAll()
        {
            return db.Query<Screenshot>("SELECT * FROM Screenshots").ToList();
        }

        public int Add(IScreenshot screenshot)
        {
            string sql = "INSERT INTO Screenshots (Url, CreatedAt, UpdatedAt) VALUES (@Url, @CreatedAt, @UpdatedAt);";

            return db.Execute(sql, screenshot);
        }

        public int Update(IScreenshot screenshot)
        {
            string sql = @"UPDATE Screenshots 
                           SET Url = @Url, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt 
                           WHERE Url = @Url";

            return db.Execute(sql, screenshot);
        }

        public int Remove(string url)
        {
            return db.Execute("DELETE FROM Screenshots WHERE Url = @Url", new { url });
        }

        public int RemoveUrlBy(string pattern)
        {
            pattern = $"%{pattern}%";
            return db.Execute("DELETE FROM Screenshots WHERE Url LIKE @pattern", new { pattern });
        }
    }
}
