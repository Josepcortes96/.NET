using System;
using System.Drawing;
using System.Windows.Forms;

namespace FitData.Forms
{
    partial class FormUsuario
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblApellido;
        private TextBox txtApellido;
        private Label lblNif;
        private TextBox txtNif;
        private Label lblRol;
        private TextBox txtRol;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private CheckBox chkShowPassword;

        private FitData.Controls.BotonRedondeado btnSave;
        private FitData.Controls.BotonRedondeado btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // ==== FORM ==== 
            this.BackColor = Color.FromArgb(85, 85, 84);
            this.ClientSize = new Size(600, 500);
            this.Font = new Font("Segoe UI", 11F);
            this.ForeColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.Sizable;   // <- AHORA ESCALA
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Gestión de Usuario";

            // ==== LOGO ARRIBA DERECHA ==== 
            PictureBox picLogoSmall = new PictureBox();
            picLogoSmall.Image = Image.FromFile(
                System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "public", "FitData.jpg")
            );
            picLogoSmall.Size = new Size(60, 60);
            picLogoSmall.SizeMode = PictureBoxSizeMode.Zoom;
            picLogoSmall.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picLogoSmall.Location = new Point(this.ClientSize.Width - 80, 10);
            this.Controls.Add(picLogoSmall);

            // ==== TABLE LAYOUT PRINCIPAL ==== 
            TableLayoutPanel layout = new TableLayoutPanel();
            layout.ColumnCount = 2;
            layout.RowCount = 7;
            layout.Dock = DockStyle.Fill;
            layout.Padding = new Padding(40, 90, 40, 20);
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));

            // ==== CREAR CONTROLES ==== 

            lblNombre = new Label() { Text = "Nombre:", AutoSize = true };
            txtNombre = CreateTextBox();

            lblApellido = new Label() { Text = "Apellido:", AutoSize = true };
            txtApellido = CreateTextBox();

            lblNif = new Label() { Text = "NIF:", AutoSize = true };
            txtNif = CreateTextBox();

            lblRol = new Label() { Text = "Rol:", AutoSize = true };
            txtRol = CreateTextBox();

            lblUsername = new Label() { Text = "Usuario:", AutoSize = true };
            txtUsername = CreateTextBox();

            lblPassword = new Label() { Text = "Contraseña:", AutoSize = true };
            txtPassword = CreateTextBox();
            txtPassword.UseSystemPasswordChar = true;

            chkShowPassword = new CheckBox()
            {
                Text = "Mostrar contraseña",
                AutoSize = true
            };
            chkShowPassword.CheckedChanged += ChkShowPassword_CheckedChanged;

            // ==== AGREGAR A TABLA ==== 
            layout.Controls.Add(lblNombre, 0, 0);
            layout.Controls.Add(txtNombre, 1, 0);

            layout.Controls.Add(lblApellido, 0, 1);
            layout.Controls.Add(txtApellido, 1, 1);

            layout.Controls.Add(lblNif, 0, 2);
            layout.Controls.Add(txtNif, 1, 2);

            layout.Controls.Add(lblRol, 0, 3);
            layout.Controls.Add(txtRol, 1, 3);

            layout.Controls.Add(lblUsername, 0, 4);
            layout.Controls.Add(txtUsername, 1, 4);

            layout.Controls.Add(lblPassword, 0, 5);
            layout.Controls.Add(txtPassword, 1, 5);

            layout.Controls.Add(chkShowPassword, 1, 6);

            this.Controls.Add(layout);

            // ==== BOTONES INFERIORES ==== 
            btnSave = new FitData.Controls.BotonRedondeado();
            btnCancel = new FitData.Controls.BotonRedondeado();

            btnSave.Text = "Guardar";
            btnSave.Size = new Size(120, 45);
            btnSave.BackColor = Color.FromArgb(0, 120, 215);
            btnSave.BorderSize = 3;
            btnSave.Anchor = AnchorStyles.Bottom;
            btnSave.Margin = new Padding(80, 0, 80, 0); 

            btnCancel.Text = "Cancelar";
            btnCancel.Size = new Size(120, 45);
            btnCancel.BackColor = Color.FromArgb(60, 60, 60);
            btnCancel.BorderSize = 3;
            btnCancel.Anchor = AnchorStyles.Bottom;
            btnCancel.Margin = new Padding(40, 0, 40, 0);

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
            buttonPanel.FlowDirection = FlowDirection.LeftToRight;
            buttonPanel.Dock = DockStyle.Bottom;
            buttonPanel.Height = 70;
            buttonPanel.Padding = new Padding(0, 10, 0, 10);
            buttonPanel.Controls.Add(btnSave);
            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.AutoSize = false;

            this.Controls.Add(buttonPanel);

            this.ResumeLayout(false);
        }

        private TextBox CreateTextBox()
        {
            return new TextBox()
            {
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Width = 250
            };
        }

        private void ChkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }
    }
}
