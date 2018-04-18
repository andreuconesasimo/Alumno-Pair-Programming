using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Vueling.Common.Logic;
using Vueling.Common.Logic.Model;
using Vueling.Common.Logic.Properties;
using Vueling.DataAccess.Dao.Interfaces;

namespace Vueling.DataAccess.Dao
{
    public class FicheroAlumnoJson : IFicheroAlumno
    {
        private string Ruta { get; set; }
        private readonly ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);

        public FicheroAlumnoJson()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                Ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + ConfigStrings.JsonFile;
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        public FicheroAlumnoJson(string ruta)
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
                List<Alumno> alumnosFicheroExistente = DeserializeJson();
                alumnosFicheroExistente.Add(alumno);
                string jsonNuevo = JsonConvert.SerializeObject(alumnosFicheroExistente, Formatting.Indented);
                FileUtils.EscribirFichero(jsonNuevo, Ruta);
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
                List<Alumno> alumnosFicheroExistente = DeserializeJson();
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

        private List<Alumno> DeserializeJson()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                List<Alumno> alumnosFicheroExistente = new List<Alumno>();
                if (File.Exists(Ruta) && new FileInfo(Ruta).Length != 0)
                {
                    alumnosFicheroExistente = JsonConvert.DeserializeObject<List<Alumno>>(File.ReadAllText(Ruta));
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

        public List<Alumno> GetAll()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                List<Alumno> alumnos = DeserializeJson();
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
                ListadoAlumnosJson listadoAlumnosJson = ListadoAlumnosJson.Instance();
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return listadoAlumnosJson.ListadoAlumnos;
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

                List<Alumno> alumnosExistentes = DeserializeJson();
                bool encontrado = false;
                int i = 0;
                while (!encontrado && i < alumnosExistentes.Count)
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
                    string jsonNuevo = JsonConvert.SerializeObject(alumnosExistentes, Formatting.Indented);
                    FileUtils.EscribirFichero(jsonNuevo, Ruta);
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
            throw new NotImplementedException();
        }
    }
}
