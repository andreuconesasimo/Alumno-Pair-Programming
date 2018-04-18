using System;
using System.Collections.Generic;

namespace Vueling.Common.Logic.Model
{
    public abstract class Persona : IVuelingObject
    {
        #region Propiedades
        public Guid GUID { get; set; }
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string DNI { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public DateTime FechaCompletaAlta { get; set; }
        #endregion


        #region Constructores
        protected Persona() => GUID = Guid.NewGuid();

        protected Persona(Guid gUID, int iD, string nombre, string apellidos, string dNI, DateTime fechaNacimiento, int edad, DateTime fechaCompletaAlta)
        {
            GUID = gUID;
            ID = iD;
            Nombre = nombre;
            Apellidos = apellidos;
            DNI = dNI;
            FechaNacimiento = fechaNacimiento;
            Edad = edad;
            FechaCompletaAlta = fechaCompletaAlta;
        }

        protected Persona(int iD, string nombre, string apellidos, string dNI, DateTime fechaNacimiento, int edad, DateTime fechaCompletaAlta)
        {
            GUID = Guid.NewGuid();
            ID = iD;
            Nombre = nombre;
            Apellidos = apellidos;
            DNI = dNI;
            FechaNacimiento = fechaNacimiento;
            Edad = edad;
            FechaCompletaAlta = fechaCompletaAlta;
        }

        protected Persona(int iD, string nombre, string apellidos, string dNI, DateTime fechaNacimiento)
        {
            ID = iD;
            Nombre = nombre;
            Apellidos = apellidos;
            DNI = dNI;
            FechaNacimiento = fechaNacimiento;
        }

        protected Persona(string guid, string nombre, string apellidos, string dni, DateTime fechaNacimiento)
        {
            GUID = new Guid(guid);
            Nombre = nombre;
            Apellidos = apellidos;
            DNI = dni;
            FechaNacimiento = fechaNacimiento;
        }
        #endregion

        public void SetGuid()
        {
            GUID = Guid.NewGuid();
        }

        public override int GetHashCode()
        {
            var hashCode = 564319517;
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(GUID);
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nombre);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Apellidos);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DNI);
            hashCode = hashCode * -1521134295 + FechaNacimiento.GetHashCode();
            hashCode = hashCode * -1521134295 + Edad.GetHashCode();
            hashCode = hashCode * -1521134295 + FechaCompletaAlta.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return string.Concat(GUID.ToString(), ",",
                                ID.ToString(), ",",
                                Nombre, ",",
                                Apellidos, ",",
                                DNI, ",",
                                FechaNacimiento, ",",
                                Edad.ToString(), ",",
                                FechaCompletaAlta.ToString());
        }

        public override bool Equals(object obj)
        {
            var persona = obj as Persona;
            return persona != null &&
                   GUID.Equals(persona.GUID) &&
                   ID == persona.ID &&
                   Nombre == persona.Nombre &&
                   Apellidos == persona.Apellidos &&
                   DNI == persona.DNI &&
                   FechaNacimiento.Date == persona.FechaNacimiento.Date &&
                   Edad == persona.Edad &&
                   FechaCompletaAlta.Date == persona.FechaCompletaAlta.Date;
        }
    }
}