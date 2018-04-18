using System;

namespace Vueling.Common.Logic.Model
{
    public class Alumno : Persona
    {
        public Alumno()
        {
        }

        public Alumno(int iD, string nombre, string apellidos, string dNI, DateTime fechaNacimiento, int edad, DateTime fechaCompletaAlta) : base(iD, nombre, apellidos, dNI, fechaNacimiento, edad, fechaCompletaAlta)
        {
        }

        public Alumno(Guid gUID, int iD, string nombre, string apellidos, string dNI, DateTime fechaNacimiento, int edad, DateTime fechaCompletaAlta) : base(gUID, iD, nombre, apellidos, dNI, fechaNacimiento, edad, fechaCompletaAlta)
        {
        }

        public Alumno(int iD, string nombre, string apellidos, string dNI, DateTime fechaNacimiento) : base(iD, nombre, apellidos, dNI, fechaNacimiento)
        {
        }

        public Alumno(string guid, string nombre, string apellidos, string dni, DateTime fechaNacimiento) : base(guid, nombre, apellidos, dni, fechaNacimiento)
        {
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
