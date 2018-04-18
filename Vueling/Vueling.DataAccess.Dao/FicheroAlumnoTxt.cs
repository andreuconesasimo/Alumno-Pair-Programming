using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Vueling.Common.Logic;
using Vueling.Common.Logic.Model;
using Vueling.Common.Logic.Properties;
using Vueling.DataAccess.Dao.Interfaces;

namespace Vueling.DataAccess.Dao
{
    public class FicheroAlumnoTxt : IFicheroAlumno
    {
        public string Ruta { get; set; }
        private readonly ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);

        public FicheroAlumnoTxt()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                Ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + ConfigStrings.TxtFile;
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);

            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        public FicheroAlumnoTxt(string ruta)
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
                using (StreamWriter sw = File.AppendText(Ruta))
                {
                    sw.WriteLine(alumno.ToString());
                }
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
            string linea;

            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                using (StreamReader sr = new StreamReader(Ruta))
                {
                    while ((linea = sr.ReadLine()) != null)
                    {
                        Alumno alumno = Deserialize(linea);
                        if (alumno.GUID == guid) return alumno;
                    }
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

        private Alumno Deserialize(string alumnoTxt)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                List<string> paramsAlumno = alumnoTxt.Split(',').ToList<string>();

                Alumno alumno = new Alumno(Guid.Parse(paramsAlumno[0]), Convert.ToInt32(paramsAlumno[1]), paramsAlumno[2],
                        paramsAlumno[3], paramsAlumno[4], DateTime.Parse(paramsAlumno[5]),
                        Convert.ToInt32(paramsAlumno[6]), DateTime.Parse(paramsAlumno[7]));
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return alumno;
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
                List<Alumno> alumnos = new List<Alumno>();
                string linea;

                using (StreamReader sr = new StreamReader(Ruta))
                {
                    while ((linea = sr.ReadLine()) != null)
                    {
                        Alumno alumno = Deserialize(linea);
                        alumnos.Add(alumno);
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

        public List<Alumno> DeleteByGuid(string guid)
        {            
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);

                var lines = File.ReadAllLines(Ruta);
                var remaining = lines.Where(x => !x.Contains(guid)).ToArray();
                File.WriteAllLines(Ruta, remaining);

                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);

                return GetAll();
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