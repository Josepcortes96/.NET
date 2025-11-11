// FitData/Forms/FormAdmin.cs
using System;
using System.Windows.Forms;
using FitData.Datos;
using FitData.Datos.Repositorios;
using FitData.Entidades;

namespace FitData.Forms
{
    public partial class FormAdmin : Form
    {
        private readonly UsuarioRepository _usuarioRepo;
        private readonly ActividadRepository _actividadRepo;
        private readonly HorarioRepository _horarioRepo;
        private readonly ListaEsperaRepository _listaRepo;

        public FormAdmin(Usuario currentUser)
        {
            InitializeComponent();

            var context = new FitDataContext();
            _usuarioRepo = new UsuarioRepository(context);
            _actividadRepo = new ActividadRepository(context);
            _horarioRepo = new HorarioRepository(context);
            _listaRepo = new ListaEsperaRepository(context);

            LoadAllData();
        }

        private void LoadAllData()
        {
            try
            {
                dataGridViewUsuarios.DataSource = _usuarioRepo.GetAll();
                dataGridViewActividades.DataSource = _actividadRepo.GetAll();
                // Horarios y lista se llenan cuando selecciones actividad/horario si lo implementas en UI
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando datos: " + ex.Message);
            }
        }

        // Usuarios
        private void btnAddUsuario_Click(object sender, EventArgs e)
        {
            var form = new FormUsuario();
            if (form.ShowDialog() == DialogResult.OK)
                dataGridViewUsuarios.DataSource = _usuarioRepo.GetAll();
        }

        private void btnEditUsuario_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.CurrentRow == null) return;
            var usuario = (Usuario)dataGridViewUsuarios.CurrentRow.DataBoundItem;
            var form = new FormUsuario(usuario);
            if (form.ShowDialog() == DialogResult.OK)
                dataGridViewUsuarios.DataSource = _usuarioRepo.GetAll();
        }

        private void btnDeleteUsuario_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.CurrentRow == null) return;
            var usuario = (Usuario)dataGridViewUsuarios.CurrentRow.DataBoundItem;
            _usuarioRepo.Delete(usuario.IdUsuario);
            dataGridViewUsuarios.DataSource = _usuarioRepo.GetAll();
        }

        // Actividades
        private void btnAddActividad_Click(object sender, EventArgs e)
        {
            var form = new FormActividad(_actividadRepo);
            if (form.ShowDialog() == DialogResult.OK)
                dataGridViewActividades.DataSource = _actividadRepo.GetAll();
        }

        private void btnEditActividad_Click(object sender, EventArgs e)
        {
            if (dataGridViewActividades.CurrentRow == null) return;
            var act = (Actividad)dataGridViewActividades.CurrentRow.DataBoundItem;
            var form = new FormActividad(_actividadRepo, act);
            if (form.ShowDialog() == DialogResult.OK)
                dataGridViewActividades.DataSource = _actividadRepo.GetAll();
        }

        private void btnDeleteActividad_Click(object sender, EventArgs e)
        {
            if (dataGridViewActividades.CurrentRow == null) return;
            var act = (Actividad)dataGridViewActividades.CurrentRow.DataBoundItem;
            _actividadRepo.Delete(act.IdActividad);
            dataGridViewActividades.DataSource = _actividadRepo.GetAll();
        }

        // Horarios
        private void btnAddHorario_Click(object sender, EventArgs e)
        {
            var form = new FormHorario(_horarioRepo, _actividadRepo.GetAll());
            if (form.ShowDialog() == DialogResult.OK)
            {
                // opcional: refrescar grilla de horarios según la actividad seleccionada
            }
        }

        // <-- AQUÍ: handler que faltaba (añádelo)
        private void btnEditHorario_Click(object sender, EventArgs e)
        {
            if (dataGridViewHorarios.CurrentRow == null) return;
            var h = (Horario)dataGridViewHorarios.CurrentRow.DataBoundItem;
            var form = new FormHorario(_horarioRepo, _actividadRepo.GetAll(), h);
            if (form.ShowDialog() == DialogResult.OK)
            {
                // refrescar si estás mostrando horarios
                // Ejemplo: dataGridViewHorarios.DataSource = _horarioRepo.GetByActividad(h.IdActividad);
            }
        }

        private void btnDeleteHorario_Click(object sender, EventArgs e)
        {
            if (dataGridViewHorarios.CurrentRow == null) return;
            var h = (Horario)dataGridViewHorarios.CurrentRow.DataBoundItem;
            _horarioRepo.Delete(h.IdHorario);
            // opcional: refrescar
        }

        // Lista espera
        private void btnDeleteLista_Click(object sender, EventArgs e)
        {
            if (dataGridViewLista.CurrentRow == null) return;
            var l = (ListaEspera)dataGridViewLista.CurrentRow.DataBoundItem;
            _listaRepo.Delete(l.IdLista);
            // refrescar si procede
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            new LoginForm().Show();
        }
    }
}
