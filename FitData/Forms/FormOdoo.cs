using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Linq;
using FitData.Datos;
using FitData.Entidades;
using FitData.configuracion.DTOs;

namespace FitData.Forms
{
    public class FormOdoo : Form
    {
        private readonly Usuario _currentUser;
        private PictureBox picOdoo;
        private Label lblTitle;
        private ListBox listTables;
        private Button btnExport;
        private Button btnImport;
        private Panel rightPanel;
        // UI para Batch 
        private TextBox txtBatchId;
        private DateTimePicker dtpDate;
        private TextBox txtCenter;
        private TextBox txtUser;
        private TextBox txtType;
        // Batch settings helper
        private FitData.Utils.BatchSettings _batchSettings;

        // CONFIG 
        private readonly string odooBaseUrl = "http://127.0.0.1:8069"; // cambiar por URL real
        private readonly string apiToken = "TU_SECRET_TOKEN";   // cambiar por token real


        public FormOdoo(Usuario currentUser)
        {
            _currentUser = currentUser;
            InitializeComponent();

            try
            {
                _batchSettings = FitData.Utils.BatchSettings.Load();
            }
            catch
            {
                _batchSettings = new FitData.Utils.BatchSettings();
            }

            // Aplicar valores iniciales en los controles 
            if (txtBatchId != null) txtBatchId.Text = string.IsNullOrWhiteSpace(_batchSettings.BatchId) ? $"export_{DateTime.UtcNow:yyyyMMddHHmmss}" : _batchSettings.BatchId;
            if (dtpDate != null) dtpDate.Value = _batchSettings.Date == default ? DateTime.UtcNow : _batchSettings.Date;
            if (txtCenter != null) txtCenter.Text = _batchSettings.CenterCode ?? "";
            if (txtUser != null) txtUser.Text = string.IsNullOrWhiteSpace(_batchSettings.User) ? (_currentUser?.Username ?? "") : _batchSettings.User;
            if (txtType != null) txtType.Text = _batchSettings.Type ?? "";
        }

        private void InitializeComponent()
        {
            this.Text = "Odoo - Import/Export";
            this.Size = new Size(800, 500);
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.Font = new Font("Segoe UI", 10F);

            picOdoo = new PictureBox();
            try
            {
                var logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "public", "odoo_logo.png");
                if (File.Exists(logoPath)) picOdoo.Image = System.Drawing.Image.FromFile(logoPath);
            }
            catch { }
            picOdoo.SizeMode = PictureBoxSizeMode.Zoom;
            picOdoo.Size = new Size(120, 60);
            picOdoo.Location = new Point(10, 10);
            this.Controls.Add(picOdoo);

            lblTitle = new Label();
            lblTitle.Text = "GenteFit ↔ Odoo";
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(140, 20);
            lblTitle.AutoSize = true;
            this.Controls.Add(lblTitle);

            listTables = new ListBox();
            listTables.Location = new Point(10, 90);
            listTables.Size = new Size(300, 320);
            listTables.Font = new Font("Segoe UI", 10F);
            this.Controls.Add(listTables);
            var tables = new List<string> { "Usuarios", "Clientes" };
            listTables.Items.AddRange(tables.ToArray());
            listTables.SelectedIndex = 0;

            rightPanel = new Panel();
            rightPanel.Location = new Point(330, 90);
            rightPanel.Size = new Size(440, 320);
            rightPanel.BackColor = Color.White;
            rightPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(rightPanel);

            // ------- Controles Batch  -------
            // ------- Controles Batch (ahora dentro de GroupBox y ocultable) -------
            var cbShowBatch = new CheckBox
            {
                Text = "Mostrar parámetros avanzados (Batch)",
                Location = new Point(20, 10),
                AutoSize = true
            };
            rightPanel.Controls.Add(cbShowBatch);

            // GroupBox para agrupar controles Batch (oculto por defecto)
            var gbBatch = new GroupBox
            {
                Text = "Parámetros de Lote (Batch)",
                Location = new Point(10, 35),
                Size = new Size(410, 140),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Visible = false
            };
            rightPanel.Controls.Add(gbBatch);

