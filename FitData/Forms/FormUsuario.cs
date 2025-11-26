using System;
using System.Windows.Forms;
using FitData.Entidades;
using FitData.Datos;
using FitData.Datos.Repositorios;

namespace FitData.Forms
{
    public partial class FormUsuario : Form
    {
        public Usuario Usuario { get; private set; }

        public FormUsuario()
        {
            InitializeComponent();
            Usuario = new Usuario();

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            if (!DesignMode)
            {
                // runtime init
            }
        }

        public FormUsuario(Usuario u) : this()
        {
            if (u == null) throw new ArgumentNullException(nameof(u));
            Usuario = u;

            txtNombre.Text = u.Nombre;
            txtApellido.Text = u.Apellido;
            txtNif.Text = u.Nif;
            txtRol.Text = u.Rol;
            txtUsername.Text = u.Username;
           
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            Usuario.Nombre = txtNombre.Text.Trim();
            Usuario.Apellido = txtApellido.Text.Trim();
            Usuario.Nif = txtNif.Text.Trim();
            Usuario.Rol = txtRol.Text.Trim();
            Usuario.Username = txtUsername.Text.Trim();

            var plainPassword = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(Usuario.Nombre) || string.IsNullOrWhiteSpace(Usuario.Username))
            {
                MessageBox.Show("Nombre y Username son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool esNuevo = Usuario.IdUsuario == 0;
            if (esNuevo && string.IsNullOrEmpty(plainPassword))
            {
                MessageBox.Show("La contraseña es obligatoria para un nuevo usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // *** GUARDAR EN TEXTO PLANO: asignamos directamente la contraseña ***
            if (!string.IsNullOrEmpty(plainPassword))
            {
                Usuario.Password = plainPassword;
            }

            var context = new FitDataContext();
            var repo = new UsuarioRepository(context);

            var existente = repo.GetByUsername(Usuario.Username);
            if (existente != null && existente.IdUsuario != Usuario.IdUsuario)
            {
                MessageBox.Show("El username ya está en uso. Elige otro.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (esNuevo)
                repo.Add(Usuario);
            else
                repo.Update(Usuario);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
