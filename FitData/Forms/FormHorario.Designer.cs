namespace FitData.Forms
{
    partial class FormHorario
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblActividad;
        private System.Windows.Forms.Label lblDiaSemana;
        private System.Windows.Forms.Label lblHoraInicio;
        private System.Windows.Forms.Label lblHoraFin;
        private System.Windows.Forms.Label lblSala;
        private System.Windows.Forms.Label lblPlazasTotales;
        private System.Windows.Forms.Label lblPlazasOcupadas;
        private System.Windows.Forms.TextBox txtDiaSemana;
        private System.Windows.Forms.TextBox txtSala;
        private System.Windows.Forms.DateTimePicker dtpHoraInicio;
        private System.Windows.Forms.DateTimePicker dtpHoraFin;
        private System.Windows.Forms.NumericUpDown numPlazasTotales;
        private System.Windows.Forms.NumericUpDown numPlazasOcupadas;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblActividad = new System.Windows.Forms.Label();
            this.lblDiaSemana = new System.Windows.Forms.Label();
            this.lblHoraInicio = new System.Windows.Forms.Label();
            this.lblHoraFin = new System.Windows.Forms.Label();
            this.lblSala = new System.Windows.Forms.Label();
            this.lblPlazasTotales = new System.Windows.Forms.Label();
            this.lblPlazasOcupadas = new System.Windows.Forms.Label();
            this.txtDiaSemana = new System.Windows.Forms.TextBox();
            this.txtSala = new System.Windows.Forms.TextBox();
            this.dtpHoraInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpHoraFin = new System.Windows.Forms.DateTimePicker();
            this.numPlazasTotales = new System.Windows.Forms.NumericUpDown();
            this.numPlazasOcupadas = new System.Windows.Forms.NumericUpDown();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.numPlazasTotales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPlazasOcupadas)).BeginInit();

            this.SuspendLayout();

            // lblActividad
            this.lblActividad.AutoSize = true;
            this.lblActividad.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblActividad.Location = new System.Drawing.Point(20, 20);
            this.lblActividad.Text = "Actividad:";

            // Día de la semana
            this.lblDiaSemana.AutoSize = true;
            this.lblDiaSemana.Location = new System.Drawing.Point(20, 60);
            this.lblDiaSemana.Text = "Día de la semana:";
            this.txtDiaSemana.Location = new System.Drawing.Point(180, 57);
            this.txtDiaSemana.Width = 150;

            // Hora inicio
            this.lblHoraInicio.AutoSize = true;
            this.lblHoraInicio.Location = new System.Drawing.Point(20, 100);
            this.lblHoraInicio.Text = "Hora inicio:";
            this.dtpHoraInicio.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraInicio.ShowUpDown = true;
            this.dtpHoraInicio.Location = new System.Drawing.Point(180, 97);

            // Hora fin
            this.lblHoraFin.AutoSize = true;
            this.lblHoraFin.Location = new System.Drawing.Point(20, 140);
            this.lblHoraFin.Text = "Hora fin:";
            this.dtpHoraFin.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraFin.ShowUpDown = true;
            this.dtpHoraFin.Location = new System.Drawing.Point(180, 137);

            // Sala
            this.lblSala.AutoSize = true;
            this.lblSala.Location = new System.Drawing.Point(20, 180);
            this.lblSala.Text = "Sala:";
            this.txtSala.Location = new System.Drawing.Point(180, 177);
            this.txtSala.Width = 150;

            // Plazas totales
            this.lblPlazasTotales.AutoSize = true;
            this.lblPlazasTotales.Location = new System.Drawing.Point(20, 220);
            this.lblPlazasTotales.Text = "Plazas totales:";
            this.numPlazasTotales.Location = new System.Drawing.Point(180, 217);
            this.numPlazasTotales.Maximum = 100;

            // Plazas ocupadas
            this.lblPlazasOcupadas.AutoSize = true;
            this.lblPlazasOcupadas.Location = new System.Drawing.Point(20, 260);
            this.lblPlazasOcupadas.Text = "Plazas ocupadas:";
            this.numPlazasOcupadas.Location = new System.Drawing.Point(180, 257);
            this.numPlazasOcupadas.Maximum = 100;

            // Botones
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Location = new System.Drawing.Point(60, 310);
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Location = new System.Drawing.Point(200, 310);
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // FormHorario
            this.ClientSize = new System.Drawing.Size(380, 370);
            this.Controls.Add(this.lblActividad);
            this.Controls.Add(this.lblDiaSemana);
            this.Controls.Add(this.txtDiaSemana);
            this.Controls.Add(this.lblHoraInicio);
            this.Controls.Add(this.dtpHoraInicio);
            this.Controls.Add(this.lblHoraFin);
            this.Controls.Add(this.dtpHoraFin);
            this.Controls.Add(this.lblSala);
            this.Controls.Add(this.txtSala);
            this.Controls.Add(this.lblPlazasTotales);
            this.Controls.Add(this.numPlazasTotales);
            this.Controls.Add(this.lblPlazasOcupadas);
            this.Controls.Add(this.numPlazasOcupadas);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Name = "FormHorario";
            this.Text = "Gestión de horarios";

            ((System.ComponentModel.ISupportInitialize)(this.numPlazasTotales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPlazasOcupadas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}