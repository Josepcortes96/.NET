// FitData/Forms/FormHorario.cs
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FitData.Entidades;
using FitData.Datos.Repositorios;

namespace FitData.Forms
{
    public partial class FormHorario : Form
    {
        private readonly HorarioRepository? _horarioRepo;
        private readonly List<Actividad>? _actividades;
        private readonly ActividadRepository? _actividadRepo;
        private readonly Horario? _horario;
        private readonly bool _isEdit;

        public FormHorario(HorarioRepository horarioRepo)
        {
            InitializeComponent();
            _horarioRepo = horarioRepo;
            _actividades = new List<Actividad>();
            _isEdit = false;
            LoadInitial();
        }

        public FormHorario(HorarioRepository horarioRepo, List<Actividad> actividades) : this(horarioRepo)
        {
            _actividades = actividades ?? new List<Actividad>();
            _isEdit = false;
            PopulateActividadCombo();
        }

        public FormHorario(HorarioRepository horarioRepo, ActividadRepository actividadRepo) : this(horarioRepo)
        {
            _actividadRepo = actividadRepo;
            _actividades = actividadRepo?.GetAll() ?? new List<Actividad>();
            PopulateActividadCombo();
        }

        public FormHorario(HorarioRepository horarioRepo, List<Actividad> actividades, Horario horario) : this(horarioRepo, actividades)
        {
            _horario = horario ?? throw new ArgumentNullException(nameof(horario));
            _isEdit = true;
            FillFieldsWithHorario();
        }

        private void LoadInitial()
        {
            dtpHoraInicio.Format = DateTimePickerFormat.Time;
            dtpHoraFin.Format = DateTimePickerFormat.Time;
            cmbActividad.DisplayMember = "Nombre";
            cmbActividad.ValueMember = "IdActividad";
            PopulateActividadCombo();
        }

        private void PopulateActividadCombo()
        {
            if (_actividades != null)
            {
                cmbActividad.DataSource = null;
                cmbActividad.DataSource = _actividades;
            }
        }

        private void FillFieldsWithHorario()
        {
            if (_horario == null) return;

            cmbActividad.SelectedValue = _horario.IdActividad;
            txtDiaSemana.Text = _horario.DiaSemana;
            dtpHoraInicio.Value = DateTime.Today.Add(_horario.HoraInicio);
            dtpHoraFin.Value = DateTime.Today.Add(_horario.HoraFin);
            nudPlazas.Value = _horario.PlazasTotales;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_horarioRepo == null)
            {
                MessageBox.Show("Repositorio no disponible.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idActividad = cmbActividad.SelectedValue != null ? Convert.ToInt32(cmbActividad.SelectedValue) : 0;
            if (idActividad == 0)
            {
                MessageBox.Show("Selecciona una actividad.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_isEdit)
            {
                var h = new Horario
                {
                    IdActividad = idActividad,
                    DiaSemana = txtDiaSemana.Text.Trim(),
                    HoraInicio = dtpHoraInicio.Value.TimeOfDay,
                    HoraFin = dtpHoraFin.Value.TimeOfDay,
                    PlazasTotales = (int)nudPlazas.Value,
                    PlazasOcupadas = 0,

                };
                _horarioRepo.Add(h);
                MessageBox.Show("Horario creado.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (_horario == null) return;
                _horario.IdActividad = idActividad;
                _horario.DiaSemana = txtDiaSemana.Text.Trim();
                _horario.HoraInicio = dtpHoraInicio.Value.TimeOfDay;
                _horario.HoraFin = dtpHoraFin.Value.TimeOfDay;
                _horario.PlazasTotales = (int)nudPlazas.Value;
                _horarioRepo.Update(_horario);
                MessageBox.Show("Horario actualizado.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
