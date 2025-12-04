// FitData/Forms/FormAdmin.cs
using System;
using System.Windows.Forms;
using FitData.Datos;
using FitData.Datos.Repositorios;
using FitData.Entidades;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;


namespace FitData.Forms
{
    public partial class FormAdmin : Form
    {
        private readonly Usuario _currentUser;

        private readonly FitDataContext _ctx;
        private readonly UsuarioRepository _usuarioRepo;
        private readonly ActividadRepository _actividadRepo;
        private readonly HorarioRepository _horarioRepo;
        private readonly ListaEsperaRepository _listaRepo;

        public FormAdmin(Usuario usuario)
        {
            InitializeComponent();
            _currentUser = usuario;

            // Un solo contexto compartido
            _ctx = new FitDataContext();
            _usuarioRepo = new UsuarioRepository(_ctx);
            _actividadRepo = new ActividadRepository(_ctx);
            _horarioRepo = new HorarioRepository(_ctx);
            _listaRepo = new ListaEsperaRepository(_ctx);

            // Cargar todo
            LoadUsuarios();
            LoadActividades();
            LoadHorarios();      // no depende de actividad
            LoadListaEspera();   // toda la tabla
        }

        // ==============================
        //            USUARIOS
        // ==============================
        private void LoadUsuarios()
        {
            dataGridViewUsuarios.DataSource = _usuarioRepo.GetAll();
            if (dataGridViewUsuarios.Columns["Password"] != null)
                dataGridViewUsuarios.Columns["Password"].Visible = false;
        }

        private void btnAddUsuario_Click(object sender, EventArgs e)
        {
            var f = new FormUsuario();
            if (f.ShowDialog() == DialogResult.OK)
                LoadUsuarios();
        }

        private async void btnSyncOdoo_Click(object sender, EventArgs e)
            {
                MessageBox.Show(">>> INICIO: Se pulsó el botón de sincronización", "DEBUG");

                try
                {
                    btnSyncOdoo.Enabled = false;
                    btnSyncOdoo.Text = "Sincronizando...";

                

                    await Program.SincronizarOdooAsync();

                

                    btnSyncOdoo.Text = "Sincronizar Odoo";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ">>> EXCEPCIÓN CAPTURADA <<<\n\n" +
                        "Mensaje: " + ex.Message + "\n\n" +
                        "StackTrace:\n" + ex.StackTrace,
                        "ERROR DEBUG",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    btnSyncOdoo.Text = "Sincronizar Odoo";
                }
                finally
                {
                    btnSyncOdoo.Enabled = true;
                
                }
            }
         private string FixLength(string value, int max)
{
    if (string.IsNullOrWhiteSpace(value))
        return new string('X', max);

    value = value.Trim();

    return value.Length > max ? value.Substring(0, max) : value;
}


          private async void btnImportFromOdoo_Click(object sender, EventArgs e)
{
    try
    {
        string apiKey = "fitdata";
        string url = "http://136.144.233.39:8069/gentefit/users";

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

            var response = await client.GetAsync(url);
            string json = await response.Content.ReadAsStringAsync();

            JObject parsed = JObject.Parse(json);

            if (parsed["error"] != null)
            {
                MessageBox.Show("Error desde Odoo: " + parsed["error"]);
                return;
            }

            var list = parsed["users"].ToObject<List<OdooUserDto>>();

            int insertados = 0, actualizados = 0;

            using (var db = new FitDataContext())
            {
                foreach (var u in list)
                {
                   // Normalizamos email para evitar nulos
string email = u.email ?? $"odoo{u.id}@fitdata.com";
email = FixLength(email, 15);

// 1️⃣ Buscar si ya existe por nombre
var existePorNombre = db.Usuarios.FirstOrDefault(x => x.Nombre == u.name);

// 2️⃣ Buscar si ya existe por email
var existePorEmail = db.Usuarios.FirstOrDefault(x => x.Username == email);

// SI YA EXISTE POR NOMBRE O EMAIL → NO INSERTAR
if (existePorNombre != null || existePorEmail != null)
{
    DebugLog($"Usuario '{u.name}' ya existe por nombre o por email. Omitido.");
    continue; // NO insertamos ni actualizamos
}

// 3️⃣ Buscar si coincide por ID (Odoo)
var existe = db.Usuarios.FirstOrDefault(x => x.IdUsuario == u.id);

if (existe == null)
{
    db.Usuarios.Add(new Usuario
    {
        Nombre = FixLength(u.name ?? "OdooUser", 15),
        Apellido = FixLength("Odoo", 15),

        // Generamos NIF aleatorio, no importa para tu sistema
        Nif = Guid.NewGuid().ToString("N").Substring(0, 10),

        Rol = FixLength("cliente", 20),
        Username = email,
        Password = FixLength("1234", 15)
    });

    insertados++;
}
else
{
    // Actualizar SOLO si lo tenías por ID (opcional)
    existe.Nombre = FixLength(u.name ?? existe.Nombre, 15);
    existe.Username = email;

    actualizados++;
}


                   
                }

                await db.SaveChangesAsync();
            }

            MessageBox.Show(
                $"Importación completada.\nInsertados: {insertados}\nActualizados: {actualizados}",
                "OK", MessageBoxButtons.OK, MessageBoxIcon.Information
            );

            CargarUsuarios();
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("ERROR REAL:\n" + (ex.InnerException?.Message ?? ex.Message),
            "EXCEPCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}


private void CargarUsuarios()
{
    try
    {
        using (var db = new FitDataContext())
        {
            var lista = db.Usuarios
                .Select(u => new
                {
                    u.IdUsuario,
                    u.Nombre,
                    u.Apellido,
                    Email = u.Username,
                    u.Rol
                })
                .ToList();

            dataGridViewUsuarios.DataSource = lista;
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error al cargar usuarios: " + ex.Message);
    }
}



        private void btnEditUsuario_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.CurrentRow == null) return;

            var user = (Usuario)dataGridViewUsuarios.CurrentRow.DataBoundItem;
            var f = new FormUsuario(user);

            if (f.ShowDialog() == DialogResult.OK)
                LoadUsuarios();
        }

        private void btnDeleteUsuario_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsuarios.CurrentRow == null) return;

            var user = (Usuario)dataGridViewUsuarios.CurrentRow.DataBoundItem;
            _usuarioRepo.Delete(user.IdUsuario);
            LoadUsuarios();
        }


