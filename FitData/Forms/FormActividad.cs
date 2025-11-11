using System;
using System.Windows.Forms;
using FitData.Entidades;
using FitData.Datos.Repositorios;

namespace FitData.Forms
{
    public partial class FormActividad : Form
    {
        private readonly ActividadRepository? _repo;
        private readonly Actividad? _actividad;
        private readonly bool _isEdit;

        public FormActividad(ActividadRepository repo)
        {
            InitializeComponent();
            _repo = repo;
            _isEdit = false;
        }

        public FormActividad(ActividadRepository repo, Actividad actividad) : this(repo)
        {
            _actividad = actividad ?? throw new ArgumentNullException(nameof(actividad));
            _isEdit = true;
            // cargar campos
            txtNombre.Text = actividad.Nombre;
            txtDescripcion.Text = actividad.Descripcion;
            txtSala.Text = actividad.Sala;
            txtNivel.Text = actividad.NivelIntensidad;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_repo == null) return;

            if (_isEdit && _actividad != null)
            {
                _actividad.Nombre = txtNombre.Text.Trim();
                _actividad.Descripcion = txtDescripcion.Text.Trim();
                _actividad.Sala = txtSala.Text.Trim();
                _actividad.NivelIntensidad = txtNivel.Text.Trim();
                _repo.Update(_actividad);
            }
            else
            {
                var a = new Actividad
                {
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    Sala = txtSala.Text.Trim(),
                    NivelIntensidad = txtNivel.Text.Trim()
                };
                _repo.Add(a);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

