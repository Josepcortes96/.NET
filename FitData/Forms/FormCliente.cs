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
        private readonly Usuario _cliente;
        private readonly ReservaRepository _reservaRepo;
        private readonly HorarioRepository _horarioRepo;
        private readonly ActividadRepository _actividadRepo;
        private readonly ListaEsperaRepository _listaRepo;

        public FormCliente(Usuario cliente)
        {
            InitializeComponent();
            _cliente = cliente;

            var context = new FitDataContext();
            _reservaRepo = new ReservaRepository(context);
            _horarioRepo = new HorarioRepository(context);
            _actividadRepo = new ActividadRepository(context);
            _listaRepo = new ListaEsperaRepository(context);

            lblBienvenida.Text = $"👋 Bienvenido, {_cliente.Nombre} ({_cliente.Username})";
            CargarActividades();
        }

        private void CargarActividades()
        {
            var actividades = _actividadRepo.GetAll();
            comboActividades.DataSource = actividades;
            comboActividades.DisplayMember = "Nombre";
            comboActividades.ValueMember = "IdActividad";
        }

        private void btnVerHorarios_Click(object sender, EventArgs e)
        {
            if (comboActividades.SelectedItem is not Actividad actividad)
            {
                MessageBox.Show("Selecciona una actividad.");
                return;
            }

            var horarios = _horarioRepo.GetByActividad(actividad.IdActividad);

            if (horarios.Count == 0)
            {
                MessageBox.Show("No hay horarios disponibles para esta actividad.");
                dataGridViewHorarios.DataSource = null;
                return;
            }

            dataGridViewHorarios.DataSource = horarios
                .Select(h => new
                {
                    h.IdHorario,
                    h.DiaSemana,
                    HoraInicio = h.HoraInicio.ToString(@"hh\:mm"),
                    HoraFin = h.HoraFin.ToString(@"hh\:mm"),
                    h.PlazasTotales,
                    h.PlazasOcupadas,
                    h.Sala
                })
                .ToList();
        }

        private void btnReservar_Click(object sender, EventArgs e)
        {
            if (dataGridViewHorarios.CurrentRow == null)
            {
                MessageBox.Show("Selecciona un horario para reservar.");
                return;
            }

            int idHorario = (int)dataGridViewHorarios.CurrentRow.Cells["IdHorario"].Value;

            // Verificar disponibilidad
            var horario = _horarioRepo.GetByActividad(0).FirstOrDefault(h => h.IdHorario == idHorario);
            if (horario == null)
            {
                MessageBox.Show("Horario no encontrado.");
                return;
            }

            if (horario.PlazasOcupadas < horario.PlazasTotales)
            {
                // Crear reserva
                var nueva = new Reserva
                {
                    IdCliente = _cliente.IdUsuario,
                    IdHorario = horario.IdHorario,
                    FechaReserva = DateTime.Now,
                    Estado = "confirmada"
                };
                _reservaRepo.Add(nueva);

                horario.PlazasOcupadas++;
                _horarioRepo.Update(horario);

                MessageBox.Show("✅ Reserva confirmada con éxito.");
            }
            else
            {
                // Añadir a lista de espera
                int posicion = _listaRepo.GetByHorario(horario.IdHorario).Count + 1;
                var nuevaLista = new ListaEspera
                {
                    IdCliente = _cliente.IdUsuario,
                    IdHorario = horario.IdHorario,
                    Posicion = posicion
                };
                _listaRepo.Add(nuevaLista);
                MessageBox.Show($"⚠️ No hay plazas disponibles. Has sido añadido a la lista de espera (posición {posicion}).");
            }

            btnVerHorarios_Click(sender, e); // refrescar tabla
        }

        private void btnVerReservas_Click(object sender, EventArgs e)
        {
            var reservas = _reservaRepo.GetByCliente(_cliente.IdUsuario);
            if (reservas.Count == 0)
            {
                MessageBox.Show("No tienes reservas actualmente.");
                dataGridViewHorarios.DataSource = null;
                return;
            }

            dataGridViewHorarios.DataSource = reservas
                .Select(r => new
                {
                    r.IdReserva,
                    r.IdHorario,
                    r.Estado,
                    r.FechaReserva
                })
                .ToList();
        }

        private void btnCancelarReserva_Click(object sender, EventArgs e)
        {
            if (dataGridViewHorarios.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una reserva para cancelar.");
                return;
            }

            if (dataGridViewHorarios.Columns.Contains("IdReserva"))
            {
                int idReserva = (int)dataGridViewHorarios.CurrentRow.Cells["IdReserva"].Value;
                _reservaRepo.Cancelar(idReserva);
                MessageBox.Show("❌ Reserva cancelada correctamente.");
                btnVerReservas_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Por favor, primero abre tus reservas antes de cancelar.");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}