using System;
using System.IO;
using System.Linq;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Data.Tables;
using Data.Access;
using Data.Access.Interfaces;

namespace ControlWork1
{
    class Program
    {
        static void Main(string[] args)
        {     
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appconfig.json", false, true);
            IConfigurationRoot configurationRoot = builder.Build();
            IUserRepository repository = new UserRepository(configurationRoot.GetConnectionString("DebugConnectionString"));
        }
    }
}
