namespace FitData.Forms
{
    partial class FormMonitor
    {
        private System.ComponentModel.IContainer components = null;

        private PictureBox picLogo;
        private DataGridView dataGridViewActividades;
        private DataGridView dataGridViewHorarios;
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

            this.picLogo = new PictureBox();
            this.dataGridViewActividades = new DataGridView();
            this.dataGridViewHorarios = new DataGridView();
            this.btnLogout = new FitData.Controls.BotonRedondeado();

            this.SuspendLayout();

            // === FORM ===
            this.BackColor = Color.FromArgb(85, 85, 84);
            this.ClientSize = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Panel de Monitor";

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
            dataGridViewActividades.SelectionChanged += new EventHandler(this.dataGridViewActividades_SelectionChanged);

            // === TABLA 2 (HORARIOS) ===
            ConfigureGrid(dataGridViewHorarios);
            dataGridViewHorarios.Dock = DockStyle.Fill;

            // === BOTÓN LOGOUT ===
            StyleButton(btnLogout, "Cerrar sesión");
            btnLogout.Width = 250;
            btnLogout.Anchor = AnchorStyles.Bottom;
            btnLogout.Click += new EventHandler(this.btnLogout_Click);

            // === LAYOUT PRINCIPAL ===
            TableLayoutPanel layout = new TableLayoutPanel();
            layout.RowCount = 4;
            layout.ColumnCount = 1;
            layout.Dock = DockStyle.Fill;

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80)); // Logo
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));  // Actividades
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));  // Horarios
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70)); // Logout

            layout.Controls.Add(picLogo, 0, 0);
            layout.Controls.Add(dataGridViewActividades, 0, 1);
            layout.Controls.Add(dataGridViewHorarios, 0, 2);

            FlowLayoutPanel panelLogout = new FlowLayoutPanel();
            panelLogout.FlowDirection = FlowDirection.LeftToRight;
            panelLogout.Dock = DockStyle.Fill;
            panelLogout.Padding = new Padding(10);
            panelLogout.Controls.Add(btnLogout);

            layout.Controls.Add(panelLogout, 0, 3);

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
