namespace FitData.Forms
{
    partial class FormCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.ComboBox comboActividades;
        private System.Windows.Forms.Button btnVerHorarios;
        private System.Windows.Forms.Button btnReservar;
        private System.Windows.Forms.Button btnVerReservas;
        private System.Windows.Forms.Button btnCancelarReserva;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.DataGridView dataGridViewHorarios;

        /// <summary>
        /// Clean up resources.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                _ctx?.Dispose();
                components.Dispose();

            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.comboActividades = new System.Windows.Forms.ComboBox();
            this.btnVerHorarios = new System.Windows.Forms.Button();
            this.btnReservar = new System.Windows.Forms.Button();
            this.btnVerReservas = new System.Windows.Forms.Button();
            this.btnCancelarReserva = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.dataGridViewHorarios = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHorarios)).BeginInit();
            this.SuspendLayout();

            // lblBienvenida
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBienvenida.Location = new System.Drawing.Point(20, 20);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(160, 23);
            this.lblBienvenida.Text = "Bienvenido, Cliente";

            // comboActividades
            this.comboActividades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboActividades.Location = new System.Drawing.Point(20, 60);
            this.comboActividades.Name = "comboActividades";
            this.comboActividades.Size = new System.Drawing.Size(250, 28);

            // btnVerHorarios
            this.btnVerHorarios.Location = new System.Drawing.Point(290, 60);
            this.btnVerHorarios.Name = "btnVerHorarios";
            this.btnVerHorarios.Size = new System.Drawing.Size(120, 28);
            this.btnVerHorarios.Text = "Ver horarios";
            this.btnVerHorarios.Click += new System.EventHandler(this.btnVerHorarios_Click);

            // dataGridViewHorarios
            this.dataGridViewHorarios.AllowUserToAddRows = false;
            this.dataGridViewHorarios.AllowUserToDeleteRows = false;
            this.dataGridViewHorarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewHorarios.Location = new System.Drawing.Point(20, 110);
            this.dataGridViewHorarios.Name = "dataGridViewHorarios";
            this.dataGridViewHorarios.ReadOnly = true;
            this.dataGridViewHorarios.RowTemplate.Height = 29;
            this.dataGridViewHorarios.Size = new System.Drawing.Size(600, 220);

            // btnReservar
            this.btnReservar.Location = new System.Drawing.Point(20, 350);
            this.btnReservar.Name = "btnReservar";
            this.btnReservar.Size = new System.Drawing.Size(100, 30);
            this.btnReservar.Text = "Reservar";
            this.btnReservar.Click += new System.EventHandler(this.btnReservar_Click);

            // btnVerReservas
            this.btnVerReservas.Location = new System.Drawing.Point(130, 350);
            this.btnVerReservas.Name = "btnVerReservas";
            this.btnVerReservas.Size = new System.Drawing.Size(120, 30);
            this.btnVerReservas.Text = "Ver reservas";
            this.btnVerReservas.Click += new System.EventHandler(this.btnVerReservas_Click);

            // btnCancelarReserva
            this.btnCancelarReserva.Location = new System.Drawing.Point(260, 350);
            this.btnCancelarReserva.Name = "btnCancelarReserva";
            this.btnCancelarReserva.Size = new System.Drawing.Size(150, 30);
            this.btnCancelarReserva.Text = "Cancelar reserva";
            this.btnCancelarReserva.Click += new System.EventHandler(this.btnCancelarReserva_Click);

            // btnSalir
            this.btnSalir.Location = new System.Drawing.Point(520, 350);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(100, 30);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);

            // FormCliente
            this.ClientSize = new System.Drawing.Size(650, 420);
            this.Controls.Add(this.lblBienvenida);
            this.Controls.Add(this.comboActividades);
            this.Controls.Add(this.btnVerHorarios);
            this.Controls.Add(this.dataGridViewHorarios);
            this.Controls.Add(this.btnReservar);
            this.Controls.Add(this.btnVerReservas);
            this.Controls.Add(this.btnCancelarReserva);
            this.Controls.Add(this.btnSalir);
            this.Name = "FormCliente";
            this.Text = "Panel del Cliente";

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHorarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}