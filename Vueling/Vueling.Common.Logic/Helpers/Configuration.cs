using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using Vueling.Common.Logic.Properties;

namespace Vueling.Common.Logic.Helpers
{
    public static class Configuration
    {
        private static readonly ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);

        public static string DbConnectionString()
        {
            try
            {
                string conn = Environment.GetEnvironmentVariable(ConfigStrings.DbConnection, EnvironmentVariableTarget.User);
                if (String.IsNullOrEmpty(conn))
                {
                    Environment.SetEnvironmentVariable(ConfigStrings.DbConnection, ConfigStrings.DbConnectionString, EnvironmentVariableTarget.User);
                    conn = ConfigStrings.DbConnectionString;
                }
                return conn;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        private static bool ExistsProcedure(string name)
        {            
            string query = "select * from sysobjects where type='P' and name='"+ name + "'";
            bool spExists = false;
            using (SqlConnection conn = new SqlConnection(DbConnectionString()))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            spExists = true;
                            break;
                        }
                    }
                }
            }
            return spExists;
        }

        private static void CreateInsertProcedure()
        {
            try
            {
                if (!ExistsProcedure("spInsertStudent"))
                {
                    StringBuilder sbSP = new StringBuilder();
                    string procedure = "CREATE PROCEDURE dbo.spInsertStudent " +
                        "@GUID nvarchar(50)," +
                        "@ID int, @Nombre nvarchar(50)," +
                        "@Apellidos nvarchar(50), @DNI nvarchar(50) ," +
                        "@FechaNacimiento datetime2(7), @Edad int ," +
                        "@FechaCompletaAlta datetime2(7)" +
                        "AS " +
                        "BEGIN SET NOCOUNT ON;" +
                        " INSERT INTO [dbo].[Students] (GUID, ID, Nombre, Apellidos, DNI, FechaNacimiento, Edad, FechaCompletaAlta)" +
                        " VALUES (@GUID,@ID,@Nombre, @Apellidos,@DNI, @FechaNacimiento,@Edad,@FechaCompletaAlta)" +
                        " END";
                    sbSP.AppendLine(procedure);
                    using (SqlConnection connection = new SqlConnection(DbConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand(sbSP.ToString(), connection))
                        {
                            connection.Open();
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        private static void CreateSelectAllProcedure()
        {
            try
            {
                if (!ExistsProcedure("spSelectAllStudents"))
                {
                    StringBuilder sbSP = new StringBuilder();
                    string procedure = "CREATE PROCEDURE dbo.spSelectAllStudents " +
                        
                        "AS " +
                        "BEGIN " +
                        "Select * from [dbo].[Students] " +                        
                        "END";
                    sbSP.AppendLine(procedure);
                    using (SqlConnection connection = new SqlConnection(DbConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand(sbSP.ToString(), connection))
                        {
                            connection.Open();
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        private static void CreateSelectByGuid()
        {
            try
            {
                if (!ExistsProcedure("spSelectByGuid"))
                {
                    StringBuilder sbSP = new StringBuilder();
                    string procedure = "CREATE PROCEDURE dbo.spSelectByGuid @GUID nvarchar(50) AS " +
                        "BEGIN " +
                        "Select * FROM [dbo].[Students] " +
                    "WHERE GUID = @GUID " +
                    "END";
                    sbSP.AppendLine(procedure);
                    using (SqlConnection connection = new SqlConnection(DbConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand(sbSP.ToString(), connection))
                        {
                            connection.Open();
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        private static void CreateUpdateProcedure()
        {            
            try
            {
                if (!ExistsProcedure("spUpdateByGuid"))
                {
                    StringBuilder sbSP = new StringBuilder();
                    string procedure = "CREATE PROCEDURE dbo.spUpdateByGuid @GUID nvarchar(50)," +
                        "@Nombre nvarchar(50), @Apellidos nvarchar(50)," +
                        "@DNI nvarchar(50), @FechaNacimiento datetime2(7)" + 
                        " AS " +
                        "BEGIN " +
                        "UPDATE [dbo].[Students] " +
                        "SET Nombre = @Nombre, " +
                        "Apellidos = @Apellidos, " +
                        "DNI = @DNI, " +
                        "FechaNacimiento = @FechaNacimiento " +                        
                    "WHERE GUID = @GUID " +
                    "END";
                    sbSP.AppendLine(procedure);
                    using (SqlConnection connection = new SqlConnection(DbConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand(sbSP.ToString(), connection))
                        {
                            connection.Open();
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        private static void CreateDeleteProcedure()
        {
            try
            {
                if (!ExistsProcedure("spDeleteStudent"))
                {
                    StringBuilder sbSP = new StringBuilder();
                    string procedure = "CREATE PROCEDURE dbo.spDeleteStudent @GUID nvarchar(50) AS " + 
                        "BEGIN " +
                        "DELETE FROM [dbo].[Students] " +
                    "WHERE GUID = @GUID " +
                    "END";
                    sbSP.AppendLine(procedure);
                    using (SqlConnection connection = new SqlConnection(DbConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand(sbSP.ToString(), connection))
                        {
                            connection.Open();
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        public static void DbCreateProcedures()
        {
            try
            {
                CreateInsertProcedure();
                CreateSelectAllProcedure();
                CreateSelectByGuid();
                CreateUpdateProcedure();
                CreateDeleteProcedure();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }
    }
}
