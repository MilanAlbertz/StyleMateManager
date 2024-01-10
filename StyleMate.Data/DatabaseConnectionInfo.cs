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
        public string Server { get; set; } = "2a02:a45c:d6dc:1:da3a:ddff:fe81:3070";
        public string Database { get; set; } = "stylematetestdb";
        public int Port { get; set; } = 3306;
        public string User { get; set; } = "root";
        public string Password { get; set; } = "B@iley2003";

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