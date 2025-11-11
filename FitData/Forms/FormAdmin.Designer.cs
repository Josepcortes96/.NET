namespace FitData.Forms
{
    partial class FormAdmin
    {
        private System.ComponentModel.IContainer components = null;

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
            this.tabControlAdmin = new System.Windows.Forms.TabControl();
            this.tabUsuarios = new System.Windows.Forms.TabPage();
            this.dataGridViewUsuarios = new System.Windows.Forms.DataGridView();
            this.btnAddUsuario = new System.Windows.Forms.Button();
            this.btnEditUsuario = new System.Windows.Forms.Button();
            this.btnDeleteUsuario = new System.Windows.Forms.Button();
            this.tabActividades = new System.Windows.Forms.TabPage();
            this.dataGridViewActividades = new System.Windows.Forms.DataGridView();
            this.btnAddActividad = new System.Windows.Forms.Button();
            this.btnEditActividad = new System.Windows.Forms.Button();
            this.btnDeleteActividad = new System.Windows.Forms.Button();
            this.tabHorarios = new System.Windows.Forms.TabPage();
            this.dataGridViewHorarios = new System.Windows.Forms.DataGridView();
            this.btnAddHorario = new System.Windows.Forms.Button();
            this.btnEditHorario = new System.Windows.Forms.Button();
            this.btnDeleteHorario = new System.Windows.Forms.Button();
            this.tabLista = new System.Windows.Forms.TabPage();
            this.dataGridViewLista = new System.Windows.Forms.DataGridView();
            this.btnDeleteLista = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();

            // TabControl
            this.tabControlAdmin.Controls.Add(this.tabUsuarios);
            this.tabControlAdmin.Controls.Add(this.tabActividades);
            this.tabControlAdmin.Controls.Add(this.tabHorarios);
            this.tabControlAdmin.Controls.Add(this.tabLista);
            this.tabControlAdmin.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControlAdmin.SelectedIndex = 0;
            this.tabControlAdmin.Height = 400;

            // Usuarios Tab
            this.tabUsuarios.Controls.Add(this.dataGridViewUsuarios);
            this.tabUsuarios.Controls.Add(this.btnAddUsuario);
            this.tabUsuarios.Controls.Add(this.btnEditUsuario);
            this.tabUsuarios.Controls.Add(this.btnDeleteUsuario);

            this.dataGridViewUsuarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridViewUsuarios.Height = 250;

            this.btnAddUsuario.Text = "Añadir";
            this.btnAddUsuario.Top = 260;
            this.btnAddUsuario.Left = 10;
            this.btnAddUsuario.Click += new System.EventHandler(this.btnAddUsuario_Click);

            this.btnEditUsuario.Text = "Editar";
            this.btnEditUsuario.Top = 260;
            this.btnEditUsuario.Left = 100;
            this.btnEditUsuario.Click += new System.EventHandler(this.btnEditUsuario_Click);

            this.btnDeleteUsuario.Text = "Eliminar";
            this.btnDeleteUsuario.Top = 260;
            this.btnDeleteUsuario.Left = 190;
            this.btnDeleteUsuario.Click += new System.EventHandler(this.btnDeleteUsuario_Click);

            // Actividades Tab
            this.tabActividades.Controls.Add(this.dataGridViewActividades);
            this.tabActividades.Controls.Add(this.btnAddActividad);
            this.tabActividades.Controls.Add(this.btnEditActividad);
            this.tabActividades.Controls.Add(this.btnDeleteActividad);

            this.dataGridViewActividades.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridViewActividades.Height = 250;

            this.btnAddActividad.Text = "Añadir";
            this.btnAddActividad.Top = 260;
            this.btnAddActividad.Left = 10;
            this.btnAddActividad.Click += new System.EventHandler(this.btnAddActividad_Click);

            this.btnEditActividad.Text = "Editar";
            this.btnEditActividad.Top = 260;
            this.btnEditActividad.Left = 100;
            this.btnEditActividad.Click += new System.EventHandler(this.btnEditActividad_Click);

            this.btnDeleteActividad.Text = "Eliminar";
            this.btnDeleteActividad.Top = 260;
            this.btnDeleteActividad.Left = 190;
            this.btnDeleteActividad.Click += new System.EventHandler(this.btnDeleteActividad_Click);

            // Horarios Tab
            this.tabHorarios.Controls.Add(this.dataGridViewHorarios);
            this.tabHorarios.Controls.Add(this.btnAddHorario);
            this.tabHorarios.Controls.Add(this.btnEditHorario);
            this.tabHorarios.Controls.Add(this.btnDeleteHorario);

            this.dataGridViewHorarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridViewHorarios.Height = 250;

            this.btnAddHorario.Text = "Añadir";
            this.btnAddHorario.Top = 260;
            this.btnAddHorario.Left = 10;
            this.btnAddHorario.Click += new System.EventHandler(this.btnAddHorario_Click);

            this.btnEditHorario.Text = "Editar";
            this.btnEditHorario.Top = 260;
            this.btnEditHorario.Left = 100;
            this.btnEditHorario.Click += new System.EventHandler(this.btnEditHorario_Click);

            this.btnDeleteHorario.Text = "Eliminar";
            this.btnDeleteHorario.Top = 260;
            this.btnDeleteHorario.Left = 190;
            this.btnDeleteHorario.Click += new System.EventHandler(this.btnDeleteHorario_Click);

            // ListaEspera Tab
            this.tabLista.Controls.Add(this.dataGridViewLista);
            this.tabLista.Controls.Add(this.btnDeleteLista);

            this.dataGridViewLista.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridViewLista.Height = 250;

            this.btnDeleteLista.Text = "Eliminar";
            this.btnDeleteLista.Top = 260;
            this.btnDeleteLista.Left = 10;
            this.btnDeleteLista.Click += new System.EventHandler(this.btnDeleteLista_Click);

            // Logout
            this.btnLogout.Text = "Cerrar Sesión";
            this.btnLogout.Top = 420;
            this.btnLogout.Left = 10;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // FormAdmin
            this.Controls.Add(this.tabControlAdmin);
            this.Controls.Add(this.btnLogout);
            this.Text = "Panel Administrador";
            this.ClientSize = new System.Drawing.Size(800, 480);
        }

        private System.Windows.Forms.TabControl tabControlAdmin;
        private System.Windows.Forms.TabPage tabUsuarios;
        private System.Windows.Forms.TabPage tabActividades;
        private System.Windows.Forms.TabPage tabHorarios;
        private System.Windows.Forms.TabPage tabLista;

        private System.Windows.Forms.DataGridView dataGridViewUsuarios;
        private System.Windows.Forms.Button btnAddUsuario;
        private System.Windows.Forms.Button btnEditUsuario;
        private System.Windows.Forms.Button btnDeleteUsuario;

        private System.Windows.Forms.DataGridView dataGridViewActividades;
        private System.Windows.Forms.Button btnAddActividad;
        private System.Windows.Forms.Button btnEditActividad;
        private System.Windows.Forms.Button btnDeleteActividad;

        private System.Windows.Forms.DataGridView dataGridViewHorarios;
        private System.Windows.Forms.Button btnAddHorario;
        private System.Windows.Forms.Button btnEditHorario;
        private System.Windows.Forms.Button btnDeleteHorario;

        private System.Windows.Forms.DataGridView dataGridViewLista;
        private System.Windows.Forms.Button btnDeleteLista;

        private System.Windows.Forms.Button btnLogout;
    }
}
