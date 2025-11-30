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
            picLogoSmall = new PictureBox();
            layout = new TableLayoutPanel();
            lblNombre = new Label();
            lblApellido = new Label();
            lblNif = new Label();
            lblRol = new Label();
            lblUsername = new Label();
            lblPassword = new Label();
            chkShowPassword = new CheckBox();
            btnSave = new FitData.Controls.BotonRedondeado();
            btnCancel = new FitData.Controls.BotonRedondeado();
            buttonPanel = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)picLogoSmall).BeginInit();
            layout.SuspendLayout();
            buttonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // picLogoSmall
            // 
            picLogoSmall.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picLogoSmall.Location = new Point(282, 10);
            picLogoSmall.Name = "picLogoSmall";
            picLogoSmall.Size = new Size(60, 60);
            picLogoSmall.SizeMode = PictureBoxSizeMode.Zoom;
            picLogoSmall.TabIndex = 0;
            picLogoSmall.TabStop = false;
            // 
            // layout
            // 
            layout.ColumnCount = 2;
            layout.ColumnStyles.Add(new ColumnStyle());
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.Controls.Add(lblNombre, 0, 0);
            layout.Controls.Add(lblApellido, 0, 1);
            layout.Controls.Add(lblNif, 0, 2);
            layout.Controls.Add(lblRol, 0, 3);
            layout.Controls.Add(lblUsername, 0, 4);
            layout.Controls.Add(lblPassword, 0, 5);
            layout.Controls.Add(chkShowPassword, 1, 6);
            layout.Dock = DockStyle.Fill;
            layout.Location = new Point(0, 0);
            layout.Name = "layout";
            layout.Padding = new Padding(40, 90, 40, 20);
            layout.RowCount = 7;
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layout.Size = new Size(600, 430);
            layout.TabIndex = 1;
            // 
            // lblNombre
            // 
            lblNombre.Location = new Point(43, 90);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(100, 23);
            lblNombre.TabIndex = 0;
            // 
            // lblApellido
            // 
            lblApellido.Location = new Point(43, 135);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(100, 23);
            lblApellido.TabIndex = 1;
            // 
            // lblNif
            // 
            lblNif.Location = new Point(43, 180);
            lblNif.Name = "lblNif";
            lblNif.Size = new Size(100, 23);
            lblNif.TabIndex = 2;
            // 
            // lblRol
            // 
            lblRol.Location = new Point(43, 225);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(100, 23);
            lblRol.TabIndex = 3;
            // 
            // lblUsername
            // 
            lblUsername.Location = new Point(43, 270);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(100, 23);
            lblUsername.TabIndex = 4;
            // 
            // lblPassword
            // 
            lblPassword.Location = new Point(43, 315);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(100, 23);
            lblPassword.TabIndex = 5;
            // 
            // chkShowPassword
            // 
            chkShowPassword.Location = new Point(149, 363);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(104, 24);
            chkShowPassword.TabIndex = 6;
            chkShowPassword.CheckedChanged += ChkShowPassword_CheckedChanged;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom;
            btnSave.BackColor = Color.FromArgb(0, 120, 215);
            btnSave.BorderColor = Color.White;
            btnSave.BorderRadius = 18;
            btnSave.BorderSize = 3;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(80, 10);
            btnSave.Margin = new Padding(80, 0, 80, 0);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 45);
            btnSave.TabIndex = 0;
            btnSave.Text = "Guardar";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom;
            btnCancel.BackColor = Color.FromArgb(60, 60, 60);
            btnCancel.BorderColor = Color.White;
            btnCancel.BorderRadius = 18;
            btnCancel.BorderSize = 3;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(320, 10);
            btnCancel.Margin = new Padding(40, 0, 40, 0);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 45);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // buttonPanel
            // 
            buttonPanel.Controls.Add(btnSave);
            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Dock = DockStyle.Bottom;
            buttonPanel.Location = new Point(0, 430);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Padding = new Padding(0, 10, 0, 10);
            buttonPanel.Size = new Size(600, 70);
            buttonPanel.TabIndex = 2;
            // 
            // FormUsuario
            // 
            BackColor = Color.FromArgb(85, 85, 84);
            ClientSize = new Size(600, 500);
            Controls.Add(picLogoSmall);
            Controls.Add(layout);
            Controls.Add(buttonPanel);
            Font = new Font("Segoe UI", 11F);
            ForeColor = Color.White;
            Name = "FormUsuario";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Usuario";
            ((System.ComponentModel.ISupportInitialize)picLogoSmall).EndInit();
            layout.ResumeLayout(false);
            buttonPanel.ResumeLayout(false);
            ResumeLayout(false);
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
        private PictureBox picLogoSmall;
        private TableLayoutPanel layout;
        private FlowLayoutPanel buttonPanel;
    }
}
