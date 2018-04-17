using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    public partial class AlumnoForm : Form
    {
        private Alumno alumno;
        private IAlumnoBL alumnoBL;
        ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public AlumnoForm()
        {
            try
            {               
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                Language.InitializeLanguage();
                InitializeComponent();   
                alumno = new Alumno();
                alumnoBL = new AlumnoBL();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);                
                MessageBox.Show(ex.Message);
            }
        }
        
        private void buttonTxt_Click(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;
                logger.Debug(button.Name + " " + LogStrings.Clicked);
                LoadAlumnoData();
                alumnoBL.SeleccionarTipoFichero(Extension.TXT);
                alumnoBL.Add(alumno);
                logger.Debug(button.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                ExceptionMessage.Show(ex);
            }
        }

        private void buttonJson_Click(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;
                logger.Debug(button.Name + " " + LogStrings.Clicked);
                LoadAlumnoData();
                alumnoBL.SeleccionarTipoFichero(Extension.JSON);
                alumnoBL.Add(alumno);
                logger.Debug(button.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                ExceptionMessage.Show(ex);
            }
        }

        private void buttonXML_Click(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;
                logger.Debug(button.Name + " " + LogStrings.Clicked);
                LoadAlumnoData();
                alumnoBL.SeleccionarTipoFichero(Extension.XML);
                alumnoBL.Add(alumno);
                logger.Debug(button.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                ExceptionMessage.Show(ex);
            }
        }

        private void LoadAlumnoData()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                alumno.SetGuid();
                alumno.ID = Convert.ToInt32(textBoxID.Text);
                alumno.Nombre = textBoxNombre.Text;
                alumno.Apellidos = textBoxApellidos.Text;
                alumno.DNI = textBoxDNI.Text;
                alumno.FechaNacimiento = textBoxNacimiento.Value.Date;
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);                
            }
            
        }

        private void buttonMostrarAlumnos_Click(object sender, EventArgs e)
        {
            try
            {
                             
                AlumnosShowForm alumnosShowForm = new AlumnosShowForm();
                logger.Debug(LogStrings.Show + " " + alumnosShowForm.GetType().Name);
                alumnosShowForm.ShowDialog();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                ExceptionMessage.Show(ex);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox box = (ComboBox)sender;
                Idioma idioma = (Idioma)box.SelectedIndex;
                switch (idioma)
                {
                    case Idioma.Catalan:
                        Language.ChangeLanguage(ConfigStrings.Catalan);
                        break;
                    case Idioma.English:
                        Language.ChangeLanguage(ConfigStrings.English);
                        break;
                    case Idioma.Spanish:
                        Language.ChangeLanguage(ConfigStrings.Spanish);
                        break;
                }
                UpdateControls();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                ExceptionMessage.Show(ex);
            }
        }

        private void UpdateControls()
        {            
            var resources = new ComponentResourceManager(this.GetType());            
            GetChildren(this).ToList().ForEach(c =>
            {
                resources.ApplyResources(c, c.Name);
            });            
            this.Text = resources.GetString("$this.Text");
        }
        public IEnumerable<Control> GetChildren(Control control)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetChildren(ctrl)).Concat(controls);
        }
        public IEnumerable<Control> GetParent(Control control)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetParent(ctrl)).Concat(controls);
        }

        private void buttonSQL_Click(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;
                logger.Debug(button.Name + " " + LogStrings.Clicked);
                LoadAlumnoData();
                alumnoBL.SeleccionarTipoFichero(Extension.SQL);
                alumnoBL.Add(alumno);
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