        // ==============================
        //          ACTIVIDADES
        // ==============================
        private void LoadActividades()
        {
            dataGridViewActividades.DataSource = _actividadRepo.GetAll();
        }

        private void btnAddActividad_Click(object sender, EventArgs e)
        {
            var f = new FormActividad(_actividadRepo);
            if (f.ShowDialog() == DialogResult.OK)
                LoadActividades();
        }

        private void btnEditActividad_Click(object sender, EventArgs e)
        {
            if (dataGridViewActividades.CurrentRow == null) return;

            var act = (Actividad)dataGridViewActividades.CurrentRow.DataBoundItem;
            var f = new FormActividad(_actividadRepo, act);

            if (f.ShowDialog() == DialogResult.OK)
                LoadActividades();
        }

        private void btnDeleteActividad_Click(object sender, EventArgs e)
        {
            if (dataGridViewActividades.CurrentRow == null) return;

            var act = (Actividad)dataGridViewActividades.CurrentRow.DataBoundItem;
            _actividadRepo.Delete(act.IdActividad);
            LoadActividades();
        }

// ======================================
//              DEBUG LOGGER
// ======================================
private void DebugLog(string msg)
{
    string path = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "odoo_import_log.txt"
    );

    try
    {
        File.AppendAllText(path, 
            $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {msg}\n");
    }
    catch
    {
        // En caso de error al escribir log, no romper la app
    }
}

        // ==============================
        //             HORARIOS
        // ==============================
private void LoadHorarios()
{
    var horarios = _horarioRepo.GetAll();

    var lista = horarios
        .Join(
            _ctx.Actividades,
            h => h.IdActividad,
            a => a.IdActividad,
            (h, a) => new
            {
                h.IdHorario,
                Actividad = a.Nombre,
                Dia = h.DiaSemana,
                Inicio = h.HoraInicio.ToString(),
                Fin = h.HoraFin.ToString(),
                a.Sala,
                h.PlazasTotales,
                h.PlazasOcupadas
            }
        )
        .ToList();

    dataGridViewHorarios.DataSource = lista;
}


        private void btnAddHorario_Click(object sender, EventArgs e)
        {
            List<Actividad> acts = _actividadRepo.GetAll();
            var f = new FormHorario(_horarioRepo, acts);

            if (f.ShowDialog() == DialogResult.OK)
                LoadHorarios();
        }

        private void btnEditHorario_Click(object sender, EventArgs e)
        {
            if (dataGridViewHorarios.CurrentRow == null) return;

            var horario = (Horario)dataGridViewHorarios.CurrentRow.DataBoundItem;
            List<Actividad> acts = _actividadRepo.GetAll();

            var f = new FormHorario(_horarioRepo, acts, horario);

            if (f.ShowDialog() == DialogResult.OK)
                LoadHorarios();
        }

        private void btnDeleteHorario_Click(object sender, EventArgs e)
        {
            if (dataGridViewHorarios.CurrentRow == null) return;

            var horario = (Horario)dataGridViewHorarios.CurrentRow.DataBoundItem;
            _horarioRepo.Delete(horario.IdHorario);

            LoadHorarios();
        }


        // ==============================
        //        LISTA DE ESPERA
        // ==============================
        private void LoadListaEspera()
        {
            dataGridViewLista.DataSource = _listaRepo.GetAll();
        }

        private void btnDeleteLista_Click(object sender, EventArgs e)
        {
            if (dataGridViewLista.CurrentRow == null) return;

            var item = (ListaEspera)dataGridViewLista.CurrentRow.DataBoundItem;
            _listaRepo.Delete(item.IdLista);

            // Rearmar posiciones por horario
            _listaRepo.ReorderPositions(item.IdHorario);

            LoadListaEspera();
        }


        // ==============================
        //             SALIR
        // ==============================
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            new LoginForm().Show();
        }
    }
}
