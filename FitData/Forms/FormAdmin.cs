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

        private readonly FitDataContext _ctx;
        private readonly UsuarioRepository _usuarioRepo;
        private readonly ActividadRepository _actividadRepo;
        private readonly HorarioRepository _horarioRepo;
        private readonly ListaEsperaRepository _listaRepo;

        public FormAdmin(Usuario usuario)
        {
            InitializeComponent();
            _currentUser = usuario;

            // Un solo contexto compartido
            _ctx = new FitDataContext();
            _usuarioRepo = new UsuarioRepository(_ctx);
            _actividadRepo = new ActividadRepository(_ctx);
            _horarioRepo = new HorarioRepository(_ctx);
            _listaRepo = new ListaEsperaRepository(_ctx);

            // Cargar todo
            LoadUsuarios();
            LoadActividades();
            LoadHorarios();      // no depende de actividad
            LoadListaEspera();   // toda la tabla
        }

        // ==============================
        //            USUARIOS
        // ==============================
        private void LoadUsuarios()
        {
            dataGridViewUsuarios.DataSource = _usuarioRepo.GetAll();
            if (dataGridViewUsuarios.Columns["Password"] != null)
                dataGridViewUsuarios.Columns["Password"].Visible = false;
        }

        private void btnAddUsuario_Click(object sender, EventArgs e)
        {
            var f = new FormUsuario();
            if (f.ShowDialog() == DialogResult.OK)
                LoadUsuarios();
        }

        private void btnEditUsuario_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.CurrentRow == null) return;

            var user = (Usuario)dataGridViewUsuarios.CurrentRow.DataBoundItem;
            var f = new FormUsuario(user);

            if (f.ShowDialog() == DialogResult.OK)
                LoadUsuarios();
        }

        private void btnDeleteUsuario_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.CurrentRow == null) return;

            var user = (Usuario)dataGridViewUsuarios.CurrentRow.DataBoundItem;
            _usuarioRepo.Delete(user.IdUsuario);
            LoadUsuarios();
        }


        // ==============================
        //          ACTIVIDADES
        // ==============================
        private void LoadActividades()
        {
            dataGridViewActividades.DataSource = _actividadRepo.GetAll();
        }

        private void btnAddActividad_Click(object sender, EventArgs e)
        {
            var f = new FormActividad(_actividadRepo);
            if (f.ShowDialog() == DialogResult.OK)
                LoadActividades();
        }

        private void btnEditActividad_Click(object sender, EventArgs e)
        {
            if (dataGridViewActividades.CurrentRow == null) return;

            var act = (Actividad)dataGridViewActividades.CurrentRow.DataBoundItem;
            var f = new FormActividad(_actividadRepo, act);

            if (f.ShowDialog() == DialogResult.OK)
                LoadActividades();
        }

        private void btnDeleteActividad_Click(object sender, EventArgs e)
        {
            if (dataGridViewActividades.CurrentRow == null) return;

            var act = (Actividad)dataGridViewActividades.CurrentRow.DataBoundItem;
            _actividadRepo.Delete(act.IdActividad);
            LoadActividades();
        }


        // ==============================
        //             HORARIOS
        // ==============================
private void LoadHorarios()
{
    var horarios = _horarioRepo.GetAll();

    var lista = horarios
        .Join(
            _ctx.Actividades,
            h => h.IdActividad,
            a => a.IdActividad,
            (h, a) => new
            {
                h.IdHorario,
                Actividad = a.Nombre,
                Dia = h.DiaSemana,
                Inicio = h.HoraInicio.ToString(),
                Fin = h.HoraFin.ToString(),
                a.Sala,
                h.PlazasTotales,
                h.PlazasOcupadas
            }
        )
        .ToList();

    dataGridViewHorarios.DataSource = lista;
}


        private void btnAddHorario_Click(object sender, EventArgs e)
        {
            List<Actividad> acts = _actividadRepo.GetAll();
            var f = new FormHorario(_horarioRepo, acts);

            if (f.ShowDialog() == DialogResult.OK)
                LoadHorarios();
        }

        private void btnEditHorario_Click(object sender, EventArgs e)
        {
            if (dataGridViewHorarios.CurrentRow == null) return;

            var horario = (Horario)dataGridViewHorarios.CurrentRow.DataBoundItem;
            List<Actividad> acts = _actividadRepo.GetAll();

            var f = new FormHorario(_horarioRepo, acts, horario);

            if (f.ShowDialog() == DialogResult.OK)
                LoadHorarios();
        }

        private void btnDeleteHorario_Click(object sender, EventArgs e)
        {
            if (dataGridViewHorarios.CurrentRow == null) return;

            var horario = (Horario)dataGridViewHorarios.CurrentRow.DataBoundItem;
            _horarioRepo.Delete(horario.IdHorario);

            LoadHorarios();
        }


        // ==============================
        //        LISTA DE ESPERA
        // ==============================
        private void LoadListaEspera()
        {
            dataGridViewLista.DataSource = _listaRepo.GetAll();
        }

        private void btnDeleteLista_Click(object sender, EventArgs e)
        {
            if (dataGridViewLista.CurrentRow == null) return;

            var item = (ListaEspera)dataGridViewLista.CurrentRow.DataBoundItem;
            _listaRepo.Delete(item.IdLista);

            // Rearmar posiciones por horario
            _listaRepo.ReorderPositions(item.IdHorario);

            LoadListaEspera();
        }


        // ==============================
        //             SALIR
        // ==============================
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            new LoginForm().Show();
        }
    }
}
