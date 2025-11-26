using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FitData.Forms
{
    partial class FormCliente
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblBienvenida;
        private ComboBox comboActividades;
        private FitData.Controls.BotonRedondeado btnVerHorarios;
        private FitData.Controls.BotonRedondeado btnReservar;
        private FitData.Controls.BotonRedondeado btnVerReservas;
        private FitData.Controls.BotonRedondeado btnCancelarReserva;
        private FitData.Controls.BotonRedondeado btnVerListaEspera;   // <--- NUEVO
        private FitData.Controls.BotonRedondeado btnSalir;

        private DataGridView dataGridViewHorarios;
        private DataGridView dataGridViewListaEspera;                  // <--- NUEVO

        private PictureBox picLogo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                _ctx?.Dispose();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblBienvenida = new Label();
            this.comboActividades = new ComboBox();
            this.btnVerHorarios = new FitData.Controls.BotonRedondeado();
            this.btnReservar = new FitData.Controls.BotonRedondeado();
            this.btnVerReservas = new FitData.Controls.BotonRedondeado();
            this.btnCancelarReserva = new FitData.Controls.BotonRedondeado();
            this.btnVerListaEspera = new FitData.Controls.BotonRedondeado(); // <--- NUEVO
            this.btnSalir = new FitData.Controls.BotonRedondeado();
            this.dataGridViewHorarios = new DataGridView();
            this.dataGridViewListaEspera = new DataGridView();               // <--- NUEVO
            this.picLogo = new PictureBox();

            this.SuspendLayout();

            // ==== FORM ====
            this.BackColor = Color.FromArgb(85, 85, 84);
            this.ClientSize = new Size(900, 600);
            this.Font = new Font("Segoe UI", 10F);
            this.ForeColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Text = "Panel del Cliente";

            // ==== LOGO ====
            this.picLogo.Image = Image.FromFile(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "public", "FitData.jpg")
            );
            this.picLogo.Size = new Size(60, 60);
            this.picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            this.picLogo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.picLogo.Location = new Point(this.ClientSize.Width - 70, 10);

            // ==== LABEL BIENVENIDA ====
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblBienvenida.Location = new Point(20, 25);

            // ==== COMBO ====
            this.comboActividades.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboActividades.Size = new Size(300, 28);
            this.comboActividades.Location = new Point(20, 70);
            this.comboActividades.BackColor = Color.FromArgb(50, 50, 50);
            this.comboActividades.ForeColor = Color.White;
            this.comboActividades.FlatStyle = FlatStyle.Flat;

            // ==== BOTÓN VER HORARIOS ====
            StyleButton(this.btnVerHorarios, "Ver horarios", 340, 70, 160);

            // ==== DATAGRID HORARIOS ====
            this.dataGridViewHorarios.Location = new Point(20, 120);
            this.dataGridViewHorarios.Size = new Size(860, 200);
            StyleGrid(this.dataGridViewHorarios);

            // ==== DATAGRID LISTA ESPERA (NUEVO) ====
            this.dataGridViewListaEspera.Location = new Point(20, 340);
            this.dataGridViewListaEspera.Size = new Size(860, 150);
            StyleGrid(this.dataGridViewListaEspera);

            // ==== PANEL INFERIOR ====
            FlowLayoutPanel bottomPanel = new FlowLayoutPanel();
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Height = 90;
            bottomPanel.Padding = new Padding(20, 10, 20, 20);
            bottomPanel.FlowDirection = FlowDirection.LeftToRight;
            bottomPanel.WrapContents = false;

            StyleButton(this.btnReservar, "Reservar", 0, 0, 140);
            StyleButton(this.btnVerReservas, "Ver reservas", 0, 0, 160);
            StyleButton(this.btnCancelarReserva, "Cancelar reserva", 0, 0, 180);
            StyleButton(this.btnVerListaEspera, "Lista espera", 0, 0, 160);
            StyleButton(this.btnSalir, "Salir", 0, 0, 120);

            bottomPanel.Controls.Add(this.btnVerListaEspera);
            bottomPanel.Controls.Add(this.btnReservar);
            bottomPanel.Controls.Add(this.btnVerReservas);
            bottomPanel.Controls.Add(this.btnCancelarReserva);
            bottomPanel.Controls.Add(this.btnSalir);

            // ==== ADD CONTROLS ====
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.lblBienvenida);
            this.Controls.Add(this.comboActividades);
            this.Controls.Add(this.btnVerHorarios);
            this.Controls.Add(this.dataGridViewHorarios);
            this.Controls.Add(this.dataGridViewListaEspera);
            this.Controls.Add(bottomPanel);

            // ==== EVENTOS ====
            this.btnVerHorarios.Click += this.btnVerHorarios_Click;
            this.btnReservar.Click += this.btnReservar_Click;
            this.btnVerReservas.Click += this.btnVerReservas_Click;
            this.btnCancelarReserva.Click += this.btnCancelarReserva_Click;
            this.btnSalir.Click += this.btnSalir_Click;
            this.btnVerListaEspera.Click += this.btnVerListaEspera_Click;

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = Color.FromArgb(60, 60, 60);
            grid.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            grid.DefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.EnableHeadersVisualStyles = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ReadOnly = true;
        }

        private void StyleButton(FitData.Controls.BotonRedondeado btn, string texto, int x, int y, int width)
        {
            btn.Text = texto;
            btn.Location = new Point(x, y);
            btn.Size = new Size(width, 40);
            btn.BorderRadius = 18;
            btn.BorderSize = 2;
            btn.BorderColor = Color.White;
            btn.BackColor = Color.FromArgb(60, 60, 60);
            btn.ForeColor = Color.White;
            btn.Cursor = Cursors.Hand;
        }
    }
}
