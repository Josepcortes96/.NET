namespace FitData.Forms
{
    partial class FormEncargado
    {
        private System.ComponentModel.IContainer components = null;
        private PictureBox picLogo;

        private DataGridView dataGridViewActividades;
        private DataGridView dataGridViewHorarios;

        private FitData.Controls.BotonRedondeado btnAddActividad;
        private FitData.Controls.BotonRedondeado btnEditActividad;
        private FitData.Controls.BotonRedondeado btnDeleteActividad;

        private FitData.Controls.BotonRedondeado btnAddHorario;
        private FitData.Controls.BotonRedondeado btnEditHorario;
        private FitData.Controls.BotonRedondeado btnDeleteHorario;

        private FitData.Controls.BotonRedondeado btnLogout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.dataGridViewActividades = new DataGridView();
            this.dataGridViewHorarios = new DataGridView();

            this.btnAddActividad = new FitData.Controls.BotonRedondeado();
            this.btnEditActividad = new FitData.Controls.BotonRedondeado();
            this.btnDeleteActividad = new FitData.Controls.BotonRedondeado();

            this.btnAddHorario = new FitData.Controls.BotonRedondeado();
            this.btnEditHorario = new FitData.Controls.BotonRedondeado();
            this.btnDeleteHorario = new FitData.Controls.BotonRedondeado();

            this.btnLogout = new FitData.Controls.BotonRedondeado();

            this.picLogo = new PictureBox();

            this.SuspendLayout();

            // === FORM ===
            this.BackColor = Color.FromArgb(85, 85, 84);
            this.ClientSize = new Size(1100, 700);
            this.Text = "Gestión de Actividades - Encargado";
            this.StartPosition = FormStartPosition.CenterScreen;

            // === LOGO ===
            this.picLogo.Image = Image.FromFile(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "public", "FitData.jpg")
            );
            this.picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            this.picLogo.BackColor = Color.Transparent;
            this.picLogo.Size = new Size(70, 70);
            this.picLogo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.picLogo.Location = new Point(this.ClientSize.Width - 90, 10);

            // === TABLA 1 (ACTIVIDADES) ===
            ConfigureGrid(dataGridViewActividades);
            dataGridViewActividades.Dock = DockStyle.Fill;

            // === TABLA 2 (HORARIOS) ===
            ConfigureGrid(dataGridViewHorarios);
            dataGridViewHorarios.Dock = DockStyle.Fill;

            // === BOTONES ACTIVIDADES ===
            FlowLayoutPanel panelActividades = new FlowLayoutPanel();
            panelActividades.FlowDirection = FlowDirection.LeftToRight;
            panelActividades.Dock = DockStyle.Fill;
            panelActividades.Padding = new Padding(10);

            StyleButton(btnAddActividad, "Añadir Actividad");
            StyleButton(btnEditActividad, "Editar Actividad");
            StyleButton(btnDeleteActividad, "Eliminar Actividad");

            panelActividades.Controls.Add(btnAddActividad);
            panelActividades.Controls.Add(btnEditActividad);
            panelActividades.Controls.Add(btnDeleteActividad);

            // === BOTONES HORARIOS ===
            FlowLayoutPanel panelHorarios = new FlowLayoutPanel();
            panelHorarios.FlowDirection = FlowDirection.LeftToRight;
            panelHorarios.Dock = DockStyle.Fill;
            panelHorarios.Padding = new Padding(10);

            StyleButton(btnAddHorario, "Añadir Horario");
            StyleButton(btnEditHorario, "Editar Horario");
            StyleButton(btnDeleteHorario, "Eliminar Horario");

            panelHorarios.Controls.Add(btnAddHorario);
            panelHorarios.Controls.Add(btnEditHorario);
            panelHorarios.Controls.Add(btnDeleteHorario);

            // === BOTÓN LOGOUT ===
            StyleButton(btnLogout, "Cerrar Sesión");
            btnLogout.Width = 250;
            btnLogout.Anchor = AnchorStyles.Bottom;

            // === EVENTOS DE ACTIVIDADES ===
this.btnAddActividad.Click += new System.EventHandler(this.btnAddActividad_Click);
this.btnEditActividad.Click += new System.EventHandler(this.btnEditActividad_Click);
this.btnDeleteActividad.Click += new System.EventHandler(this.btnDeleteActividad_Click);

// === EVENTOS DE HORARIOS ===
this.btnAddHorario.Click += new System.EventHandler(this.btnAddHorario_Click);
this.btnEditHorario.Click += new System.EventHandler(this.btnEditHorario_Click);
this.btnDeleteHorario.Click += new System.EventHandler(this.btnDeleteHorario_Click);

// === EVENTO LOGOUT ===
this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

// === EVENTO GRID ACTIVIDADES ===
this.dataGridViewActividades.SelectionChanged += new System.EventHandler(this.dataGridViewActividades_SelectionChanged);


            // === TABLA PRINCIPAL (RESPONSIVA) ===
            TableLayoutPanel layout = new TableLayoutPanel();
            layout.RowCount = 5;
            layout.ColumnCount = 1;
            layout.Dock = DockStyle.Fill;

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));    // Logo
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 45));     // Grid Actividades
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));    // Buttons Actividades
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 45));     // Grid Horarios
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));    // Logout button

            layout.Controls.Add(picLogo, 0, 0);
            layout.Controls.Add(dataGridViewActividades, 0, 1);
            layout.Controls.Add(panelActividades, 0, 2);
            layout.Controls.Add(dataGridViewHorarios, 0, 3);

            FlowLayoutPanel logoutPanel = new FlowLayoutPanel();
            logoutPanel.FlowDirection = FlowDirection.LeftToRight;
            logoutPanel.Dock = DockStyle.Fill;
            logoutPanel.Padding = new Padding(10);
            logoutPanel.Controls.Add(btnLogout);

            layout.Controls.Add(logoutPanel, 0, 4);

            this.Controls.Add(layout);

            this.ResumeLayout(false);
        }

        // === ESTILO GRID ===
        private void ConfigureGrid(DataGridView grid)
        {
            grid.BackgroundColor = Color.FromArgb(60, 60, 60);
            grid.GridColor = Color.Gray;
            grid.ReadOnly = true;
            grid.EnableHeadersVisualStyles = false;

            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            grid.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            grid.DefaultCellStyle.ForeColor = Color.White;

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        // === ESTILO BOTONES ===
        private void StyleButton(FitData.Controls.BotonRedondeado b, string text)
        {
            b.Text = text;
            b.Width = 160;
            b.Height = 40;
            b.BorderRadius = 18;
            b.BorderSize = 2;
            b.BorderColor = Color.White;
            b.BackColor = Color.FromArgb(60, 60, 60);
            b.ForeColor = Color.White;
        }
    }
}
