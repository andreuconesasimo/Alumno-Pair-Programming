namespace Vueling.Presentation.Winsite
{
    partial class AlumnoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlumnoForm));
            this.textBoxNombre = new System.Windows.Forms.TextBox();
            this.textBoxDNI = new System.Windows.Forms.TextBox();
            this.textBoxApellidos = new System.Windows.Forms.TextBox();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.buttonText = new System.Windows.Forms.Button();
            this.buttonJson = new System.Windows.Forms.Button();
            this.buttonXML = new System.Windows.Forms.Button();
            this.labelID = new System.Windows.Forms.Label();
            this.labelNombre = new System.Windows.Forms.Label();
            this.labelApellidos = new System.Windows.Forms.Label();
            this.labelDNI = new System.Windows.Forms.Label();
            this.labelNacimiento = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnStoredProcedure = new System.Windows.Forms.Button();
            this.buttonSQL = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttonMostrarAlumnos = new System.Windows.Forms.Button();
            this.textBoxNacimiento = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxNombre
            // 
            resources.ApplyResources(this.textBoxNombre, "textBoxNombre");
            this.textBoxNombre.Name = "textBoxNombre";
            // 
            // textBoxDNI
            // 
            resources.ApplyResources(this.textBoxDNI, "textBoxDNI");
            this.textBoxDNI.Name = "textBoxDNI";
            // 
            // textBoxApellidos
            // 
            resources.ApplyResources(this.textBoxApellidos, "textBoxApellidos");
            this.textBoxApellidos.Name = "textBoxApellidos";
            // 
            // textBoxID
            // 
            resources.ApplyResources(this.textBoxID, "textBoxID");
            this.textBoxID.Name = "textBoxID";
            // 
            // buttonText
            // 
            resources.ApplyResources(this.buttonText, "buttonText");
            this.buttonText.Name = "buttonText";
            this.buttonText.UseVisualStyleBackColor = true;
            this.buttonText.Click += new System.EventHandler(this.buttonTxt_Click);
            // 
            // buttonJson
            // 
            resources.ApplyResources(this.buttonJson, "buttonJson");
            this.buttonJson.Name = "buttonJson";
            this.buttonJson.UseVisualStyleBackColor = true;
            this.buttonJson.Click += new System.EventHandler(this.buttonJson_Click);
            // 
            // buttonXML
            // 
            resources.ApplyResources(this.buttonXML, "buttonXML");
            this.buttonXML.Name = "buttonXML";
            this.buttonXML.UseVisualStyleBackColor = true;
            this.buttonXML.Click += new System.EventHandler(this.buttonXML_Click);
            // 
            // labelID
            // 
            resources.ApplyResources(this.labelID, "labelID");
            this.labelID.Name = "labelID";
            // 
            // labelNombre
            // 
            resources.ApplyResources(this.labelNombre, "labelNombre");
            this.labelNombre.Name = "labelNombre";
            // 
            // labelApellidos
            // 
            resources.ApplyResources(this.labelApellidos, "labelApellidos");
            this.labelApellidos.Name = "labelApellidos";
            // 
            // labelDNI
            // 
            resources.ApplyResources(this.labelDNI, "labelDNI");
            this.labelDNI.Name = "labelDNI";
            // 
            // labelNacimiento
            // 
            resources.ApplyResources(this.labelNacimiento, "labelNacimiento");
            this.labelNacimiento.Name = "labelNacimiento";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.btnStoredProcedure);
            this.panel1.Controls.Add(this.buttonSQL);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.buttonMostrarAlumnos);
            this.panel1.Controls.Add(this.textBoxNacimiento);
            this.panel1.Controls.Add(this.labelNacimiento);
            this.panel1.Controls.Add(this.labelDNI);
            this.panel1.Controls.Add(this.labelApellidos);
            this.panel1.Controls.Add(this.labelNombre);
            this.panel1.Controls.Add(this.labelID);
            this.panel1.Controls.Add(this.buttonXML);
            this.panel1.Controls.Add(this.buttonJson);
            this.panel1.Controls.Add(this.buttonText);
            this.panel1.Controls.Add(this.textBoxID);
            this.panel1.Controls.Add(this.textBoxApellidos);
            this.panel1.Controls.Add(this.textBoxDNI);
            this.panel1.Controls.Add(this.textBoxNombre);
            this.panel1.Name = "panel1";
            // 
            // btnStoredProcedure
            // 
            resources.ApplyResources(this.btnStoredProcedure, "btnStoredProcedure");
            this.btnStoredProcedure.Name = "btnStoredProcedure";
            this.btnStoredProcedure.UseVisualStyleBackColor = true;
            this.btnStoredProcedure.Click += new System.EventHandler(this.btnStoredProcedure_Click);
            // 
            // buttonSQL
            // 
            resources.ApplyResources(this.buttonSQL, "buttonSQL");
            this.buttonSQL.Name = "buttonSQL";
            this.buttonSQL.UseVisualStyleBackColor = true;
            this.buttonSQL.Click += new System.EventHandler(this.buttonSQL_Click);
            // 
            // comboBox1
            // 
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            resources.GetString("comboBox1.Items"),
            resources.GetString("comboBox1.Items1"),
            resources.GetString("comboBox1.Items2")});
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // buttonMostrarAlumnos
            // 
            resources.ApplyResources(this.buttonMostrarAlumnos, "buttonMostrarAlumnos");
            this.buttonMostrarAlumnos.Name = "buttonMostrarAlumnos";
            this.buttonMostrarAlumnos.UseVisualStyleBackColor = true;
            this.buttonMostrarAlumnos.Click += new System.EventHandler(this.buttonMostrarAlumnos_Click);
            // 
            // textBoxNacimiento
            // 
            resources.ApplyResources(this.textBoxNacimiento, "textBoxNacimiento");
            this.textBoxNacimiento.Name = "textBoxNacimiento";
            // 
            // AlumnoForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "AlumnoForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxNombre;
        private System.Windows.Forms.TextBox textBoxDNI;
        private System.Windows.Forms.TextBox textBoxApellidos;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Button buttonText;
        private System.Windows.Forms.Button buttonJson;
        private System.Windows.Forms.Button buttonXML;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.Label labelApellidos;
        private System.Windows.Forms.Label labelDNI;
        private System.Windows.Forms.Label labelNacimiento;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker textBoxNacimiento;
        private System.Windows.Forms.Button buttonMostrarAlumnos;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button buttonSQL;
        private System.Windows.Forms.Button btnStoredProcedure;
    }
}