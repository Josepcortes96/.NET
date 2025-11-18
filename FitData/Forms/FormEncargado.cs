// FitData/Forms/FormEncargado.cs
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FitData.Datos;
using FitData.Datos.Repositorios;
using FitData.Entidades;

namespace FitData.Forms
{
    public partial class FormEncargado : Form
    {
        private readonly Usuario _currentUser;
        private readonly ActividadRepository _actividadRepo;
        private readonly HorarioRepository _horarioRepo;
        private readonly ReservaRepository _reservaRepo;
        private readonly ListaEsperaRepository _listaRepo;

        public FormEncargado(Usuario user)
        {
            InitializeComponent();
            _currentUser = user;
            var ctx = new FitDataContext();
            _actividadRepo = new ActividadRepository(ctx);
            _horarioRepo = new HorarioRepository(ctx);
            _reservaRepo = new ReservaRepository(ctx);
            _listaRepo = new ListaEsperaRepository(ctx);
            LoadActividades();
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
            if (dataGridViewActividades.CurrentRow == null) return;

            // Pasa todas las actividades al FormHorario
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
    }
}
