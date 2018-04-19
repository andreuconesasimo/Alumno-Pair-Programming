using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Vueling.Business.Logic;
using Vueling.Common.Logic;
using Vueling.Common.Logic.Enums;
using Vueling.Common.Logic.Helpers;
using Vueling.Common.Logic.Model;
using Vueling.Common.Logic.Properties;

namespace Vueling.Presentation.Winsite
{
    public partial class AlumnosShowForm : Form
    {
        private IAlumnoBL alumnoBL;        
        private List<Alumno> alumnos;

        ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public AlumnosShowForm()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);                
                InitializeComponent();
                alumnoBL = new AlumnoBL();                
                CargarDatosTxt();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                ExceptionMessage.Show(ex);
            }
        }
                
        private void CargarDatosTxt()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                buttonTxt_Click(null, null);
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);                
            }
        }

        private void CargarGrid(List<Alumno> alumnos)
        {            
            var source = new BindingSource();
            source.DataSource = alumnos;
            dataGridAlumnos.DataSource = source;
        }

        private void buttonJson_Click(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;
                logger.Debug(button.Name + " " + LogStrings.Clicked);
                alumnoBL.SeleccionarTipoFichero(Extension.JSON);
                alumnos = alumnoBL.GetSingletonInstance();
                CargarGrid(alumnos);       
                logger.Debug(button.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                ExceptionMessage.Show(ex);
            }
            
        }
               
        private void buttonXml_Click(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;
                logger.Debug(button.Name + " " + LogStrings.Clicked);
                alumnoBL.SeleccionarTipoFichero(Extension.XML);
                alumnos = alumnoBL.GetSingletonInstance();
                CargarGrid(alumnos);
                logger.Debug(button.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                ExceptionMessage.Show(ex);
            }
        }

        private void buttonTxt_Click(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;
                logger.Debug(button.Name + " " + LogStrings.Clicked);
                alumnoBL.SeleccionarTipoFichero(Extension.TXT);
                alumnos = alumnoBL.GetAll();
                CargarGrid(alumnos);
                logger.Debug(button.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                ExceptionMessage.Show(ex);
            }
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;
                logger.Debug(button.Name + " " + LogStrings.Clicked);                
                List<Alumno> result = alumnoBL.Filter(txtGuid.Text, txtNombre.Text, txtApellidos.Text,
                    txtDni.Text, txtId.Text, dtpFechaNacimiento.Value, chckBxFechaNacimiento.Checked,
                    txtEdad.Text, dtpFechaRegistro.Value, chckBxFechaRegistro.Checked);
                CargarGrid(result); 
                logger.Debug(button.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                ExceptionMessage.Show(ex);
            }
        }

        private void chckBxFechaRegistro_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox checkBox = (CheckBox)sender;
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);                
                if (checkBox.Checked) dtpFechaRegistro.Enabled = true;
                else dtpFechaRegistro.Enabled = false;
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                ExceptionMessage.Show(ex);
            }
        }

        private void chckBxFechaNacimiento_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                CheckBox checkBox = (CheckBox)sender;
                if (checkBox.Checked) dtpFechaNacimiento.Enabled = true;
                else dtpFechaNacimiento.Enabled = false;
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                ExceptionMessage.Show(ex);
            }
        }

        private void buttonSql_Click(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;
                logger.Debug(button.Name + " " + LogStrings.Clicked);
                alumnoBL.SeleccionarTipoFichero(Extension.SQL);
                alumnos = alumnoBL.GetAll();
                CargarGrid(alumnos);
                logger.Debug(button.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                ExceptionMessage.Show(ex);
            }
        }

        private void buttonBorrar_Click(object sender, EventArgs e)
        {

            try
            {
                string guid = txtBoxGuidBorrar.Text;
                var button = (Button)sender;
                logger.Debug(button.Name + " " + LogStrings.Clicked);
                alumnos = alumnoBL.DeleteByGuid(guid);
                CargarGrid(alumnos);
                logger.Debug(button.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                ExceptionMessage.Show(ex);
            }
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            try
            {
                string guid = txtBoxGuidBorrar.Text;
                var button = (Button)sender;
                logger.Debug(button.Name + " " + LogStrings.Clicked);
                Alumno alumno = alumnoBL.SelectByGuid(guid);
                Alumno aux = new Alumno();
                if (!alumno.Equals(aux))
                {
                    panelEdicion.Visible = true;
                    CargarDatosEdicion(alumno);
                }                
                logger.Debug(button.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                ExceptionMessage.Show(ex);
            }
        }

        private void CargarDatosEdicion(Alumno alumno)
        {
            txtGuidEdicion.Text = alumno.GUID.ToString();
            txtNombreEdicion.Text = alumno.Nombre;
            txtApellidosEdicion.Text = alumno.Apellidos;
            txtDNIEdicion.Text = alumno.DNI;
            dtpFechNacimientoEdicion.Value = alumno.FechaNacimiento;             
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtGuidEdicion.Text.Equals(""))
                {
                    
                    var button = (Button)sender;
                    logger.Debug(button.Name + " " + LogStrings.Clicked);
                    Alumno alumno = new Alumno(txtGuidEdicion.Text, txtNombreEdicion.Text, txtApellidosEdicion.Text, txtDNIEdicion.Text, dtpFechNacimientoEdicion.Value);
                    alumnoBL.Update(alumno);
                    CargarGrid(alumnoBL.GetAll());
                    alumno = alumnoBL.SelectByGuid(txtGuidEdicion.Text);
                    MessageBox.Show(InfoStrings.ResourceManager.GetString("UpdatedStudent") + Environment.NewLine + alumno.ToString());
                    logger.Debug(button.Name + " " + LogStrings.Ends);
                }
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                ExceptionMessage.Show(ex);
            }
        }

        private void btnStoredProcedure_Click(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;
                logger.Debug(button.Name + " " + LogStrings.Clicked);
                alumnoBL.SeleccionarTipoFichero(Extension.SP);
                alumnos = alumnoBL.GetAll();
                CargarGrid(alumnos);
                logger.Debug(button.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                ExceptionMessage.Show(ex);
            }
        }
    }
}
