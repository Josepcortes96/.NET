namespace FitData.Forms
{
    partial class FormRecepcionista
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewUsuarios;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewUsuarios = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsuarios)).BeginInit();
            this.SuspendLayout();
            // dataGridViewUsuarios
            this.dataGridViewUsuarios.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewUsuarios.Size = new System.Drawing.Size(600, 300);
            this.dataGridViewUsuarios.ReadOnly = true;
            this.dataGridViewUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // FormRecepcionista
            this.ClientSize = new System.Drawing.Size(750, 350);
            this.Controls.Add(this.dataGridViewUsuarios);
            this.Text = "Recepcionista FitData";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsuarios)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

