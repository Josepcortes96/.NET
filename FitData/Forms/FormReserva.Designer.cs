namespace FitData.Forms
{
    partial class FormReserva
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewMisReservas;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnVer;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Button btnCerrar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewMisReservas = new System.Windows.Forms.DataGridView();
            this.btnCrear = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnVer = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMisReservas)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewMisReservas
            // 
            this.dataGridViewMisReservas.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewMisReservas.Size = new System.Drawing.Size(680, 360);
            this.dataGridViewMisReservas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMisReservas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            // 
            // btnCrear
            // 
            this.btnCrear.Location = new System.Drawing.Point(710, 12);
            this.btnCrear.Size = new System.Drawing.Size(160, 34);
            this.btnCrear.Text = "Crear reserva";
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(710, 62);
            this.btnActualizar.Size = new System.Drawing.Size(160, 34);
            this.btnActualizar.Text = "Actualizar reserva";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnVer
            // 
            this.btnVer.Location = new System.Drawing.Point(710, 112);
            this.btnVer.Size = new System.Drawing.Size(160, 34);
            this.btnVer.Text = "Ver reserva";
            this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(710, 162);
            this.btnEliminar.Size = new System.Drawing.Size(160, 34);
            this.btnEliminar.Text = "Eliminar reserva";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Location = new System.Drawing.Point(710, 212);
            this.btnRefrescar.Size = new System.Drawing.Size(160, 34);
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(710, 338);
            this.btnCerrar.Size = new System.Drawing.Size(160, 34);
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // FormReserva
            // 
            this.ClientSize = new System.Drawing.Size(890, 390);
            this.Controls.Add(this.dataGridViewMisReservas);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnVer);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.btnCerrar);
            this.Text = "Reservas - Gestión";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMisReservas)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

