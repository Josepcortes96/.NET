namespace FitData.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private FitData.Controls.BotonRedondeado btnLogin;
        private FitData.Controls.BotonRedondeado btnRegister;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.btnLogin = new FitData.Controls.BotonRedondeado();
            this.btnRegister = new FitData.Controls.BotonRedondeado();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();

            // ==== FORM (ESTILO MODERNO) ====
            this.BackColor = System.Drawing.Color.FromArgb(85, 85, 84);
            this.ClientSize = new System.Drawing.Size(400, 340);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ForeColor = System.Drawing.Color.White;
            this.Text = "Login - FitData";

            // ==== LOGO ====
            this.picLogo.Image = System.Drawing.Image.FromFile(
                System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "public", "FitData.jpg")
            );
            this.picLogo.Location = new System.Drawing.Point(135, 10);
            this.picLogo.Size = new System.Drawing.Size(130, 130);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Parent = this;  

            // ==== LABEL USERNAME ====
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(40, 150);
            this.lblUsername.Text = "Usuario:";

            // ==== TEXTBOX USERNAME ====
            this.txtUsername.Location = new System.Drawing.Point(140, 148);
            this.txtUsername.Size = new System.Drawing.Size(220, 25);
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.txtUsername.ForeColor = System.Drawing.Color.White;

            // ==== LABEL PASSWORD ====
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(40, 190);
            this.lblPassword.Text = "Contraseña:";

            // ==== TEXTBOX PASSWORD ====
            this.txtPassword.Location = new System.Drawing.Point(140, 188);
            this.txtPassword.Size = new System.Drawing.Size(220, 25);
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.txtPassword.ForeColor = System.Drawing.Color.White;
            this.txtPassword.UseSystemPasswordChar = true;

            // ==== CHECKBOX SHOW PASSWORD ====
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.Location = new System.Drawing.Point(140, 218);
            this.chkShowPassword.Text = "Mostrar contraseña";
            this.chkShowPassword.ForeColor = System.Drawing.Color.White;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);

           // ==== BOTÓN LOGIN ====

            this.btnLogin.Location = new System.Drawing.Point(140, 260);
            this.btnLogin.Size = new System.Drawing.Size(100, 40);
            this.btnLogin.Text = "Iniciar sesión";
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.BorderRadius = 18;
            this.btnLogin.BorderSize = 3;               // 🔥 BORDE BLANCO
            this.btnLogin.BorderColor = System.Drawing.Color.White;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);


            // ==== BOTÓN REGISTER  ====
            this.btnRegister.Location = new System.Drawing.Point(260, 260);
            this.btnRegister.Size = new System.Drawing.Size(100, 40);
            this.btnRegister.Text = "Registrarse";
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.BorderRadius = 18;
            this.btnRegister.BorderSize = 2;          
            this.btnRegister.BorderColor = System.Drawing.Color.White;
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);



            // ==== ADD CONTROLS ====
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.chkShowPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnRegister);

            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
