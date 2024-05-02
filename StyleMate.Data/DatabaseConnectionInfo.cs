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
        public string Server { get; set; }
        public string Database { get; set; }
        public int Port { get; set; } 
        public string User { get; set; }
        public string Password { get; set; } 

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