using System;
using System.Drawing;
using System.Windows.Forms;

namespace FitData.Forms
{
    partial class FormAdmin
    {
        private System.ComponentModel.IContainer components = null;

        private PictureBox picLogo;
        private TabControl tabControlAdmin;

        private TabPage tabUsuarios;
        private DataGridView dataGridViewUsuarios;
        private FitData.Controls.BotonRedondeado btnAddUsuario;
        private FitData.Controls.BotonRedondeado btnEditUsuario;
        private FitData.Controls.BotonRedondeado btnDeleteUsuario;

        private TabPage tabActividades;
        private DataGridView dataGridViewActividades;
        private FitData.Controls.BotonRedondeado btnAddActividad;
        private FitData.Controls.BotonRedondeado btnEditActividad;
        private FitData.Controls.BotonRedondeado btnDeleteActividad;

        private TabPage tabHorarios;
        private DataGridView dataGridViewHorarios;
        private FitData.Controls.BotonRedondeado btnAddHorario;
        private FitData.Controls.BotonRedondeado btnEditHorario;
        private FitData.Controls.BotonRedondeado btnDeleteHorario;

        private TabPage tabLista;
        private DataGridView dataGridViewLista;
        private FitData.Controls.BotonRedondeado btnDeleteLista;

        private FitData.Controls.BotonRedondeado btnLogout;

        // ==== NUEVOS BOTONES ====
        private FitData.Controls.BotonRedondeado btnSyncOdoo;
        private FitData.Controls.BotonRedondeado btnImportFromOdoo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // ==== FORM ====
            this.BackColor = Color.FromArgb(85, 85, 84);
            this.ClientSize = new Size(950, 650);
            this.Font = new Font("Segoe UI", 10F);
            this.ForeColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Panel Administrador";

            picLogo = new PictureBox();
            picLogo.Image = Image.FromFile(
                System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "public", "FitData.jpg")
            );
            picLogo.Size = new Size(65, 65);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picLogo.Location = new Point(this.ClientSize.Width - 80, 10);
            this.Controls.Add(picLogo);

            // ==== TABCONTROL ====
            tabControlAdmin = new TabControl();
            tabControlAdmin.Dock = DockStyle.Fill;

            tabUsuarios = new TabPage("Usuarios");
            tabActividades = new TabPage("Actividades");
            tabHorarios = new TabPage("Horarios");
            tabLista = new TabPage("Lista Espera");

            ApplyTabDarkStyle(tabUsuarios);
            ApplyTabDarkStyle(tabActividades);
            ApplyTabDarkStyle(tabHorarios);
            ApplyTabDarkStyle(tabLista);

