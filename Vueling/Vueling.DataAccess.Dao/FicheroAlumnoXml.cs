using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using Vueling.Common.Logic;
using Vueling.Common.Logic.Model;
using Vueling.Common.Logic.Properties;
using Vueling.DataAccess.Dao.Interfaces;

namespace Vueling.DataAccess.Dao
{
    public class FicheroAlumnoXml : IFicheroAlumno
    {
        public string Ruta { get; set; }
        private readonly ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);

        public FicheroAlumnoXml()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                Ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + ConfigStrings.XmlFile;
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        public FicheroAlumnoXml(string ruta)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                Ruta = ruta;
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
                List<Alumno> alumnosFicheroExistente = DeserializeXml();
                alumnosFicheroExistente.Add(alumno);
                string xmlNuevo = SerializeXml(alumnosFicheroExistente);
                FileUtils.EscribirFichero(xmlNuevo, Ruta);
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return Select(alumno.GUID);
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
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                List<Alumno> alumnosFicheroExistente = DeserializeXml();
                foreach (Alumno alumno in alumnosFicheroExistente)
                {
                    if (alumno.GUID == guid) return alumno;
                }
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return null;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        private string SerializeXml(List<Alumno> alumnosFicheroExistente)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Alumno>));
                using (StringWriter writer = new StringWriter())
                {
                    xmlSerializer.Serialize(writer, alumnosFicheroExistente);
                    logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                    return writer.ToString();
                }
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        private List<Alumno> DeserializeXml()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                List<Alumno> alumnosFicheroExistente = new List<Alumno>();
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Alumno>));

                if (File.Exists(Ruta) && new FileInfo(Ruta).Length != 0)
                {
                    alumnosFicheroExistente = (List<Alumno>)xmlSerializer.Deserialize(new StringReader(File.ReadAllText(Ruta)));
                }
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return alumnosFicheroExistente;
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
                ListadoAlumnosXml listadoAlumnosXml = ListadoAlumnosXml.Instance();
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return listadoAlumnosXml.ListadoAlumnos;
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
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                List<Alumno> alumnos = DeserializeXml();
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return alumnos;
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

                List<Alumno> alumnosExistentes = DeserializeXml();
                bool encontrado = false;
                int i = 0;
                while(!encontrado && i < alumnosExistentes.Count)
                {
                    if (alumnosExistentes[i].GUID == new Guid(guid))
                    {
                        alumnosExistentes.Remove(alumnosExistentes[i]);
                        encontrado = true;                        
                    }
                    ++i;
                }
                if (encontrado)
                {
                    string xmlNuevo = SerializeXml(alumnosExistentes);
                    FileUtils.EscribirFichero(xmlNuevo, Ruta);
                }
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return alumnosExistentes;
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

                List<Alumno> alumnosExistentes = DeserializeXml();
                bool encontrado = false;
                int i = 0;
                while (!encontrado && i < alumnosExistentes.Count)
                {
                    if (alumnosExistentes[i].GUID == alumno.GUID)
                    {
                        alumnosExistentes[i].Nombre = alumno.Nombre;
                        alumnosExistentes[i].Apellidos = alumno.Apellidos;
                        alumnosExistentes[i].DNI = alumno.DNI;
                        alumnosExistentes[i].Edad = alumno.Edad;
                        alumnosExistentes[i].FechaNacimiento = alumno.FechaNacimiento;
                        encontrado = true;
                    }
                    ++i;
                }
                if (encontrado)
                {
                    string xmlNuevo = SerializeXml(alumnosExistentes);
                    FileUtils.EscribirFichero(xmlNuevo, Ruta);
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
