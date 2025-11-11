using System;
using System.Windows.Forms;
using FitData.Entidades;
using FitData.Datos.Repositorios;
using FitData.Datos;

namespace FitData.Forms
{
    public partial class FormRecepcionista : Form
    {
        private Usuario _currentUser;
        private UsuarioRepository _usuarioRepo;

        public FormRecepcionista(Usuario currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _usuarioRepo = new UsuarioRepository(new FitDataContext());
            LoadData();
        }

        private void LoadData()
        {
            // Ejemplo: cargar lista de usuarios en la grid de solo lectura
            try
            {
                dataGridViewUsuarios.DataSource = _usuarioRepo.GetAll();
                dataGridViewUsuarios.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando datos: " + ex.Message);
            }
        }
    }
}


