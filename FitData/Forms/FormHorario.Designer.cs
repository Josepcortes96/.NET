namespace FitData.Forms
{
    partial class FormHorario
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblActividad;
        private System.Windows.Forms.ComboBox cmbActividad;
        private System.Windows.Forms.Label lblDia;
        private System.Windows.Forms.TextBox txtDia;
        private System.Windows.Forms.Label lblInicio;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.Label lblFin;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.Label lblPlazas;
        private System.Windows.Forms.NumericUpDown nudPlazas;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblActividad = new System.Windows.Forms.Label();
            this.cmbActividad = new System.Windows.Forms.ComboBox();
            this.lblDia = new System.Windows.Forms.Label();
            this.txtDia = new System.Windows.Forms.TextBox();
            this.lblInicio = new System.Windows.Forms.Label();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.lblFin = new System.Windows.Forms.Label();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.lblPlazas = new System.Windows.Forms.Label();
            this.nudPlazas = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlazas)).BeginInit();
            this.SuspendLayout();
            //
            // lblActividad
            //
            this.lblActividad.Location = new System.Drawing.Point(12, 15);
            this.lblActividad.Size = new System.Drawing.Size(80, 23);
            this.lblActividad.Text = "Actividad:";
            //
            // cmbActividad
            //
            this.cmbActividad.Location = new System.Drawing.Point(100, 12);
            this.cmbActividad.Size = new System.Drawing.Size(260, 23);
            //
            // lblDia
            //
            this.lblDia.Location = new System.Drawing.Point(12, 50);
            this.lblDia.Size = new System.Drawing.Size(80, 23);
            this.lblDia.Text = "Día semana:";
            //
            // txtDia
            //
            this.txtDia.Location = new System.Drawing.Point(100, 47);
            this.txtDia.Size = new System.Drawing.Size(260, 23);
            //
            // lblInicio
            //
            this.lblInicio.Location = new System.Drawing.Point(12, 85);
            this.lblInicio.Size = new System.Drawing.Size(80, 23);
            this.lblInicio.Text = "Hora inicio:";
            //
            // dtpInicio
            //
            this.dtpInicio.Location = new System.Drawing.Point(100, 82);
            this.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpInicio.ShowUpDown = true;
            //
            // lblFin
            //
            this.lblFin.Location = new System.Drawing.Point(12, 120);
            this.lblFin.Size = new System.Drawing.Size(80, 23);
            this.lblFin.Text = "Hora fin:";
            //
            // dtpFin
            //
            this.dtpFin.Location = new System.Drawing.Point(100, 117);
            this.dtpFin.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpFin.ShowUpDown = true;
            //
            // lblPlazas
            //
            this.lblPlazas.Location = new System.Drawing.Point(12, 155);
            this.lblPlazas.Size = new System.Drawing.Size(80, 23);
            this.lblPlazas.Text = "Plazas:";
            //
            // nudPlazas
            //
            this.nudPlazas.Location = new System.Drawing.Point(100, 152);
            this.nudPlazas.Minimum = 1;
            this.nudPlazas.Maximum = 100;
            //
            // btnSave
            //
            this.btnSave.Location = new System.Drawing.Point(100, 190);
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.Text = "Guardar";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnCancel
            //
            this.btnCancel.Location = new System.Drawing.Point(210, 190);
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // FormHorario
            //
            this.ClientSize = new System.Drawing.Size(380, 240);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.nudPlazas);
            this.Controls.Add(this.lblPlazas);
            this.Controls.Add(this.dtpFin);
            this.Controls.Add(this.lblFin);
            this.Controls.Add(this.dtpInicio);
            this.Controls.Add(this.lblInicio);
            this.Controls.Add(this.txtDia);
            this.Controls.Add(this.lblDia);
            this.Controls.Add(this.cmbActividad);
            this.Controls.Add(this.lblActividad);
            this.Text = "Horario";
            ((System.ComponentModel.ISupportInitialize)(this.nudPlazas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

