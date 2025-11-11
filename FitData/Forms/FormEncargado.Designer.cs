namespace FitData.Forms
{
    partial class FormEncargado
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewActividades;
        private System.Windows.Forms.DataGridView dataGridViewHorarios;
        private System.Windows.Forms.Button btnAddActividad;
        private System.Windows.Forms.Button btnEditActividad;
        private System.Windows.Forms.Button btnDeleteActividad;
        private System.Windows.Forms.Button btnAddHorario;
        private System.Windows.Forms.Button btnEditHorario;
        private System.Windows.Forms.Button btnDeleteHorario;
        private System.Windows.Forms.Button btnLogout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewActividades = new System.Windows.Forms.DataGridView();
            this.dataGridViewHorarios = new System.Windows.Forms.DataGridView();
            this.btnAddActividad = new System.Windows.Forms.Button();
            this.btnEditActividad = new System.Windows.Forms.Button();
            this.btnDeleteActividad = new System.Windows.Forms.Button();
            this.btnAddHorario = new System.Windows.Forms.Button();
            this.btnEditHorario = new System.Windows.Forms.Button();
            this.btnDeleteHorario = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewActividades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHorarios)).BeginInit();
            this.SuspendLayout();
            // dataGridViewActividades
            this.dataGridViewActividades.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewActividades.Size = new System.Drawing.Size(600, 200);
            this.dataGridViewActividades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewActividades.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewActividades.SelectionChanged += new System.EventHandler(this.dataGridViewActividades_SelectionChanged);
            // dataGridViewHorarios
            this.dataGridViewHorarios.Location = new System.Drawing.Point(12, 240);
            this.dataGridViewHorarios.Size = new System.Drawing.Size(600, 200);
            this.dataGridViewHorarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewHorarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            // Botones
            this.btnAddActividad.Location = new System.Drawing.Point(630, 20);
            this.btnAddActividad.Size = new System.Drawing.Size(150, 30);
            this.btnAddActividad.Text = "Añadir Actividad";
            this.btnAddActividad.Click += new System.EventHandler(this.btnAddActividad_Click);
            this.btnEditActividad.Location = new System.Drawing.Point(630, 60);
            this.btnEditActividad.Size = new System.Drawing.Size(150, 30);
            this.btnEditActividad.Text = "Editar Actividad";
            this.btnEditActividad.Click += new System.EventHandler(this.btnEditActividad_Click);
            this.btnDeleteActividad.Location = new System.Drawing.Point(630, 100);
            this.btnDeleteActividad.Size = new System.Drawing.Size(150, 30);
            this.btnDeleteActividad.Text = "Eliminar Actividad";
            this.btnDeleteActividad.Click += new System.EventHandler(this.btnDeleteActividad_Click);
            this.btnAddHorario.Location = new System.Drawing.Point(630, 240);
            this.btnAddHorario.Size = new System.Drawing.Size(150, 30);
            this.btnAddHorario.Text = "Añadir Horario";
            this.btnAddHorario.Click += new System.EventHandler(this.btnAddHorario_Click);
            this.btnEditHorario.Location = new System.Drawing.Point(630, 280);
            this.btnEditHorario.Size = new System.Drawing.Size(150, 30);
            this.btnEditHorario.Text = "Editar Horario";
            this.btnEditHorario.Click += new System.EventHandler(this.btnEditHorario_Click);
            this.btnDeleteHorario.Location = new System.Drawing.Point(630, 320);
            this.btnDeleteHorario.Size = new System.Drawing.Size(150, 30);
            this.btnDeleteHorario.Text = "Eliminar Horario";
            this.btnDeleteHorario.Click += new System.EventHandler(this.btnDeleteHorario_Click);
            this.btnLogout.Location = new System.Drawing.Point(630, 410);
            this.btnLogout.Size = new System.Drawing.Size(150, 30);
            this.btnLogout.Text = "Cerrar Sesión";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // FormEncargado
            this.ClientSize = new System.Drawing.Size(800, 460);
            this.Controls.Add(this.dataGridViewActividades);
            this.Controls.Add(this.dataGridViewHorarios);
            this.Controls.Add(this.btnAddActividad);
            this.Controls.Add(this.btnEditActividad);
            this.Controls.Add(this.btnDeleteActividad);
            this.Controls.Add(this.btnAddHorario);
            this.Controls.Add(this.btnEditHorario);
            this.Controls.Add(this.btnDeleteHorario);
            this.Controls.Add(this.btnLogout);
            this.Text = "Gestión de Actividades - Encargado";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewActividades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHorarios)).EndInit();
            this.ResumeLayout(false);
        }
    }
}