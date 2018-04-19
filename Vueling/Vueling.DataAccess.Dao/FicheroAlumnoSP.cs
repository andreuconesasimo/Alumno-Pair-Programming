using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Vueling.Common.Logic;
using Vueling.Common.Logic.Helpers;
using Vueling.Common.Logic.Model;
using Vueling.Common.Logic.Properties;
using Vueling.DataAccess.Dao.Interfaces;

namespace Vueling.DataAccess.Dao
{
    public class FicheroAlumnoSP : IFicheroAlumno
    {
        private readonly string ConnectionString = Configuration.DbConnectionString();
        private readonly ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);

        public Alumno Add(Alumno alumno)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("spInsertStudent", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@GUID", SqlDbType.NVarChar).Value = alumno.GUID.ToString();
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = alumno.ID;
                        command.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = alumno.Nombre;
                        command.Parameters.Add("@Apellidos", SqlDbType.NVarChar).Value = alumno.Apellidos;
                        command.Parameters.Add("@DNI", SqlDbType.NVarChar).Value = alumno.DNI;
                        command.Parameters.Add("@FechaNacimiento", SqlDbType.DateTime2).Value = alumno.FechaNacimiento;
                        command.Parameters.Add("@Edad", SqlDbType.Int).Value = alumno.Edad;
                        command.Parameters.Add("@FechaCompletaAlta", SqlDbType.DateTime2).Value = alumno.FechaCompletaAlta;
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if (result < 0)
                            logger.Error(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.InsertError);
                        else
                            logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + alumno.ToString());                         
                    }
                }                
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
                    using (SqlCommand command = new SqlCommand("spDeleteStudent", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@GUID", SqlDbType.NVarChar).Value = guid;
                        con.Open();
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
                    using (SqlCommand command = new SqlCommand("spSelectAllStudents", connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Alumno alumno = new Alumno(Guid.Parse(reader.GetString(0)), reader.GetInt32(1),
                                        reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetDateTime(5),
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
            throw new NotImplementedException();
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
                    using (SqlCommand command = new SqlCommand("spSelectByGuid", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@GUID", SqlDbType.NVarChar).Value = guid.ToString();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    alumno = new Alumno(Guid.Parse(reader.GetString(0)), reader.GetInt32(1),
                                        reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetDateTime(5),
                                        reader.GetInt32(6), reader.GetDateTime(7));
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
                    using (SqlCommand command = new SqlCommand("spUpdateByGuid", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
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
