using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StyleMate.Data
{
    /// <summary>
    /// Holds database connection information, loaded from file
    /// </summary>
    public sealed class DatabaseConnectionHandler
    {
        private static DatabaseConnectionHandler? instance;
        private static readonly object padlock = new();

        /// <summary>
        /// The database connection info
        /// </summary>
        public DatabaseConnectionInfo Connection { get; set; }

        public DatabaseConnectionHandler()
        {
            Connection = new DatabaseConnectionInfo();
            SettingsFromFile();
        }

        public static DatabaseConnectionHandler Instance
        {
            get
            {
                lock (padlock)
                {
                    instance ??= new();
                }
                return instance;
            }
        }

        private void SettingsFromFile()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            string settingsFileName = Path.Combine(assemblyFolder, "dbsetting.json");

            if (!File.Exists(settingsFileName))
            {
                File.WriteAllText(settingsFileName, JsonSerializer.Serialize(Connection));
            }
            else
            {
                var fileContent = File.ReadAllText(settingsFileName);
                if (!string.IsNullOrEmpty(fileContent))
                {
                    Connection = JsonSerializer.Deserialize<DatabaseConnectionInfo>(fileContent) ?? new();
                }
            }
        }
    }
}