            // ============================================================
            // ####################   TAB USUARIOS   #######################
            // ============================================================
            {
                TableLayoutPanel layout = new TableLayoutPanel();
                layout.Dock = DockStyle.Fill;
                layout.RowCount = 2;
                layout.ColumnCount = 1;

                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 80));
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

                // GRID
                dataGridViewUsuarios = new DataGridView();
                dataGridViewUsuarios.Dock = DockStyle.Fill;
                dataGridViewUsuarios.ReadOnly = true;
                dataGridViewUsuarios.BackgroundColor = Color.FromArgb(60, 60, 60);
                dataGridViewUsuarios.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
                dataGridViewUsuarios.DefaultCellStyle.ForeColor = Color.White;

                // PANEL INFERIOR
                FlowLayoutPanel panel = new FlowLayoutPanel();
                panel.Dock = DockStyle.Fill;
                panel.FlowDirection = FlowDirection.LeftToRight;
                panel.Padding = new Padding(10);
                panel.BackColor = Color.FromArgb(70, 70, 70);

                // BOTONES CRUD
                btnAddUsuario = CreateAdminButton("Añadir");
                btnEditUsuario = CreateAdminButton("Editar");
                btnDeleteUsuario = CreateAdminButton("Eliminar");

                btnAddUsuario.Click += btnAddUsuario_Click;
                btnEditUsuario.Click += btnEditUsuario_Click;
                btnDeleteUsuario.Click += btnDeleteUsuario_Click;

                panel.Controls.Add(btnAddUsuario);
                panel.Controls.Add(btnEditUsuario);
                panel.Controls.Add(btnDeleteUsuario);

                // ======== BOTONES ODOO ABAJO =========
                btnSyncOdoo = CreateAdminButton("Sincronizar Odoo");
                btnSyncOdoo.Click += btnSyncOdoo_Click;

                btnImportFromOdoo = CreateAdminButton("Importar desde Odoo");
                btnImportFromOdoo.Click += btnImportFromOdoo_Click;

                panel.Controls.Add(btnSyncOdoo);
                panel.Controls.Add(btnImportFromOdoo);
                // ======================================

                layout.Controls.Add(dataGridViewUsuarios, 0, 0);
                layout.Controls.Add(panel, 0, 1);

                tabUsuarios.Controls.Add(layout);
            }

            // ==== TAB ACTIVIDADES ====
            SetupTab(tabActividades, out dataGridViewActividades,
                     out btnAddActividad, out btnEditActividad, out btnDeleteActividad);

            btnAddActividad.Click += btnAddActividad_Click;
            btnEditActividad.Click += btnEditActividad_Click;
            btnDeleteActividad.Click += btnDeleteActividad_Click;

            // ==== TAB HORARIOS ====
            SetupTab(tabHorarios, out dataGridViewHorarios,
                     out btnAddHorario, out btnEditHorario, out btnDeleteHorario);

            btnAddHorario.Click += btnAddHorario_Click;
            btnEditHorario.Click += btnEditHorario_Click;
            btnDeleteHorario.Click += btnDeleteHorario_Click;

            // ==== TAB LISTA ====
            SetupTab(tabLista, out dataGridViewLista, out btnDeleteLista);
            btnDeleteLista.Text = "Eliminar";
            btnDeleteLista.Click += btnDeleteLista_Click;

            // ==== LOGOUT ====
            btnLogout = new FitData.Controls.BotonRedondeado();
            btnLogout.Text = "Cerrar sesión";
            btnLogout.Dock = DockStyle.Bottom;
            btnLogout.Height = 50;
            btnLogout.BorderRadius = 18;
            btnLogout.BorderSize = 2;
            btnLogout.BackColor = Color.FromArgb(60, 60, 60);
            btnLogout.ForeColor = Color.White;
            btnLogout.Click += btnLogout_Click;

            // ==== AÑADIR TABS ====
            tabControlAdmin.TabPages.Add(tabUsuarios);
            tabControlAdmin.TabPages.Add(tabActividades);
            tabControlAdmin.TabPages.Add(tabHorarios);
            tabControlAdmin.TabPages.Add(tabLista);

            this.Controls.Add(tabControlAdmin);
            this.Controls.Add(btnLogout);
        }

        // =====================================
        //   FUNCIONES AUXILIARES
        // =====================================

        private void ApplyTabDarkStyle(TabPage tab)
        {
            tab.BackColor = Color.FromArgb(70, 70, 70);
            tab.ForeColor = Color.White;
            tab.Padding = new Padding(10);
        }

        private void SetupTab(
            TabPage tab,
            out DataGridView grid,
            out FitData.Controls.BotonRedondeado btnAdd,
            out FitData.Controls.BotonRedondeado btnEdit,
            out FitData.Controls.BotonRedondeado btnDelete)
        {
            TableLayoutPanel layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.RowCount = 2;
            layout.ColumnCount = 1;

            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 80));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

            grid = new DataGridView();
            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.BackgroundColor = Color.FromArgb(60, 60, 60);
            grid.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            grid.DefaultCellStyle.ForeColor = Color.White;

            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.FlowDirection = FlowDirection.LeftToRight;
            panel.Padding = new Padding(10);
            panel.BackColor = Color.FromArgb(70, 70, 70);

            btnAdd = CreateAdminButton("Añadir");
            btnEdit = CreateAdminButton("Editar");
            btnDelete = CreateAdminButton("Eliminar");

            panel.Controls.Add(btnAdd);
            panel.Controls.Add(btnEdit);
            panel.Controls.Add(btnDelete);

            layout.Controls.Add(grid, 0, 0);
            layout.Controls.Add(panel, 0, 1);

            tab.Controls.Add(layout);
        }

        private void SetupTab(
            TabPage tab,
            out DataGridView grid,
            out FitData.Controls.BotonRedondeado btnDelete)
        {
            TableLayoutPanel layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.RowCount = 2;
            layout.ColumnCount = 1;

            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 80));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

            grid = new DataGridView();
            grid.Dock = DockStyle.Fill;

            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.FlowDirection = FlowDirection.LeftToRight;
            panel.Padding = new Padding(10);
            panel.BackColor = Color.FromArgb(70, 70, 70);

            btnDelete = CreateAdminButton("Eliminar");

            panel.Controls.Add(btnDelete);

            layout.Controls.Add(grid, 0, 0);
            layout.Controls.Add(panel, 0, 1);

            tab.Controls.Add(layout);
        }

        private FitData.Controls.BotonRedondeado CreateAdminButton(string text)
        {
            return new FitData.Controls.BotonRedondeado
            {
                Text = text,
                Size = new Size(120, 45),
                BorderRadius = 18,
                BorderSize = 2,
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                Margin = new Padding(10),
                Cursor = Cursors.Hand
            };
        }
    }
}
