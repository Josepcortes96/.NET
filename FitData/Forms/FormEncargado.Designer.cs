namespace FitData.Forms
{
    partial class FormEncargado
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewActividades;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewActividades = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewActividades)).BeginInit();
            this.SuspendLayout();
            // dataGridViewActividades
            this.dataGridViewActividades.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewActividades.Size = new System.Drawing.Size(600, 300);
            this.dataGridViewActividades.ReadOnly = false;
            this.dataGridViewActividades.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // FormEncargado
            this.ClientSize = new System.Drawing.Size(750, 350);
            this.Controls.Add(this.dataGridViewActividades);
            this.Text = "Encargado FitData";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewActividades)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