            // Dentro del GroupBox: BatchId
            var lblBatchId = new Label { Text = "BatchId:", Location = new Point(10, 22), Size = new Size(60, 22) };
            this.txtBatchId = new TextBox { Name = "txtBatchId", Location = new Point(80, 20), Size = new Size(320, 24), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            gbBatch.Controls.Add(lblBatchId);
            gbBatch.Controls.Add(this.txtBatchId);

            // Date
            var lblDate = new Label { Text = "Fecha (UTC):", Location = new Point(10, 50), Size = new Size(80, 22) };
            this.dtpDate = new DateTimePicker { Name = "dtpDate", Format = DateTimePickerFormat.Custom, CustomFormat = "yyyy-MM-ddTHH:mm:ss'Z'", Location = new Point(100, 48), Size = new Size(180, 24) };
            gbBatch.Controls.Add(lblDate);
            gbBatch.Controls.Add(this.dtpDate);

            // CenterCode
            var lblCenter = new Label { Text = "CenterCode:", Location = new Point(10, 78), Size = new Size(80, 22) };
            this.txtCenter = new TextBox { Name = "txtCenter", Location = new Point(100, 76), Size = new Size(140, 24) };
            gbBatch.Controls.Add(lblCenter);
            gbBatch.Controls.Add(this.txtCenter);

            // User
            var lblUser = new Label { Text = "User:", Location = new Point(250, 78), Size = new Size(40, 22) };
            this.txtUser = new TextBox { Name = "txtUser", Location = new Point(290, 76), Size = new Size(110, 24) };
            gbBatch.Controls.Add(lblUser);
            gbBatch.Controls.Add(this.txtUser);

            // Type
            var lblType = new Label { Text = "Type:", Location = new Point(10, 106), Size = new Size(40, 22) };
            this.txtType = new TextBox { Name = "txtType", Location = new Point(60, 104), Size = new Size(120, 24) };
            gbBatch.Controls.Add(lblType);
            gbBatch.Controls.Add(this.txtType);

            // Toggle show/hide
            cbShowBatch.CheckedChanged += (s, ev) =>
            {
                gbBatch.Visible = cbShowBatch.Checked;
            };

            // ==== Botones ====
            // Posición dinámica: justo debajo del GroupBox (o bajo el checkbox si oculto)
            btnExport = new Button { Text = "Exportar a Odoo", Size = new Size(160, 45), Location = new Point(40, gbBatch.Bottom + 10), Anchor = AnchorStyles.Top | AnchorStyles.Left };
            btnExport.Click += BtnExport_Click;
            rightPanel.Controls.Add(btnExport);

            btnImport = new Button { Text = "Importar desde Odoo", Size = new Size(160, 45), Location = new Point(220, gbBatch.Bottom + 10), Anchor = AnchorStyles.Top | AnchorStyles.Left };
            btnImport.Click += BtnImport_Click;
            rightPanel.Controls.Add(btnImport);

            // Información/ayuda (debajo de los botones)
            var lblInfo = new Label
            {
                Text = "Selecciona Usuarios o Clientes.\nExportar -> envía XML a Odoo.\nImportar -> trae XML desde Odoo y aplica upsert local.",
                Location = new Point(20, btnExport.Bottom + 10),
                Size = new Size(380, 130),
                AutoSize = false
            };
            rightPanel.Controls.Add(lblInfo);
        }

