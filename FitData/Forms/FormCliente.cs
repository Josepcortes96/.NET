using FitData.Datos;
using FitData.Datos.Repositorios;
using FitData.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FitData.Forms
{
    public partial class FormCliente : Form
    {
        private readonly Usuario _cliente;
        private readonly ReservaRepository _reservaRepo;
        private readonly HorarioRepository _horarioRepo;
        private readonly ActividadRepository _actividadRepo;
        private readonly ListaEsperaRepository _listaRepo;

        // NUEVO: un único contexto para todo el formulario (evita problemas de tracking)
        private readonly FitDataContext _ctx;

        public FormCliente(Usuario cliente)
        {
            InitializeComponent();
            _cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));

            // Crear y reutilizar UNA sola instancia de contexto para este formulario
            _ctx = new FitDataContext();

            // Inicializar repositorios con el mismo contexto
            _reservaRepo = new ReservaRepository(_ctx);
            _horarioRepo = new HorarioRepository(_ctx);
            _actividadRepo = new ActividadRepository(_ctx);
            _listaRepo = new ListaEsperaRepository(_ctx);

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
            // 1) Obtener la fila seleccionada de forma robusta
            if (dataGridViewHorarios.CurrentRow == null && dataGridViewHorarios.SelectedRows.Count == 0 && dataGridViewHorarios.SelectedCells.Count == 0)
            {
                MessageBox.Show("Selecciona un horario para reservar.");
                return;
            }

            DataGridViewRow selectedRow = null;
            if (dataGridViewHorarios.SelectedRows.Count > 0)
                selectedRow = dataGridViewHorarios.SelectedRows[0];
            else if (dataGridViewHorarios.CurrentRow != null)
                selectedRow = dataGridViewHorarios.CurrentRow;
            else if (dataGridViewHorarios.SelectedCells.Count > 0)
                selectedRow = dataGridViewHorarios.Rows[dataGridViewHorarios.SelectedCells[0].RowIndex];

            if (selectedRow == null)
            {
                MessageBox.Show("Selecciona un horario para reservar.");
                return;
            }

            // 2) Extraer el IdHorario de la fila (si la columna se llama distinto hacemos fallback a la primera celda)
            object idObj = null;
            foreach (DataGridViewCell c in selectedRow.Cells)
            {
                var col = c.OwningColumn;
                if (string.Equals(col.DataPropertyName, "IdHorario", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(col.Name, "IdHorario", StringComparison.OrdinalIgnoreCase))
                {
                    idObj = c.Value;
                    break;
                }
            }
            if (idObj == null && selectedRow.Cells.Count > 0)
                idObj = selectedRow.Cells[0].Value;

            if (idObj == null)
            {
                MessageBox.Show("No se pudo leer el Id del horario seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(idObj.ToString(), out int idHorario) || idHorario <= 0)
            {
                MessageBox.Show("El Id del horario no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3) Asegurar que la fila Cliente existe para el usuario actual (usa _ctx)
            try
            {
                EnsureClienteExists(_cliente.IdUsuario);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo asegurar el perfil de cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4) Recuperar el horario por Id (usa el repo correcto)
            var horario = _horarioRepo.GetById(idHorario);
            if (horario == null)
            {
                MessageBox.Show("Horario no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 5) Lógica de plazas / reserva / lista de espera
            try
            {
                if (horario.PlazasOcupadas < horario.PlazasTotales)
                {
                    var nueva = new Reserva
                    {
                        IdCliente = _cliente.IdUsuario,
                        IdHorario = horario.IdHorario,
                        FechaReserva = DateTime.Now,
                        Estado = "confirmada"
                    };

                    // Dejar que el repositorio haga la verificación y el incremento de plazas
                    _reservaRepo.Add(nueva);
                   
                    // Simplemente refrescamos los datos en pantalla
                    MessageBox.Show("✅ Reserva confirmada con éxito.");

                    // refrescar: reconsultar los horarios para ver valores actualizados
                    btnVerHorarios_Click(sender, e);
                }
                else
                {
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
            }
            catch (Exception ex)
            {
                // Mensaje amigable si algo falla en la inserción en repositorios
                MessageBox.Show("Error al crear la reserva: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // 6) Refrescar la vista de horarios
            btnVerHorarios_Click(sender, e);
        }

        // NUEVO helper: asegura que exista la fila Cliente correspondiente a un usuario
        // Usa el mismo contexto _ctx y evita duplicados en el ChangeTracker
        private void EnsureClienteExists(int idUsuario)
        {
            // 1) Si ya hay una entidad Cliente con ese Id en el ChangeTracker -> nada que hacer
            var localCliente = _ctx.Clientes.Local.FirstOrDefault(c => c.IdUsuario == idUsuario);
            if (localCliente != null) return;

            // 2) Intentamos encontrarlo en la BD (Find revisa Local primero)
            var clienteEnBd = _ctx.Clientes.Find(idUsuario);
            if (clienteEnBd != null) return;

            // 3) Si no existe, obtenemos el Usuario (sin trackearlo) para copiar datos
            var usuario = _ctx.Usuarios.AsNoTracking().FirstOrDefault(u => u.IdUsuario == idUsuario);
            if (usuario == null)
            {
                throw new InvalidOperationException($"No existe Usuario con Id {idUsuario} en la base de datos.");
            }

            // 4) Crear la entidad Cliente nueva y añadirla al mismo contexto (_ctx)
            var nuevoCliente = new Cliente
            {
                IdUsuario = usuario.IdUsuario,
                
            };

            try
            {
                _ctx.Clientes.Add(nuevoCliente);
                _ctx.SaveChanges();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException dbEx)
            {
                // Mostrar inner exception y las entidades implicadas (útil para diagnosticar)
                var inner = dbEx.InnerException?.Message ?? "(sin inner exception)";
                var entries = string.Join(", ", dbEx.Entries.Select(e => e.Entity.GetType().Name));
                MessageBox.Show(
                    $"DbUpdateException al crear Cliente.\nInner: {inner}\nEntities: {entries}",
                    "Error BD", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // escribe también en Output para Visual Studio
                System.Diagnostics.Debug.WriteLine("DbUpdateException al crear Cliente: " + dbEx);
                throw; // re-lanzamos para que el flujo original lo capture si corresponde
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado al crear Cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
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
            // Si tenemos un Owner (el formulario que nos abrió), lo mostramos y cerramos este.
            if (this.Owner != null)
            {
                this.Owner.Show();
                this.Close();
                return;
            }

            // Fallback: si no hay owner, buscamos un form abierto que sea el menú (opcional)
            var menu = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.Name == "FormMenuPrincipal" || f.Name == "FormLogin");
            if (menu != null)
            {
                menu.Show();
                this.Close();
                return;
            }

            // Último recurso: abrir una nueva instancia del menú (reemplaza FormMenuPrincipal por tu formulario real)
            var nuevoMenu = new LoginForm(); 
            nuevoMenu.Show();
            this.Close();
        }

    }
}
