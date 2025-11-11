using FitData.Datos;
using FitData.Datos.Repositorios;
using FitData.Entidades;
using System;
using System.Windows.Forms;

namespace FitData.Forms
{
    public partial class FormEncargado : Form
    {
        private Usuario CurrentUser;
        private ActividadRepository actividadRepo;

        public FormEncargado(Usuario user)
        {
            InitializeComponent();
            CurrentUser = user;
            actividadRepo = new ActividadRepository(new FitDataContext());
            LoadActividades();
        }

        private void LoadActividades()
        {
            dataGridViewActividades.DataSource = null;
            dataGridViewActividades.DataSource = actividadRepo.GetByEncargado(CurrentUser.IdUsuario);
        }
    }
}

