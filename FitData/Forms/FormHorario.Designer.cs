using System;
using System.Drawing;
using System.Windows.Forms;

namespace FitData.Forms
{
    partial class FormHorario
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblActividad, lblDia, lblInicio, lblFin, lblPlazas;
        private ComboBox cmbActividad;
        private TextBox txtDiaSemana;
        private DateTimePicker dtpHoraInicio, dtpHoraFin;
        private NumericUpDown nudPlazas;
        private FitData.Controls.BotonRedondeado btnSave, btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // ==== FORM ====
            this.BackColor = Color.FromArgb(85, 85, 84);
            this.ClientSize = new Size(520, 500);
            this.Font = new Font("Segoe UI", 11F);
            this.ForeColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Gestión de Horario";
            this.FormBorderStyle = FormBorderStyle.Sizable;

            // ==== LOGO ARRIBA DERECHA ====
            PictureBox picLogoSmall = new PictureBox();
            picLogoSmall.Image = Image.FromFile(
                System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "public", "FitData.jpg")
            );
            picLogoSmall.Size = new Size(60, 60);
            picLogoSmall.SizeMode = PictureBoxSizeMode.Zoom;
            picLogoSmall.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picLogoSmall.Location = new Point(this.ClientSize.Width - 75, 10);
            this.Controls.Add(picLogoSmall);

            this.Resize += (s, e) =>
            {
                picLogoSmall.Location = new Point(this.ClientSize.Width - 75, 10);
            };

            // ========== PANEL SCROLLABLE ==========
            Panel scrollPanel = new Panel();
            scrollPanel.Dock = DockStyle.Fill;
            scrollPanel.AutoScroll = true;
            scrollPanel.Padding = new Padding(30, 90, 30, 20);
            this.Controls.Add(scrollPanel);

            // ========== TABLA PRINCIPAL ==========
            TableLayoutPanel layout = new TableLayoutPanel();
            layout.ColumnCount = 2;
            layout.Dock = DockStyle.Top;
            layout.AutoSize = true;

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            // ==== CAMPOS ====
            lblActividad = new Label() { Text = "Actividad:", AutoSize = true };
            cmbActividad = new ComboBox()
            {
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 250
            };

            lblDia = new Label() { Text = "Día semana:", AutoSize = true };
            txtDiaSemana = CreateTextBox();

            lblInicio = new Label() { Text = "Hora inicio:", AutoSize = true };
            dtpHoraInicio = new DateTimePicker()
            {
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true,
                Width = 150,
                BackColor = Color.FromArgb(50, 50, 50)
            };

            lblFin = new Label() { Text = "Hora fin:", AutoSize = true };
            dtpHoraFin = new DateTimePicker()
            {
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true,
                Width = 150,
                BackColor = Color.FromArgb(50, 50, 50)
            };

            lblPlazas = new Label() { Text = "Plazas totales:", AutoSize = true };
            nudPlazas = new NumericUpDown()
            {
                Minimum = 1,
                Maximum = 100,
                Value = 16,
                Width = 150,
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White
            };

            // Añadir filas al layout
            AddRow(layout, lblActividad, cmbActividad);
            AddRow(layout, lblDia, txtDiaSemana);
            AddRow(layout, lblInicio, dtpHoraInicio);
            AddRow(layout, lblFin, dtpHoraFin);
            AddRow(layout, lblPlazas, nudPlazas);

            scrollPanel.Controls.Add(layout);

            // ==== BOTONES ====
            btnSave = CreateButton("Guardar", Color.FromArgb(0, 120, 215));
            btnSave.Click += btnSave_Click;

            btnCancel = CreateButton("Cancelar", Color.FromArgb(60, 60, 60));
            btnCancel.Click += btnCancel_Click;

            TableLayoutPanel buttonLayout = new TableLayoutPanel();
            buttonLayout.Dock = DockStyle.Bottom;
            buttonLayout.Height = 80;
            buttonLayout.ColumnCount = 2;
            buttonLayout.Padding = new Padding(40, 10, 40, 10);

            buttonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            buttonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            buttonLayout.Controls.Add(btnSave, 0, 0);
            buttonLayout.Controls.Add(btnCancel, 1, 0);

            this.Controls.Add(buttonLayout);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // === Helpers ===
        private TextBox CreateTextBox()
        {
            return new TextBox()
            {
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Width = 250,
                Height = 28
            };
        }

        private FitData.Controls.BotonRedondeado CreateButton(string text, Color backColor)
        {
            return new FitData.Controls.BotonRedondeado()
            {
                Text = text,
                BorderRadius = 18,
                BorderSize = 3,
                BorderColor = Color.White,
                BackColor = backColor,
                ForeColor = Color.White,
                Size = new Size(150, 48),
                Margin = new Padding(20)
            };
        }

        private void AddRow(TableLayoutPanel layout, Control lbl, Control ctrl)
        {
            int row = layout.RowCount;

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            layout.Controls.Add(lbl, 0, row);
            layout.Controls.Add(ctrl, 1, row);

            layout.RowCount++;
        }
    }
}
