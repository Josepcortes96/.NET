// FitData/Forms/FormAdmin.cs
using System;
using System.Windows.Forms;
using FitData.Datos;
using FitData.Datos.Repositorios;
using FitData.Entidades;
using System.Collections.Generic;

namespace FitData.Forms
{
    public partial class FormAdmin : Form
    {
        private readonly Usuario _currentUser;
        private readonly UsuarioRepository _usuarioRepo;
        private readonly ActividadRepository _actividadRepo;
        private readonly HorarioRepository _horarioRepo;

        public FormAdmin(Usuario usuario)
        {
            InitializeComponent();
            _currentUser = usuario;

            var ctx = new FitDataContext();
            _usuarioRepo = new UsuarioRepository(ctx);
            _actividadRepo = new ActividadRepository(ctx);
            _horarioRepo = new HorarioRepository(ctx);

            LoadUsuarios();
            LoadActividades();
        }

        private void LoadUsuarios()
        {
            dataGridViewUsuarios.DataSource = _usuarioRepo.GetAll();
            if (dataGridViewUsuarios.Columns["Password"] != null)
                dataGridViewUsuarios.Columns["Password"].Visible = false;
        }

        private void LoadActividades()
        {
            dataGridViewActividades.DataSource = _actividadRepo.GetAll();
        }

        private void LoadHorarios(int idActividad)
        {
            dataGridViewHorarios.DataSource = _horarioRepo.GetByActividad(idActividad);
        }

        private void dataGridViewActividades_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewActividades.CurrentRow != null)
            {
                var act = (Actividad)dataGridViewActividades.CurrentRow.DataBoundItem;
                LoadHorarios(act.IdActividad);
            }
        }

        // ------------------ USUARIOS ------------------

        private void btnAddUsuario_Click(object sender, EventArgs e)
        {
            var form = new FormUsuario();
            if (form.ShowDialog() == DialogResult.OK)
                LoadUsuarios();
        }

        private void btnEditUsuario_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.CurrentRow == null) return;
            var user = (Usuario)dataGridViewUsuarios.CurrentRow.DataBoundItem;
            var form = new FormUsuario(user);
            if (form.ShowDialog() == DialogResult.OK)
                LoadUsuarios();
        }

        private void btnDeleteUsuario_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.CurrentRow == null) return;
            var user = (Usuario)dataGridViewUsuarios.CurrentRow.DataBoundItem;
            _usuarioRepo.Delete(user.IdUsuario);
            LoadUsuarios();
        }

        // ------------------ ACTIVIDADES ------------------

        private void btnAddActividad_Click(object sender, EventArgs e)
        {
            var form = new FormActividad(_actividadRepo);
            if (form.ShowDialog() == DialogResult.OK)
                LoadActividades();
        }

        private void btnEditActividad_Click(object sender, EventArgs e)
        {
            if (dataGridViewActividades.CurrentRow == null) return;
            var act = (Actividad)dataGridViewActividades.CurrentRow.DataBoundItem;
            var form = new FormActividad(_actividadRepo, act);
            if (form.ShowDialog() == DialogResult.OK)
                LoadActividades();
        }

        private void btnDeleteActividad_Click(object sender, EventArgs e)
        {
            if (dataGridViewActividades.CurrentRow == null) return;
            var act = (Actividad)dataGridViewActividades.CurrentRow.DataBoundItem;
            _actividadRepo.Delete(act.IdActividad);
            LoadActividades();
        }

        // ------------------ HORARIOS ------------------

        private void btnAddHorario_Click(object sender, EventArgs e)
        {
            if (dataGridViewActividades.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una actividad primero.");
                return;
            }

            // Pasa todas las actividades al constructor
            List<Actividad> actividades = _actividadRepo.GetAll();
            var form = new FormHorario(_horarioRepo, actividades);

            if (form.ShowDialog() == DialogResult.OK)
            {
                var act = (Actividad)dataGridViewActividades.CurrentRow.DataBoundItem;
                LoadHorarios(act.IdActividad);
            }
        }

        private void btnEditHorario_Click(object sender, EventArgs e)
        {
            if (dataGridViewHorarios.CurrentRow == null) return;

            var horario = (Horario)dataGridViewHorarios.CurrentRow.DataBoundItem;
            List<Actividad> actividades = _actividadRepo.GetAll();

            var form = new FormHorario(_horarioRepo, actividades, horario);
            if (form.ShowDialog() == DialogResult.OK)
                LoadHorarios(horario.IdActividad);
        }

        private void btnDeleteHorario_Click(object sender, EventArgs e)
        {
            if (dataGridViewHorarios.CurrentRow == null) return;
            var horario = (Horario)dataGridViewHorarios.CurrentRow.DataBoundItem;
            _horarioRepo.Delete(horario.IdHorario);
            LoadHorarios(horario.IdActividad);
        }

        // ------------------ SALIR ------------------

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            new LoginForm().Show();
        }

        private void btnDeleteLista_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad de eliminar lista aún no implementada.");
        }
    }
}
