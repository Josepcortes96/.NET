using System;
using System.Windows.Forms;
using FitData.Datos;
using FitData.Datos.Repositorios;
using FitData.Entidades;

namespace FitData.Forms
{
    public partial class FormActividad : Form
    {
        private readonly ActividadRepository _repo;
        private readonly Actividad _actividad;
        private readonly Usuario? _usuario; // ahora acepta null para evitar aviso CS8618

        // ✅ Constructor por defecto (crear nueva actividad)
        public FormActividad()
        {
            InitializeComponent();
            _repo = new ActividadRepository(new FitDataContext());
            _actividad = new Actividad();
        }

        // ✅ Constructor con usuario (por ejemplo, encargado o admin)
        public FormActividad(Usuario usuario) : this()
        {
            _usuario = usuario;
        }

        // ✅ Constructor adicional para compatibilidad (solo con repositorio)
        public FormActividad(ActividadRepository repo) : this()
        {
            _repo = repo;
        }

        // ✅ Constructor con repositorio y actividad (editar)
        public FormActividad(ActividadRepository repo, Actividad actividad)
        {
            InitializeComponent();
            _repo = repo;
            _actividad = actividad ?? new Actividad();

            // Rellenar los campos si se edita una actividad existente
            txtNombre.Text = _actividad.Nombre;
            txtDescripcion.Text = _actividad.Descripcion;
            txtNivelIntensidad.Text = _actividad.NivelIntensidad;
            txtSala.Text = _actividad.Sala;
            txtIdMonitor.Text = _actividad.IdMonitor.ToString();
            txtIdEncargado.Text = _actividad.IdEncargado?.ToString() ?? string.Empty;
        }

        // ✅ Botón Guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(txtIdMonitor.Text, out int idMonitor))
                {
                    MessageBox.Show("El ID del monitor debe ser numérico.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(txtIdEncargado.Text, out int idEncargado))
                {
                    MessageBox.Show("El ID del encargado debe ser numérico.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _actividad.Nombre = txtNombre.Text.Trim();
                _actividad.Descripcion = txtDescripcion.Text.Trim();
                _actividad.NivelIntensidad = txtNivelIntensidad.Text.Trim();
                _actividad.Sala = txtSala.Text.Trim();
                _actividad.IdMonitor = idMonitor;
                _actividad.IdEncargado = idEncargado;

              
                if (_actividad.IdActividad == 0)
                    _repo.Add(_actividad);
                else
                    _repo.Update(_actividad);

                MessageBox.Show("✅ Actividad guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error al guardar la actividad:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}