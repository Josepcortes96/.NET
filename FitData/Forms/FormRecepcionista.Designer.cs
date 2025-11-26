namespace FitData.Forms
{
    partial class FormRecepcionista
    {
        private System.ComponentModel.IContainer components = null;

        private PictureBox picLogo;
        private DataGridView dataGridViewUsuarios;

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
            this.dataGridViewUsuarios = new DataGridView();

            this.SuspendLayout();

            // === FORM ===
            this.BackColor = Color.FromArgb(85, 85, 84);
            this.ClientSize = new Size(900, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Recepcionista FitData";

            // === LOGO ===
            this.picLogo.Image = Image.FromFile(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "public", "FitData.jpg")
            );
            this.picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            this.picLogo.BackColor = Color.Transparent;
            this.picLogo.Size = new Size(70, 70);

            // RESPONSIVE → anclar arriba a la derecha
            this.picLogo.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // === GRID USUARIOS ===
            ConfigureGrid(dataGridViewUsuarios);
            dataGridViewUsuarios.Dock = DockStyle.Fill;

            // === LAYOUT PRINCIPAL ===
            TableLayoutPanel layout = new TableLayoutPanel();
            layout.RowCount = 2;
            layout.ColumnCount = 1;
            layout.Dock = DockStyle.Fill;

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80)); // logo
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // tabla

            // Panel para alinear el LOGO arriba a la derecha
            FlowLayoutPanel panelLogo = new FlowLayoutPanel();
            panelLogo.FlowDirection = FlowDirection.RightToLeft;
            panelLogo.Dock = DockStyle.Fill;
            panelLogo.Padding = new Padding(10);
            panelLogo.Controls.Add(picLogo);

            layout.Controls.Add(panelLogo, 0, 0);
            layout.Controls.Add(dataGridViewUsuarios, 0, 1);

            this.Controls.Add(layout);

            this.ResumeLayout(false);
        }

        // === MISMO ESTILO DE GRID QUE FORM MONITOR ===
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
    }
}
