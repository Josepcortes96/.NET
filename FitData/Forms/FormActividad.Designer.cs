namespace FitData.Forms
{
    partial class FormActividad
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblSala;
        private System.Windows.Forms.TextBox txtSala;
        private System.Windows.Forms.Label lblNivel;
        private System.Windows.Forms.TextBox txtNivel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblSala = new System.Windows.Forms.Label();
            this.txtSala = new System.Windows.Forms.TextBox();
            this.lblNivel = new System.Windows.Forms.Label();
            this.txtNivel = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblNombre
            //
            this.lblNombre.Location = new System.Drawing.Point(12, 15);
            this.lblNombre.Size = new System.Drawing.Size(80, 23);
            this.lblNombre.Text = "Nombre:";
            //
            // txtNombre
            //
            this.txtNombre.Location = new System.Drawing.Point(100, 12);
            this.txtNombre.Size = new System.Drawing.Size(260, 23);
            //
            // lblDescripcion
            //
            this.lblDescripcion.Location = new System.Drawing.Point(12, 50);
            this.lblDescripcion.Size = new System.Drawing.Size(80, 23);
            this.lblDescripcion.Text = "Descripcion:";
            //
            // txtDescripcion
            //
            this.txtDescripcion.Location = new System.Drawing.Point(100, 47);
            this.txtDescripcion.Size = new System.Drawing.Size(260, 60);
            this.txtDescripcion.Multiline = true;
            //
            // lblSala
            //
            this.lblSala.Location = new System.Drawing.Point(12, 120);
            this.lblSala.Size = new System.Drawing.Size(80, 23);
            this.lblSala.Text = "Sala:";
            //
            // txtSala
            //
            this.txtSala.Location = new System.Drawing.Point(100, 117);
            this.txtSala.Size = new System.Drawing.Size(260, 23);
            //
            // lblNivel
            //
            this.lblNivel.Location = new System.Drawing.Point(12, 155);
            this.lblNivel.Size = new System.Drawing.Size(80, 23);
            this.lblNivel.Text = "Nivel:";
            //
            // txtNivel
            //
            this.txtNivel.Location = new System.Drawing.Point(100, 152);
            this.txtNivel.Size = new System.Drawing.Size(260, 23);
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
            // FormActividad
            //
            this.ClientSize = new System.Drawing.Size(380, 240);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNivel);
            this.Controls.Add(this.lblNivel);
            this.Controls.Add(this.txtSala);
            this.Controls.Add(this.lblSala);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblNombre);
            this.Text = "Actividad";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

