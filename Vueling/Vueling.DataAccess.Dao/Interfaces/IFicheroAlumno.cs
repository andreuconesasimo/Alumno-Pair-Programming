using System;
using System.Collections.Generic;
using Vueling.Common.Logic.Model;

namespace Vueling.DataAccess.Dao.Interfaces
{
    public interface IFicheroAlumno
    {
        Alumno Add(Alumno alumno);
        Alumno Select(Guid guid);
        List<Alumno> GetAll();
        List<Alumno> GetSingletonInstance();
        List<Alumno> DeleteByGuid(string guid);
        void Update(Alumno alumno);
    }
}