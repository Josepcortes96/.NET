using System;
using System.Drawing;
using System.Windows.Forms;

namespace FitData.Forms
{
    partial class FormActividad
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblNombre, lblDescripcion, lblNivelIntensidad, lblSala, lblIdMonitor, lblIdEncargado;
        private TextBox txtNombre, txtDescripcion, txtNivelIntensidad, txtSala, txtIdMonitor, txtIdEncargado;
        private FitData.Controls.BotonRedondeado btnGuardar, btnCancelar;

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
            this.ClientSize = new Size(520, 550);
            this.Font = new Font("Segoe UI", 11F);
            this.ForeColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Gestión de Actividad";
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

            // ========== CONTENIDO SCROLEABLE ==========
            Panel scrollPanel = new Panel();
            scrollPanel.Dock = DockStyle.Fill;
            scrollPanel.AutoScroll = true;
            scrollPanel.Padding = new Padding(30, 90, 30, 20);
            this.Controls.Add(scrollPanel);

            // ==== TABLE LAYOUT PRINCIPAL ====
            TableLayoutPanel layout = new TableLayoutPanel();
            layout.ColumnCount = 2;
            layout.Dock = DockStyle.Top;
            layout.AutoSize = true;

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            // ==== CAMPOS ====
            lblNombre = new Label() { Text = "Nombre:", AutoSize = true };
            txtNombre = CreateTextBox();

            lblDescripcion = new Label() { Text = "Descripción:", AutoSize = true };
            txtDescripcion = CreateTextBox();

            lblNivelIntensidad = new Label() { Text = "Nivel Intensidad:", AutoSize = true };
            txtNivelIntensidad = CreateTextBox();

            lblSala = new Label() { Text = "Sala:", AutoSize = true };
            txtSala = CreateTextBox();

            lblIdMonitor = new Label() { Text = "ID Monitor:", AutoSize = true };
            txtIdMonitor = CreateTextBox();

            lblIdEncargado = new Label() { Text = "ID Encargado:", AutoSize = true };
            txtIdEncargado = CreateTextBox();

            AddRow(layout, lblNombre, txtNombre);
            AddRow(layout, lblDescripcion, txtDescripcion);
            AddRow(layout, lblNivelIntensidad, txtNivelIntensidad);
            AddRow(layout, lblSala, txtSala);
            AddRow(layout, lblIdMonitor, txtIdMonitor);
            AddRow(layout, lblIdEncargado, txtIdEncargado);

            scrollPanel.Controls.Add(layout);

            // ==== BOTONES INFERIORES ====
            btnGuardar = CreateButton("Guardar", Color.FromArgb(0, 120, 215));
            btnGuardar.Click += btnGuardar_Click;

            btnCancelar = CreateButton("Cancelar", Color.FromArgb(60, 60, 60));
            btnCancelar.Click += btnCancelar_Click;

            TableLayoutPanel buttonLayout = new TableLayoutPanel();
            buttonLayout.Dock = DockStyle.Bottom;
            buttonLayout.Height = 80;
            buttonLayout.ColumnCount = 2;
            buttonLayout.RowCount = 1;
            buttonLayout.Padding = new Padding(40, 10, 40, 10);

            buttonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            buttonLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            btnGuardar.Anchor = AnchorStyles.None;
            btnCancelar.Anchor = AnchorStyles.None;

            buttonLayout.Controls.Add(btnGuardar, 0, 0);
            buttonLayout.Controls.Add(btnCancelar, 1, 0);

            this.Controls.Add(buttonLayout);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

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

        private void AddRow(TableLayoutPanel layout, Label lbl, TextBox txt)
        {
            int row = layout.RowCount;

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            layout.Controls.Add(lbl, 0, row);
            layout.Controls.Add(txt, 1, row);

            layout.RowCount++;
        }
    }
}
