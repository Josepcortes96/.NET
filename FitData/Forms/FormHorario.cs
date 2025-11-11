using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FitData.Entidades;
using FitData.Datos.Repositorios;

namespace FitData.Forms
{
    public partial class FormHorario : Form
    {
        private readonly HorarioRepository? _repo;
        private readonly Horario? _horario;
        private readonly bool _isEdit;
        private readonly List<Actividad> _actividades;

        public FormHorario(HorarioRepository repo, List<Actividad> actividades)
        {
            InitializeComponent();
            _repo = repo;
            _actividades = actividades ?? new List<Actividad>();
            cmbActividad.DisplayMember = "Nombre";
            cmbActividad.ValueMember = "IdActividad";
            cmbActividad.DataSource = _actividades;
            _isEdit = false;
        }

        public FormHorario(HorarioRepository repo, List<Actividad> actividades, Horario horario) : this(repo, actividades)
        {
            _horario = horario ?? throw new ArgumentNullException(nameof(horario));
            _isEdit = true;
            // rellenar campos
            cmbActividad.SelectedValue = horario.IdActividad;
            txtDia.Text = horario.DiaSemana;
            dtpInicio.Value = horario.HoraInicio;
            dtpFin.Value = horario.HoraFin;
            nudPlazas.Value = horario.PlazasTotales;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_repo == null) return;
            if (_isEdit && _horario != null)
            {
                _horario.IdActividad = (int)cmbActividad.SelectedValue;
                _horario.DiaSemana = txtDia.Text.Trim();
                _horario.HoraInicio = dtpInicio.Value;
                _horario.HoraFin = dtpFin.Value;
                _horario.PlazasTotales = (int)nudPlazas.Value;
                _repo.Update(_horario);
            }
            else
            {
                var h = new Horario
                {
                    IdActividad = (int)cmbActividad.SelectedValue,
                    DiaSemana = txtDia.Text.Trim(),
                    HoraInicio = dtpInicio.Value,
                    HoraFin = dtpFin.Value,
                    PlazasTotales = (int)nudPlazas.Value,
                    PlazasOcupadas = 0
                };
                _repo.Add(h);
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

