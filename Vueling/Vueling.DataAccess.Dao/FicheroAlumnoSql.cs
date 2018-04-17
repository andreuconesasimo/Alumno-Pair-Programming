using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using Vueling.Common.Logic;
using Vueling.Common.Logic.Model;
using Vueling.Common.Logic.Properties;
using Vueling.DataAccess.Dao.Interfaces;

namespace Vueling.DataAccess.Dao
{
    public class FicheroAlumnoSql : IFicheroAlumno
    {
        private readonly ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string ConnectionString;
        private readonly string queryInsert = "INSERT INTO dbo.Students (GUID, ID, Nombre, Apellidos, DNI, FechaNacimiento, Edad, FechaCompletaAlta) VALUES (@GUID,@ID,@Nombre, @Apellidos, @DNI, @FechaNacimiento, @Edad, @FechaCompletaAlta)";
        private readonly string querySelect = "Select * from dbo.Students";
        private readonly string queryDelete = "DELETE from dbo.Students WHERE GUID = @GUID";

        public FicheroAlumnoSql()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);                
                ConnectionString = "Server = localhost; Database = TutorialDB; Integrated Security = True;";
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
                    command.Parameters.Clear();
                    command.CommandText = "SELECT @@IDENTITY";
                    alumno.ID = Convert.ToInt32(command.ExecuteScalar());
                                            
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
                    using (SqlCommand command = new SqlCommand(queryDelete, con))
                    {
                        command.Parameters.AddWithValue("@GUID", guid);
                        command.ExecuteNonQuery();
                    }
                    con.Close();
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
                    using (SqlCommand command = new SqlCommand(querySelect, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Alumno alumno = new Alumno();
                                    alumno.GUID = Guid.Parse(reader.GetString(0));
                                    alumno.ID = reader.GetInt32(1);
                                    alumno.Nombre = reader.GetString(2);
                                    alumno.Apellidos = reader.GetString(3);
                                    alumno.DNI = reader.GetString(4);
                                    alumno.FechaNacimiento = reader.GetDateTime(5);
                                    alumno.Edad = reader.GetInt32(6);
                                    alumno.FechaCompletaAlta = reader.GetDateTime(7);
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
            throw new NotImplementedException();
        }

        public Alumno Select(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}
