using System;
using System.Windows.Forms;
using FitData.Datos.Repositorios;
using FitData.Datos;
using FitData.Entidades;
using FitData.Utils;

namespace FitData.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Introduce usuario y contraseña.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var repo = new UsuarioRepository(new FitDataContext());
            var usuario = repo.GetByUsername(username);

            if (usuario == null)
            {
                MessageBox.Show("Usuario o contraseña incorrecta.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            var stored = usuario.Password ?? "";

            bool loginOk = false;

            
            bool looksLikeHash = stored.Length > 30 && (stored.Contains("$") || stored.Contains("=") || stored.Length > 50);

            if (looksLikeHash)
            {
                try
                {
                  
                    if (PasswordHelper.VerifyPassword(stored, password))
                    {
                        
                        usuario.Password = password;
                        repo.Update(usuario);
                        loginOk = true;
                    }
                }
                catch
                {
                   
                    loginOk = false;
                }
            }
            else
            {
                
                loginOk = stored == password;
            }

            if (!loginOk)
            {
                MessageBox.Show("Usuario o contraseña incorrecta.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            this.Hide();
            switch (usuario.Rol?.ToLowerInvariant())
            {
                case "administrador":
                    new FormAdmin(usuario).Show();
                    break;
                case "cliente":
                    new FormCliente(usuario).Show();
                    break;
                case "encargado":
                    new FormEncargado(usuario).Show();
                    break;
                case "recepcionista":
                    new FormRecepcionista(usuario).Show();
                    break;
                case "monitor":
                    new FormMonitor(usuario).Show();  
                    break;
                default:
                    MessageBox.Show("Rol desconocido.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Show();
                    break;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var form = new FormUsuario();
            form.ShowDialog();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }
    }
}
