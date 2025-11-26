// FitData/Forms/FormMonitor.cs
using System;
using System.Windows.Forms;
using FitData.Datos;
using FitData.Datos.Repositorios;
using FitData.Entidades;

namespace FitData.Forms
{
    public partial class FormMonitor : Form
    {
        private readonly Usuario _currentUser;
        private readonly ActividadRepository _actividadRepo;
        private readonly HorarioRepository _horarioRepo;

        public FormMonitor(Usuario user)
        {
            InitializeComponent();
            _currentUser = user;

            var ctx = new FitDataContext();
            _actividadRepo = new ActividadRepository(ctx);
            _horarioRepo = new HorarioRepository(ctx);

            LoadActividadesMonitor();
        }

        // =============================
        // CARGAR ACTIVIDADES DEL MONITOR
        // =============================
        private void LoadActividadesMonitor()
        {
            // Cargar actividades donde idMonitor = usuario.IdUsuario
            var actividades = _actividadRepo.GetByMonitor(_currentUser.IdUsuario);
            dataGridViewActividades.DataSource = actividades;
        }

        private void LoadHorarios(int idActividad)
        {
            dataGridViewHorarios.DataSource = _horarioRepo.GetByActividad(idActividad);
        }

        // Cuando selecciona actividad -> mostrar horarios
        private void dataGridViewActividades_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewActividades.CurrentRow != null)
            {
                var act = (Actividad)dataGridViewActividades.CurrentRow.DataBoundItem;
                LoadHorarios(act.IdActividad);
            }
        }

        // =============================
        // BOTÓN SALIR
        // =============================
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            new LoginForm().Show();
        }
    }
}
