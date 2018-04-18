using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using Vueling.Common.Logic;
using Vueling.Common.Logic.Helpers;
using Vueling.Common.Logic.Model;
using Vueling.Common.Logic.Properties;
using Vueling.DataAccess.Dao.Interfaces;

namespace Vueling.DataAccess.Dao
{
    public class FicheroAlumnoSql : IFicheroAlumno
    {
        private readonly ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string ConnectionString = Configuration.DbConnectionString();
        private readonly string queryInsert = "INSERT INTO dbo.Students (GUID, ID, Nombre, Apellidos, DNI, FechaNacimiento, Edad, FechaCompletaAlta) VALUES (@GUID,@ID,@Nombre, @Apellidos, @DNI, @FechaNacimiento, @Edad, @FechaCompletaAlta)";
        private readonly string querySelectAll = "Select * from dbo.Students";
        private readonly string queryDeleteByGuid = "DELETE from dbo.Students WHERE GUID = @GUID";
        private readonly string querySelectByGuid = "Select * from dbo.Students where GUID = @GUID";
        private readonly string queryUpdateByGuid =  "UPDATE dbo.Students SET Nombre = @Nombre, Apellidos = @Apellidos, DNI = @DNI, FechaNacimiento = @FechaNacimiento, Edad = @Edad Where GUID = @GUID";
            

        public FicheroAlumnoSql()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        public Alumno Add(Alumno alumno)
        {
            try
            {
            logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryInsert, connection))
                {
                    command.Parameters.AddWithValue("@GUID", alumno.GUID.ToString());
                    command.Parameters.AddWithValue("@ID", alumno.ID);
                    command.Parameters.AddWithValue("@Nombre", alumno.Nombre);
                    command.Parameters.AddWithValue("@Apellidos", alumno.Apellidos);
                    command.Parameters.AddWithValue("@DNI", alumno.DNI);
                    command.Parameters.AddWithValue("@FechaNacimiento", alumno.FechaNacimiento);
                    command.Parameters.AddWithValue("@Edad", alumno.Edad);
                    command.Parameters.AddWithValue("@FechaCompletaAlta", alumno.FechaCompletaAlta);

                    connection.Open();
                    int result = command.ExecuteNonQuery();                                            
                    if (result < 0)                        
                        logger.Error(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.InsertError);       
                    else                      
                        logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + alumno.ToString());
                }
            }
            logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
            return alumno;
        }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
}

        public List<Alumno> DeleteByGuid(string guid)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);                
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(queryDeleteByGuid, con))
                    {
                        command.Parameters.AddWithValue("@GUID", guid);
                        command.ExecuteNonQuery();
                    }
                }                
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return GetAll();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        public List<Alumno> GetAll()
        {
            try
            {
                List<Alumno> alumnos = new List<Alumno>();
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(querySelectAll, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Alumno alumno = new Alumno(Guid.Parse(reader.GetString(0)),reader.GetInt32(1),
                                        reader.GetString(2),reader.GetString(3),reader.GetString(4),reader.GetDateTime(5),
                                        reader.GetInt32(6), reader.GetDateTime(7));
                                    logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + alumno.ToString());
                                    alumnos.Add(alumno);
                                }
                            }
                        }
                    }
                }
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);

                return alumnos;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        public List<Alumno> GetSingletonInstance()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                List<Alumno> alumnos = GetAll();
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return alumnos;

            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        public Alumno Select(Guid guid)
        {
            try
            {
                Alumno alumno = new Alumno();
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(querySelectByGuid, con))
                    {                        
                        command.Parameters.AddWithValue("@GUID", guid.ToString());
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    alumno = new Alumno(Guid.Parse(reader.GetString(0)),reader.GetInt32(1),
                                    reader.GetString(2), reader.GetString(3), reader.GetString(4),
                                    reader.GetDateTime(5), reader.GetInt32(6), reader.GetDateTime(7));
                                    logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + alumno.ToString());                                    
                                }
                            }
                        }
                    }
                }
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return alumno;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        public void Update(Alumno alumno)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(queryUpdateByGuid, con))
                    {
                        command.Parameters.AddWithValue("@GUID", alumno.GUID);
                        command.Parameters.AddWithValue("@Nombre", alumno.Nombre);
                        command.Parameters.AddWithValue("@Apellidos", alumno.Apellidos);
                        command.Parameters.AddWithValue("@DNI", alumno.DNI);
                        command.Parameters.AddWithValue("@FechaNacimiento", alumno.FechaNacimiento);
                        command.ExecuteNonQuery();
                    }
                }
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);                
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }
    }
}
