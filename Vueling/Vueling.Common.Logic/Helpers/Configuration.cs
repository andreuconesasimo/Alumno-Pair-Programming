using System;
using Vueling.Common.Logic.Properties;

namespace Vueling.Common.Logic.Helpers
{
    public static class Configuration
    {
        public static string DbConnectionString()
        {
            string conn = Environment.GetEnvironmentVariable(ConfigStrings.DbConnection, EnvironmentVariableTarget.User);
            if (String.IsNullOrEmpty(conn))
            {
                Environment.SetEnvironmentVariable(ConfigStrings.DbConnection, ConfigStrings.DbConnectionString, EnvironmentVariableTarget.User);
                conn = ConfigStrings.DbConnectionString;
            }            
            return conn;
        }
    }
}