            // ---------------- EXPORT (síncrono) ----------------
        private void BtnExport_Click(object sender, EventArgs e)
        {
            var table = listTables.SelectedItem?.ToString() ?? "Usuarios";
            if (MessageBox.Show($"Vas a exportar '{table}' a Odoo. ¿Proceder?", "Exportar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // Guardar settings actuales para la próxima vez
            try
            {
                _batchSettings.BatchId = string.IsNullOrWhiteSpace(txtBatchId.Text) ? $"export_{DateTime.UtcNow:yyyyMMddHHmmss}" : txtBatchId.Text.Trim();
                _batchSettings.Date = dtpDate.Value.ToUniversalTime();
                _batchSettings.CenterCode = txtCenter.Text.Trim();
                _batchSettings.User = txtUser.Text.Trim();
                _batchSettings.Type = txtType.Text.Trim();
                _batchSettings.Save();
            }
            catch { /* no bloquear si falla guardar */ }

            try
            {
                var dto = BuildExportDto(table);

                var tmp = Path.Combine(Path.GetTempPath(), $"gentefit_export_{Guid.NewGuid():N}.xml");
                SerializeToXml(dto, tmp);

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiToken);
                using var content = new MultipartFormDataContent();
                var bytes = File.ReadAllBytes(tmp);
                var fileContent = new ByteArrayContent(bytes);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/xml");
                content.Add(fileContent, "file", Path.GetFileName(tmp));

                var resp = client.PostAsync($"{odooBaseUrl}/gentefit/api/import", content).GetAwaiter().GetResult();
                var text = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode) MessageBox.Show("Export OK: " + text);
                else MessageBox.Show("Error exportando: " + resp.StatusCode + "\n" + text);

                try { File.Delete(tmp); } catch { }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exportando: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------- IMPORT (síncrono) ----------------
        private void BtnImport_Click(object sender, EventArgs e)
        {
            var table = listTables.SelectedItem?.ToString() ?? "Usuarios";
            if (MessageBox.Show($"Vas a importar '{table}' desde Odoo. ¿Proceder?", "Importar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiToken);
                var resp = client.GetAsync($"{odooBaseUrl}/gentefit/api/export?table={Uri.EscapeDataString(table)}").GetAwaiter().GetResult();
                if (!resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error en export Odoo: " + resp.StatusCode);
                    return;
                }

                var xmlBytes = resp.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                var tmp = Path.Combine(Path.GetTempPath(), $"gentefit_import_{Guid.NewGuid():N}.xml");
                File.WriteAllBytes(tmp, xmlBytes);

                var serializer = new XmlSerializer(typeof(GenteFitExportDto));
                using var fs = File.OpenRead(tmp);
                var dto = (GenteFitExportDto)serializer.Deserialize(fs);

                ApplyDtoToLocalDatabase(dto);

                MessageBox.Show("Importación completada.");
                try { File.Delete(tmp); } catch { }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error importando: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------- Build DTO (síncrono) ----------------
        private GenteFitExportDto BuildExportDto(string table)
        {

            var dto = new GenteFitExportDto();

            var batchId = !string.IsNullOrWhiteSpace(txtBatchId?.Text) ? txtBatchId.Text.Trim() : $"export_{DateTime.UtcNow:yyyyMMddHHmmss}";
            var date = dtpDate != null ? dtpDate.Value.ToUniversalTime() : DateTime.UtcNow;
            var center = txtCenter?.Text?.Trim() ?? "";
            var user = !string.IsNullOrWhiteSpace(txtUser?.Text) ? txtUser.Text.Trim() : (_currentUser?.Username ?? "");
            var type = txtType?.Text?.Trim() ?? "partial";

            dto.Batch = new BatchDto
            {
                BatchId = batchId,
                Date = date,
                Origin = "GenteFit",
                CenterCode = center,
                User = user,
                Type = type
            };

            using var ctx = new FitDataContext();

            // Usuarios
            if (table == "Usuarios" || table == "Clientes")
            {
                var usuarios = ctx.Set<Usuario>().ToList();
                foreach (var u in usuarios)
                {
                    dto.Usuarios.Usuario.Add(new UsuarioDto
                    {
                        IdUsuario = u.IdUsuario,
                        Nombre = u.Nombre,
                        Apellido = u.Apellido,
                        Nif = u.Nif,
                        Rol = u.Rol,
                        Username = u.Username
                    });
                }
            }

            // Clientes (si existe tabla Clientes mapeada, úsala; si no, filtramos por rol)
            var clientesSet = ctx.Model.FindEntityType(typeof(FitData.Entidades.Cliente)) != null
                ? ctx.Set<FitData.Entidades.Cliente>().ToList()
                : null;

            if (table == "Clientes")
            {
                if (clientesSet != null)
                {
                    foreach (var c in clientesSet)
                    {
                        // si Cliente tiene navegación Usuario, intenta usarla
                        var usuarioEntity = ctx.Set<Usuario>().Find(c.IdUsuario);
                        dto.Clientes.Cliente.Add(new ClienteDto
                        {
                            IdUsuario = c.IdUsuario,
                            Nombre = usuarioEntity?.Nombre ?? "",
                            Apellido = usuarioEntity?.Apellido ?? "",
                            Nif = usuarioEntity?.Nif ?? "",
                            Username = usuarioEntity?.Username ?? "",
                            Rol = usuarioEntity?.Rol ?? "cliente"
                        });
                    }
                }
                else
                {
                    var usuariosClientes = ctx.Set<Usuario>().Where(u => (u.Rol ?? "").ToLower() == "cliente").ToList();
                    foreach (var u in usuariosClientes)
                    {
                        dto.Clientes.Cliente.Add(new ClienteDto
                        {
                            IdUsuario = u.IdUsuario,
                            Nombre = u.Nombre,
                            Apellido = u.Apellido,
                            Nif = u.Nif,
                            Username = u.Username,
                            Rol = u.Rol
                        });
                    }
                }
            }

            return dto;
        }

        // ---------------- Serializar ----------------
        private void SerializeToXml(GenteFitExportDto dto, string path)
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "http://example.org/gentefit");
            var serializer = new XmlSerializer(typeof(GenteFitExportDto));
            using var fs = File.Create(path);
            serializer.Serialize(fs, dto, ns);
        }

        // ---------------- Upsert en BD local ----------------
        private void ApplyDtoToLocalDatabase(GenteFitExportDto dto)
        {
            if (dto == null) return;
            using var ctx = new FitDataContext();

            // Usuarios upsert
            foreach (var u in dto.Usuarios.Usuario)
            {
                var existing = ctx.Set<Usuario>().Find(u.IdUsuario);
                if (existing == null)
                {
                    ctx.Set<Usuario>().Add(new Usuario
                    {
                        IdUsuario = u.IdUsuario,
                        Nombre = u.Nombre,
                        Apellido = u.Apellido,
                        Nif = u.Nif,
                        Rol = u.Rol,
                        Username = u.Username
                    });
                }
                else
                {
                    existing.Nombre = u.Nombre;
                    existing.Apellido = u.Apellido;
                    existing.Nif = u.Nif;
                    existing.Rol = u.Rol;
                    existing.Username = u.Username;
                    ctx.Set<Usuario>().Update(existing);
                }
            }

            // Clientes upsert (asegurar usuario primero)
            foreach (var c in dto.Clientes.Cliente)
            {
                var user = ctx.Set<Usuario>().Find(c.IdUsuario);
                if (user == null)
                {
                    user = new Usuario
                    {
                        IdUsuario = c.IdUsuario,
                        Nombre = c.Nombre,
                        Apellido = c.Apellido,
                        Nif = c.Nif,
                        Username = c.Username,
                        Rol = c.Rol
                    };
                    ctx.Set<Usuario>().Add(user);
                    // No SaveChanges aún; se hará al final
                }

                // Si existe entidad Cliente en el modelo, crear si faltara
                var clienteEntityType = ctx.Model.FindEntityType(typeof(FitData.Entidades.Cliente));
                if (clienteEntityType != null)
                {
                    var existingCliente = ctx.Set<FitData.Entidades.Cliente>().Find(c.IdUsuario);
                    if (existingCliente == null)
                    {
                        ctx.Set<FitData.Entidades.Cliente>().Add(new FitData.Entidades.Cliente { IdUsuario = c.IdUsuario });
                    }
                }
            }

            ctx.SaveChanges();
        }
    }
}
