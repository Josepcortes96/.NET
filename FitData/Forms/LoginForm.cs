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

            // 1) Si el valor almacenado _parece_ un hash, intentamos verificar con PasswordHelper
            var stored = usuario.Password ?? "";

            bool loginOk = false;

            // Heurística: muchos hash (PBKDF2/BCrypt/Base64) tienen caracteres '=' al final en base64 o contienen '$'
            // Pero en tu caso usábamos probablemente Base64 terminando en '='
            bool looksLikeHash = stored.Length > 30 && (stored.Contains("$") || stored.Contains("=") || stored.Length > 50);

            if (looksLikeHash)
            {
                try
                {
                    // Si verifica, migramos: guardamos la contraseña en texto plano en la BD
                    if (PasswordHelper.VerifyPassword(stored, password))
                    {
                        // migración: guardamos la contraseña en texto plano (sin hash)
                        usuario.Password = password;
                        repo.Update(usuario);
                        loginOk = true;
                    }
                }
                catch
                {
                    // si la verificación lanza, fallback después
                    loginOk = false;
                }
            }
            else
            {
                // valor actual no parece hash -> comparación directa
                loginOk = stored == password;
            }

            if (!loginOk)
            {
                MessageBox.Show("Usuario o contraseña incorrecta.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Login correcto — abrir form según rol
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
                    new FormMonitor(usuario).Show();  // <-- TU FORM DE MONITOR
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
