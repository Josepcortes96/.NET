using System;
using System.Linq;
using System.Windows.Forms;
using FitData.Entidades;
using FitData.Datos;
using FitData.Datos.Repositorios;

namespace FitData.Forms
{
    public partial class FormCliente : Form
    {
        private readonly Usuario _currentUser;
        private readonly ReservaRepository _reservaRepo;
        private readonly HorarioRepository _horarioRepo;
        private readonly ActividadRepository _actividadRepo;
        private readonly ListaEsperaRepository _listaRepo;

        public FormCliente(Usuario user)
        {
            InitializeComponent();
            _currentUser = user;

            var ctx = new FitDataContext();
            _reservaRepo = new ReservaRepository(ctx);
            _horarioRepo = new HorarioRepository(ctx);
            _actividadRepo = new ActividadRepository(ctx);
            _listaRepo = new ListaEsperaRepository(ctx);

            LoadInitialData();
        }

        private void LoadInitialData()
        {
            // cargar actividades en combobox
            var acts = _actividadRepo.GetAll();
            cmbActividad.DisplayMember = "Nombre";
            cmbActividad.ValueMember = "IdActividad";
            cmbActividad.DataSource = acts;

            // seleccionar hoy por defecto
            monthCalendar.SelectionStart = DateTime.Today;
            monthCalendar.SelectionEnd = DateTime.Today;

            // Cargar grids
            LoadAvailableHorarios();
            LoadMyReservations();
            LoadMyWaitings();
        }

        private void LoadAvailableHorarios()
        {
            if (cmbActividad.SelectedValue == null) return;

            int idAct = Convert.ToInt32(cmbActividad.SelectedValue);
            DateTime selectedDate = monthCalendar.SelectionStart.Date;

            var horarios = _horarioRepo.GetByActividadAndDate(idAct, selectedDate)
                .Select(h => new
                {
                    h.IdHorario,
                    Actividad = _actividadRepo.GetAll().FirstOrDefault(a => a.IdActividad == h.IdActividad)?.Nombre ?? ("Act " + h.IdActividad),
                    Dia = h.DiaSemana,
                    HoraInicio = h.HoraInicio.ToString("HH:mm"),
                    HoraFin = h.HoraFin.ToString("HH:mm"),
                    PlazasTotales = h.PlazasTotales,
                    PlazasOcupadas = h.PlazasOcupadas,
                    PlazasLibres = Math.Max(0, h.PlazasTotales - h.PlazasOcupadas)
                }).ToList();

            dataGridViewHorarios.DataSource = horarios;

            // Ocultar columnas técnicas si las hubiera
            if (dataGridViewHorarios.Columns["IdHorario"] != null)
                dataGridViewHorarios.Columns["IdHorario"].Visible = true;
        }

        private void LoadMyReservations()
        {
            var reservas = _reservaRepo.GetByCliente(_currentUser.IdUsuario)
                .Select(r => new
                {
                    r.IdReserva,
                    r.IdHorario,
                    Fecha = r.FechaReserva.ToString("g"),
                    r.Estado
                }).ToList();

            dataGridViewReservas.DataSource = reservas;
        }

        private void LoadMyWaitings()
        {
            var waits = _listaRepo.GetAllByCliente(_currentUser.IdUsuario)
                .Select(l => new
                {
                    l.IdLista,
                    l.IdHorario,
                    l.Posicion
                }).ToList();

            dataGridViewLista.DataSource = waits;
        }

        // evento: cambio actividad
        private void cmbActividad_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAvailableHorarios();
        }

        // evento: cambio fecha en el calendario
        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            LoadAvailableHorarios();
        }

        private void btnMakeReservation_Click(object sender, EventArgs e)
        {
            if (dataGridViewHorarios.CurrentRow == null)
            {
                MessageBox.Show("Selecciona un horario en la lista (click en la fila).", "Reservar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idHorario = Convert.ToInt32(dataGridViewHorarios.CurrentRow.Cells["IdHorario"].Value);

            var result = _reservaRepo.TryAddReservation(_currentUser.IdUsuario, idHorario);

            if (result == ReservationResult.Confirmed) MessageBox.Show("Reserva confirmada.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (result == ReservationResult.AddedToWaitingList) MessageBox.Show("Horario lleno. Añadido a la lista de espera.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (result == ReservationResult.AlreadyReserved) MessageBox.Show("Ya tienes reserva para este horario.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("No se pudo crear la reserva.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            LoadAll();
        }

        private void btnCancelReservation_Click(object sender, EventArgs e)
        {
            if (dataGridViewReservas.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una reserva para cancelar.", "Cancelar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idReserva = Convert.ToInt32(dataGridViewReservas.CurrentRow.Cells["IdReserva"].Value);
            var ok = _reservaRepo.CancelReservation(idReserva);
            if (ok) MessageBox.Show("Reserva cancelada.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("No se pudo cancelar la reserva.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            LoadAll();
        }

        private void btnEditReservation_Click(object sender, EventArgs e)
        {
            // editar: cancelamos la reserva y pedimos que el usuario seleccione otro horario y pulse Reservar
            if (dataGridViewReservas.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una reserva a editar.", "Editar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idReserva = Convert.ToInt32(dataGridViewReservas.CurrentRow.Cells["IdReserva"].Value);
            var confirm = MessageBox.Show("Se cancelará la reserva seleccionada. ¿Continuar para elegir otro horario?", "Editar reserva", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            if (!_reservaRepo.CancelReservation(idReserva))
            {
                MessageBox.Show("No se pudo cancelar la reserva.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Reserva cancelada. Selecciona el nuevo horario y pulsa 'Reservar'.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadAll();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void btnLeaveWaiting_Click(object sender, EventArgs e)
        {
            if (dataGridViewLista.CurrentRow == null)
            {
                MessageBox.Show("Selecciona un elemento de la lista de espera.", "Salir lista", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idLista = Convert.ToInt32(dataGridViewLista.CurrentRow.Cells["IdLista"].Value);
            _listaRepo.Delete(idLista);
            _listaRepo.ReorderPositions(Convert.ToInt32(dataGridViewLista.CurrentRow.Cells["IdHorario"].Value));
            MessageBox.Show("Has salido de la lista de espera.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadAll();
        }

        private void LoadAll()
        {
            LoadAvailableHorarios();
            LoadMyReservations();
            LoadMyWaitings();
        }
    }
}
