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
        private FitData.Controls.BotonRedondeado btnOdoo;


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

            // ==== LOGO ARRIBA DERECHA ====
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
            tabControlAdmin.Appearance = TabAppearance.Normal;

            // ==== TABS ====
            tabUsuarios = new TabPage("Usuarios");
            tabActividades = new TabPage("Actividades");
            tabHorarios = new TabPage("Horarios");
            tabLista = new TabPage("Lista Espera");

            // ==== ESTILO GENERAL ====
            ApplyTabDarkStyle(tabUsuarios);
            ApplyTabDarkStyle(tabActividades);
            ApplyTabDarkStyle(tabHorarios);
            ApplyTabDarkStyle(tabLista);

            // ==== CREAR CONTENIDO DE CADA TAB ====
            SetupTab(tabUsuarios, out dataGridViewUsuarios,
                     out btnAddUsuario, out btnEditUsuario, out btnDeleteUsuario);

            btnAddUsuario.Click += btnAddUsuario_Click;
            btnEditUsuario.Click += btnEditUsuario_Click;
            btnDeleteUsuario.Click += btnDeleteUsuario_Click;

            SetupTab(tabActividades, out dataGridViewActividades,
                     out btnAddActividad, out btnEditActividad, out btnDeleteActividad);

            btnAddActividad.Click += btnAddActividad_Click;
            btnEditActividad.Click += btnEditActividad_Click;
            btnDeleteActividad.Click += btnDeleteActividad_Click;

            SetupTab(tabHorarios, out dataGridViewHorarios,
                     out btnAddHorario, out btnEditHorario, out btnDeleteHorario);

            btnAddHorario.Click += btnAddHorario_Click;
            btnEditHorario.Click += btnEditHorario_Click;
            btnDeleteHorario.Click += btnDeleteHorario_Click;

            // LISTA ESPERA
            SetupTab(tabLista, out dataGridViewLista,
                     out btnDeleteLista);

            btnDeleteLista.Text = "Eliminar";
            btnDeleteLista.Click += btnDeleteLista_Click;


            // ==== BOTON ODOO (solo visible para admins) ====
            btnOdoo = new FitData.Controls.BotonRedondeado();
            btnOdoo.Text = "Odoo";
            btnOdoo.Size = new Size(120, 45);
            btnOdoo.BorderRadius = 18;
            btnOdoo.BorderSize = 2;
            btnOdoo.BackColor = Color.FromArgb(60, 60, 60);
            btnOdoo.ForeColor = Color.White;
            btnOdoo.Margin = new Padding(10);
            btnOdoo.Cursor = Cursors.Hand;
            // Posición: lo colocamos en la parte inferior, encima del logout (puedes cambiar Dock)
            // btnOdoo.Dock = DockStyle.Bottom; // si prefieres modo dock
            btnOdoo.Location = new Point(10, this.ClientSize.Height - 110); // ajustar si no usas Dock
            btnOdoo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnOdoo.Click += btnOdoo_Click;

            // Añadir al form 
            this.Controls.Add(btnOdoo);



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

            // ==== AGREGAR TABS ====
            tabControlAdmin.TabPages.Add(tabUsuarios);
            tabControlAdmin.TabPages.Add(tabActividades);
            tabControlAdmin.TabPages.Add(tabHorarios);
            tabControlAdmin.TabPages.Add(tabLista);
         

            // ==== ADD CONTROLS ====
            this.Controls.Add(tabControlAdmin);
            this.Controls.Add(btnLogout);
        }

        // =====================================
        //          FUNCIONES AUXILIARES
        // =====================================

        private void ApplyTabDarkStyle(TabPage tab)
        {
            tab.BackColor = Color.FromArgb(70, 70, 70);
            tab.ForeColor = Color.White;
            tab.Padding = new Padding(10);
        }

        private void SetupTab(TabPage tab,
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

            // GRID
            grid = new DataGridView();
            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.BackgroundColor = Color.FromArgb(60, 60, 60);
            grid.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            grid.DefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.EnableHeadersVisualStyles = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // PANEL DE BOTONES
            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.FlowDirection = FlowDirection.LeftToRight;
            panel.Padding = new Padding(10);
            panel.BackColor = Color.FromArgb(70, 70, 70);

            btnAdd = CreateExportButton("Añadir");
            btnEdit = CreateExportButton("Editar");
            btnDelete = CreateExportButton("Eliminar");

            panel.Controls.Add(btnAdd);
            panel.Controls.Add(btnEdit);
            panel.Controls.Add(btnDelete);

            layout.Controls.Add(grid, 0, 0);
            layout.Controls.Add(panel, 0, 1);

            tab.Controls.Add(layout);
        }

        private void SetupTab(TabPage tab,
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
            grid.ReadOnly = true;
            grid.BackgroundColor = Color.FromArgb(60, 60, 60);
            grid.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            grid.DefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.EnableHeadersVisualStyles = false;

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
