// FitData/Forms/FormHorario.Designer.cs
namespace FitData.Forms
{
    partial class FormHorario
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cmbActividad;
        private System.Windows.Forms.TextBox txtDiaSemana;
        private System.Windows.Forms.DateTimePicker dtpHoraInicio;
        private System.Windows.Forms.DateTimePicker dtpHoraFin;
        private System.Windows.Forms.NumericUpDown nudPlazas;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblActividad;
        private System.Windows.Forms.Label lblDia;
        private System.Windows.Forms.Label lblInicio;
        private System.Windows.Forms.Label lblFin;
        private System.Windows.Forms.Label lblPlazas;
        private System.Windows.Forms.TextBox txtSala;
        private System.Windows.Forms.Label lblSala;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbActividad = new System.Windows.Forms.ComboBox();
            this.txtDiaSemana = new System.Windows.Forms.TextBox();
            this.dtpHoraInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpHoraFin = new System.Windows.Forms.DateTimePicker();
            this.nudPlazas = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblActividad = new System.Windows.Forms.Label();
            this.lblDia = new System.Windows.Forms.Label();
            this.lblInicio = new System.Windows.Forms.Label();
            this.lblFin = new System.Windows.Forms.Label();
            this.lblPlazas = new System.Windows.Forms.Label();
            this.txtSala = new System.Windows.Forms.TextBox();
            this.lblSala = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudPlazas)).BeginInit();
            this.SuspendLayout();
            // lblActividad
            this.lblActividad.Location = new System.Drawing.Point(12, 15);
            this.lblActividad.Size = new System.Drawing.Size(80, 23);
            this.lblActividad.Text = "Actividad:";
            this.cmbActividad.Location = new System.Drawing.Point(100, 12);
            this.cmbActividad.Size = new System.Drawing.Size(260, 23);
            // lblDia
            this.lblDia.Location = new System.Drawing.Point(12, 50);
            this.lblDia.Size = new System.Drawing.Size(80, 23);
            this.lblDia.Text = "Día semana:";
            this.txtDiaSemana.Location = new System.Drawing.Point(100, 47);
            this.txtDiaSemana.Size = new System.Drawing.Size(260, 23);
            // lblInicio
            this.lblInicio.Location = new System.Drawing.Point(12, 85);
            this.lblInicio.Size = new System.Drawing.Size(80, 23);
            this.lblInicio.Text = "Hora inicio:";
            this.dtpHoraInicio.Location = new System.Drawing.Point(100, 82);
            this.dtpHoraInicio.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraInicio.ShowUpDown = true;
            // lblFin
            this.lblFin.Location = new System.Drawing.Point(12, 120);
            this.lblFin.Size = new System.Drawing.Size(80, 23);
            this.lblFin.Text = "Hora fin:";
            this.dtpHoraFin.Location = new System.Drawing.Point(100, 117);
            this.dtpHoraFin.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraFin.ShowUpDown = true;
            // lblSala
            this.lblSala.Location = new System.Drawing.Point(12, 155);
            this.lblSala.Size = new System.Drawing.Size(80, 23);
            this.lblSala.Text = "Sala:";
            this.txtSala.Location = new System.Drawing.Point(100, 152);
            this.txtSala.Size = new System.Drawing.Size(260, 23);
            // lblPlazas
            this.lblPlazas.Location = new System.Drawing.Point(12, 190);
            this.lblPlazas.Size = new System.Drawing.Size(80, 23);
            this.lblPlazas.Text = "Plazas:";
            this.nudPlazas.Location = new System.Drawing.Point(100, 187);
            this.nudPlazas.Minimum = 1;
            this.nudPlazas.Maximum = 100;
            this.nudPlazas.Value = 16;
            // btnSave
            this.btnSave.Location = new System.Drawing.Point(100, 230);
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.Text = "Guardar";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(210, 230);
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // FormHorario
            this.ClientSize = new System.Drawing.Size(380, 280);
            this.Controls.Add(this.lblActividad);
            this.Controls.Add(this.cmbActividad);
            this.Controls.Add(this.lblDia);
            this.Controls.Add(this.txtDiaSemana);
            this.Controls.Add(this.lblInicio);
            this.Controls.Add(this.dtpHoraInicio);
            this.Controls.Add(this.lblFin);
            this.Controls.Add(this.dtpHoraFin);
            this.Controls.Add(this.lblSala);
            this.Controls.Add(this.txtSala);
            this.Controls.Add(this.lblPlazas);
            this.Controls.Add(this.nudPlazas);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Text = "Horario";
            ((System.ComponentModel.ISupportInitialize)(this.nudPlazas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
