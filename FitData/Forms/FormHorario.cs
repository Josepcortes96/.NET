using System;
using System.Windows.Forms;
using FitData.Entidades;
using FitData.Datos.Repositorios;

namespace FitData.Forms
{
    public partial class FormHorario : Form
    {
        private readonly HorarioRepository _horarioRepo;
        private readonly Horario _horario;
        private readonly Actividad _actividad;

        // Constructor para crear un nuevo horario
        public FormHorario(HorarioRepository horarioRepo, Actividad actividad)
        {
            InitializeComponent();
            _horarioRepo = horarioRepo;
            _actividad = actividad;
            _horario = new Horario();
            lblActividad.Text = $"Actividad: {_actividad.Nombre}";
        }

        // Constructor para editar un horario existente
        public FormHorario(HorarioRepository horarioRepo, Horario horario)
        {
            InitializeComponent();
            _horarioRepo = horarioRepo;
            _horario = horario;
            lblActividad.Text = $"Editar horario (ID: {_horario.IdHorario})";

            // ✅ Convierte TimeSpan -> DateTimePicker (usando hoy como base)
            dtpHoraInicio.Value = DateTime.Today.Add(_horario.HoraInicio);
            dtpHoraFin.Value = DateTime.Today.Add(_horario.HoraFin);
            txtDiaSemana.Text = _horario.DiaSemana;
            txtSala.Text = _horario.Sala;
            numPlazasTotales.Value = _horario.PlazasTotales;
            numPlazasOcupadas.Value = _horario.PlazasOcupadas;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // ✅ Convierte DateTimePicker -> TimeSpan
            _horario.HoraInicio = dtpHoraInicio.Value.TimeOfDay;
            _horario.HoraFin = dtpHoraFin.Value.TimeOfDay;
            _horario.DiaSemana = txtDiaSemana.Text.Trim();
            _horario.Sala = txtSala.Text.Trim();
            _horario.PlazasTotales = (int)numPlazasTotales.Value;
            _horario.PlazasOcupadas = (int)numPlazasOcupadas.Value;

            if (_horario.IdActividad == 0 && _actividad != null)
                _horario.IdActividad = _actividad.IdActividad;

            if (_horario.IdHorario == 0)
                _horarioRepo.Add(_horario);
            else
                _horarioRepo.Update(_horario);

            MessageBox.Show("Horario guardado correctamente.");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}