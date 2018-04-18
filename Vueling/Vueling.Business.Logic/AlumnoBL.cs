using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Vueling.Common.Logic;
using Vueling.Common.Logic.Enums;
using Vueling.Common.Logic.Model;
using Vueling.Common.Logic.Properties;
using Vueling.DataAccess.Dao;
using Vueling.DataAccess.Dao.Interfaces;

namespace Vueling.Business.Logic
{
    public class AlumnoBL : IAlumnoBL
    {
        private IFicheroAlumno ficheroAlumno;
        private readonly ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);

        public AlumnoBL()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);                
                FicheroFactory ficheroFactory = new FicheroFactory();
                ficheroAlumno = ficheroFactory.CrearFichero((Extension)Enum.Parse(typeof(Extension), ConfigurationManager.AppSettings[ConfigStrings.FileType]));
                
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
                alumno.FechaCompletaAlta = DateTime.Now;
                alumno.Edad = CalcularEdad(DateTime.Now, alumno.FechaNacimiento);
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return ficheroAlumno.Add(alumno);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        public int CalcularEdad(DateTime fechaCompletaActual, DateTime fechaNacimiento)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return (Convert.ToInt32((fechaCompletaActual - fechaNacimiento).TotalDays) / 365);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        } 

        public void SeleccionarTipoFichero(Extension extension)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                FicheroFactory ficheroFactory = new FicheroFactory();
                ficheroAlumno = ficheroFactory.CrearFichero(extension);
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
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
                List<Alumno> alumnos = ficheroAlumno.GetAll();
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
                List<Alumno> alumnos = ficheroAlumno.GetSingletonInstance();
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return alumnos;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

      
        public List<Alumno> Filter(string guid, string nombre, string apellidos, string dni, string id, DateTime dtFechaNacimiento,bool fechaNacimientoChecked, string edad, DateTime dtFechaRegistro, bool fechaRegistroChecked)
        {
            try
            {
                List<Alumno> alumnos = GetSingletonInstance();
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                int idInt, edadInt;
                var query = from alu in alumnos select alu;
                if (!String.IsNullOrEmpty(guid))
                    query = query.Where(alu => alu.GUID.Equals(Guid.Parse(guid)));
                if (!String.IsNullOrEmpty(nombre))
                    query = query.Where(alu => alu.Nombre.Equals(nombre));
                if (!String.IsNullOrEmpty(apellidos))
                    query = query.Where(alu => alu.Apellidos.Equals(apellidos));
                if (!String.IsNullOrEmpty(dni))
                    query = query.Where(alu => alu.DNI.Equals(dni));
                if (!String.IsNullOrEmpty(id))
                {
                    idInt = Convert.ToInt32(id);
                    query = query.Where(alu => alu.ID.Equals(idInt));
                }
                if (fechaNacimientoChecked)
                    query = query.Where(alu => alu.FechaNacimiento.Date.Equals(dtFechaNacimiento.Date));
                if (!String.IsNullOrEmpty(edad))
                {
                    edadInt = Convert.ToInt32(edad);
                    query = query.Where(alu => alu.Edad.Equals(edadInt));
                }
                if (fechaRegistroChecked)
                    query = query.Where(alu => alu.FechaCompletaAlta.Date.Equals(dtFechaRegistro.Date));

                query = query.OrderBy(alu => alu.ID);

                var result = query.ToList();
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return result;
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
                List<Alumno> alumnos = ficheroAlumno.DeleteByGuid(guid);
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return alumnos;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        public Alumno SelectByGuid(string guid)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                Alumno alumno = ficheroAlumno.Select(new Guid(guid));
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
                ficheroAlumno.Update(alumno);
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
