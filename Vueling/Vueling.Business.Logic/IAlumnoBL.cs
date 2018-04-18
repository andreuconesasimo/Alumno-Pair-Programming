using System;
using System.Collections.Generic;
using Vueling.Common.Logic.Enums;
using Vueling.Common.Logic.Model;

namespace Vueling.Business.Logic
{
    public interface IAlumnoBL
    {
        Alumno Add(Alumno alumno);
        void SeleccionarTipoFichero(Extension extension);
        int CalcularEdad(DateTime fechaCompletaActual, DateTime fechaNacimiento);
        List<Alumno> GetAll();
        List<Alumno> GetSingletonInstance();
        List<Alumno> Filter(string guid, string nombre, string apellidos, string dni, string id, DateTime dtFechaNacimiento, bool fechaNacimientoChecked, string edad, DateTime dtFechaRegistro, bool fechaRegistroChecked);
        List<Alumno> DeleteByGuid(string guid);
        Alumno SelectByGuid(string guid);
        void Update(Alumno alumno);
    }
}