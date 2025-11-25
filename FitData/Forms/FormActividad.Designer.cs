namespace FitData.Forms
{
    partial class FormActividad
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblNivelIntensidad;
        private System.Windows.Forms.Label lblSala;
        private System.Windows.Forms.Label lblIdMonitor;
        private System.Windows.Forms.Label lblIdEncargado;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtNivelIntensidad;
        private System.Windows.Forms.TextBox txtSala;
        private System.Windows.Forms.TextBox txtIdMonitor;
        private System.Windows.Forms.TextBox txtIdEncargado;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblNivelIntensidad = new System.Windows.Forms.Label();
            this.lblSala = new System.Windows.Forms.Label();
            this.lblIdMonitor = new System.Windows.Forms.Label();
            this.lblIdEncargado = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtNivelIntensidad = new System.Windows.Forms.TextBox();
            this.txtSala = new System.Windows.Forms.TextBox();
            this.txtIdMonitor = new System.Windows.Forms.TextBox();
            this.txtIdEncargado = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // Labels
            // 
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.Location = new System.Drawing.Point(20, 20);
            this.lblDescripcion.Text = "Descripción:";
            this.lblDescripcion.Location = new System.Drawing.Point(20, 60);
            this.lblNivelIntensidad.Text = "Nivel Intensidad:";
            this.lblNivelIntensidad.Location = new System.Drawing.Point(20, 100);
            this.lblSala.Text = "Sala:";
            this.lblSala.Location = new System.Drawing.Point(20, 140);
            this.lblIdMonitor.Text = "ID Monitor:";
            this.lblIdMonitor.Location = new System.Drawing.Point(20, 180);
            this.lblIdEncargado.Text = "ID Encargado:";
            this.lblIdEncargado.Location = new System.Drawing.Point(20, 220);

            // 
            // TextBoxes
            // 
            this.txtNombre.Location = new System.Drawing.Point(150, 20);
            this.txtDescripcion.Location = new System.Drawing.Point(150, 60);
            this.txtNivelIntensidad.Location = new System.Drawing.Point(150, 100);
            this.txtSala.Location = new System.Drawing.Point(150, 140);
            this.txtIdMonitor.Location = new System.Drawing.Point(150, 180);
            this.txtIdEncargado.Location = new System.Drawing.Point(150, 220);

            this.txtNombre.Size = this.txtDescripcion.Size = this.txtNivelIntensidad.Size =
                this.txtSala.Size = this.txtIdMonitor.Size = this.txtIdEncargado.Size = new System.Drawing.Size(180, 23);

            // 
            // Buttons
            // 
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Location = new System.Drawing.Point(50, 270);
            this.btnGuardar.Size = new System.Drawing.Size(100, 30);
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Location = new System.Drawing.Point(180, 270);
            this.btnCancelar.Size = new System.Drawing.Size(100, 30);
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // 
            // FormActividad
            // 
            this.ClientSize = new System.Drawing.Size(370, 330);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.lblNivelIntensidad);
            this.Controls.Add(this.lblSala);
            this.Controls.Add(this.lblIdMonitor);
            this.Controls.Add(this.lblIdEncargado);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtNivelIntensidad);
            this.Controls.Add(this.txtSala);
            this.Controls.Add(this.txtIdMonitor);
            this.Controls.Add(this.txtIdEncargado);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Name = "FormActividad";
            this.Text = "Añadir Actividad";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}