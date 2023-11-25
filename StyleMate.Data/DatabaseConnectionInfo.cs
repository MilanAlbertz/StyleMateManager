using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StyleMate.Data
{
    /// <summary>
    /// Holds database connection information, loaded from file
    /// </summary>
    public class DatabaseConnectionInfo
    {
        public string Server { get; set; } = "localhost";
        public string Database { get; set; } = "StyleMateDatabase";
        public int Port { get; set; } = 3306;
        public string User { get; set; } = "root";
        public string Password { get; set; } = "TestDB123";

        [JsonIgnore]
        public string ConnectionString
        {
            get
            {
                return $"server={Server};port={Port};database={Database};user={User};password={Password}";
            }
        }

    }
}