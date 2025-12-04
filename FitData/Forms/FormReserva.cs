// FitData/Forms/FormReserva.cs
using FitData.Datos;
using FitData.Datos.Repositorios;
using FitData.Entidades;
using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FitData.Forms
{
    public partial class FormReserva : Form
    {
        private readonly Usuario _currentUser;
        private readonly ReservaRepository _reservaRepo;

        public FormReserva(Usuario user)
        {
            InitializeComponent();
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));
            var ctx = new FitDataContext();
            _reservaRepo = new ReservaRepository(ctx);

            LoadMyReservations();
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
                })
                .ToList();

            dataGridViewMisReservas.DataSource = reservas;
        }

        
        private void btnCrear_Click(object sender, EventArgs e)
        {
            
            string input = Interaction.InputBox("Introduce el Id del horario que quieres reservar:", "Seleccionar horario", "");
            if (string.IsNullOrWhiteSpace(input)) return;

            if (!int.TryParse(input.Trim(), out int idHorario) || idHorario <= 0)
            {
                MessageBox.Show("Id de horario no válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar existencia
            using var ctx = new FitDataContext();
            var horario = ctx.Horarios.FirstOrDefault(h => h.IdHorario == idHorario);
            if (horario == null)
            {
                MessageBox.Show("No existe ese horario (se ha eliminado o era inválido).", "Horario no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear reserva
            var nueva = new Reserva
            {
                IdCliente = _currentUser.IdUsuario,
                IdHorario = idHorario,
                FechaReserva = DateTime.Now,
                Estado = "Confirmada"
            };
            _reservaRepo.Add(nueva);

            MessageBox.Show("Reserva creada correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadMyReservations();
        }


        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dataGridViewMisReservas.CurrentRow == null)
            {
                MessageBox.Show("Selecciona la reserva que quieras actualizar.", "Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idReserva = Convert.ToInt32(dataGridViewMisReservas.CurrentRow.Cells["IdReserva"].Value);

            // Cancelamos la reserva actual con contexto directo
            using (var ctx2 = new FitDataContext())
            {
                var reservaOriginal = ctx2.Reservas.FirstOrDefault(r => r.IdReserva == idReserva);
                if (reservaOriginal != null)
                {
                    ctx2.Reservas.Remove(reservaOriginal);
                    ctx2.SaveChanges();
                }
                else
                {
                    MessageBox.Show("No se pudo cancelar la reserva original.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Pedimos nuevo IdHorario con InputBox
            string input = Interaction.InputBox("Introduce el Id del nuevo horario:", "Seleccionar nuevo horario", "");
            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Has cancelado la actualización. La reserva original ha sido eliminada.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMyReservations();
                return;
            }

            if (!int.TryParse(input.Trim(), out int idHorario) || idHorario <= 0)
            {
                MessageBox.Show("Id de horario no válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadMyReservations();
                return;
            }

            // Verificamos que el nuevo horario existe
            using var ctx = new FitDataContext();
            var horario = ctx.Horarios.FirstOrDefault(h => h.IdHorario == idHorario);
            if (horario == null)
            {
                MessageBox.Show("No existe ese horario (se ha eliminado o era inválido).", "Horario no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadMyReservations();
                return;
            }

            // Creamos la nueva reserva
            var nueva = new Reserva
            {
                IdCliente = _currentUser.IdUsuario,
                IdHorario = idHorario,
                FechaReserva = DateTime.Now,
                Estado = "Confirmada"
            };
            _reservaRepo.Add(nueva);

            MessageBox.Show("Reserva actualizada correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadMyReservations();
        }


        private void btnVer_Click(object sender, EventArgs e)
        {
            if (dataGridViewMisReservas.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una reserva para ver sus detalles.", "Ver", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idReserva = Convert.ToInt32(dataGridViewMisReservas.CurrentRow.Cells["IdReserva"].Value);
            using var ctx = new FitDataContext();
            var reserva = ctx.Reservas.FirstOrDefault(r => r.IdReserva == idReserva);
            if (reserva == null)
            {
                MessageBox.Show("Reserva no encontrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadMyReservations();
                return;
            }

            string info = $"IdReserva: {reserva.IdReserva}\nIdCliente: {reserva.IdCliente}\nIdHorario: {reserva.IdHorario}\nFecha: {reserva.FechaReserva:g}\nEstado: {reserva.Estado}";
            MessageBox.Show(info, "Detalles reserva", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewMisReservas.CurrentRow == null)
            {
                MessageBox.Show("Selecciona una reserva para eliminar.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idReserva = Convert.ToInt32(dataGridViewMisReservas.CurrentRow.Cells["IdReserva"].Value);
            var confirm = MessageBox.Show("¿Confirmas eliminar esta reserva?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            using var ctx = new FitDataContext();
            var reserva = ctx.Reservas.FirstOrDefault(r => r.IdReserva == idReserva);
            if (reserva != null)
            {
                ctx.Reservas.Remove(reserva);
                ctx.SaveChanges();
                MessageBox.Show("Reserva eliminada correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo eliminar la reserva.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LoadMyReservations();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            LoadMyReservations();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
