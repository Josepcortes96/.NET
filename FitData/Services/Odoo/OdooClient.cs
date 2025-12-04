using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitData.Services.Odoo
{
    public class OdooClient
    {
        private readonly string _db;
        private readonly string _user;
        private readonly string _password;
        private readonly HttpClient _client;
        private int _uid;

        public OdooClient(string db, string user, string password)
        {
            _db = db;
            _user = user;
            _password = password;
            _client = new HttpClient();
        }

        // ============================================================
        //  MÉTODO CENTRAL JSON-RPC CON DEPURACIÓN
        // ============================================================
        private async Task<JsonDocument> CallAsync(object payload)
        {
            string json = JsonSerializer.Serialize(payload);

            Console.WriteLine("\n===============================");
            Console.WriteLine(">>> JSON ENVIADO A ODOO:");
            Console.WriteLine(json);
            Console.WriteLine("===============================\n");

            var response = await _client.PostAsync(
                "http://136.144.233.39:8069/jsonrpc",
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            string result = await response.Content.ReadAsStringAsync();

            Console.WriteLine("\n>>> RESPUESTA RAW DE ODOO:");
            Console.WriteLine(result);
            Console.WriteLine("===============================\n");

            try
            {
                return JsonDocument.Parse(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Error parseando JSON:\n{ex.Message}\n\nRespuesta recibida:\n{result}",
                    "Odoo ERROR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                throw;
            }
        }

        // ============================================================
        //  LOGIN CORREGIDO (maneja result=false sin explotar)
        // ============================================================
        public async Task<bool> LoginAsync()
        {
            var payload = new
            {
                jsonrpc = "2.0",
                method = "call",
                @params = new
                {
                    service = "common",
                    method = "authenticate",
                    args = new object[]
                    {
                        _db,
                        _user,
                        _password,
                        new { }
                    }
                },
                id = 1
            };

            var resp = await CallAsync(payload);

            Console.WriteLine(">>> DEBUG LoginAsync:");
            Console.WriteLine(resp.RootElement.ToString());

            // === Caso 1: Odoo devuelve error ===
            if (resp.RootElement.TryGetProperty("error", out var error))
            {
                MessageBox.Show(
                    $"❌ Error en login Odoo:\n{error}",
                    "Odoo Login",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            // === Caso 2: Respuesta válida ===
            if (resp.RootElement.TryGetProperty("result", out var result))
            {
                // Si result = "false" ⇒ login incorrecto
                if (result.ValueKind == JsonValueKind.False)
                {
                    MessageBox.Show(
                        "❌ Odoo rechazó el login.\nRevisa DB, usuario y contraseña.",
                        "Odoo Login",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return false;
                }

                // Si result = número ⇒ login correcto
                if (result.ValueKind == JsonValueKind.Number)
                {
                    _uid = result.GetInt32();
                    Console.WriteLine($">>> UID obtenido: {_uid}");
                    return _uid > 0;
                }
            }

            // === Caso 3: algo raro ===
            MessageBox.Show(
                "❌ Respuesta inesperada de Odoo.\nNo se encontró 'result'.",
                "Odoo Login",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            return false;
        }

        // ============================================================
        //  CREAR CONTACTO (res.partner)
        // ============================================================
        public async Task<int> CrearPartnerAsync(object data)
        {
            var payload = new
            {
                jsonrpc = "2.0",
                method = "call",
                @params = new
                {
                    service = "object",
                    method = "execute_kw",
                    args = new object[]
                    {
                        _db,
                        _uid,
                        _password,
                        "res.partner",
                        "create",
                        new object[] { data }
                    }
                },
                id = 2
            };

            var resp = await CallAsync(payload);

            return resp.RootElement.GetProperty("result").GetInt32();
        }

        // ============================================================
        //  CREAR USUARIO DE ODOO (res.users)
        // ============================================================
        public async Task<int> CrearUsuarioOdooAsync(object data)
{
    var payload = new
    {
        jsonrpc = "2.0",
        method = "call",
        @params = new
        {
            service = "object",
            method = "execute_kw",
            args = new object[] 
            {
                _db,
                _uid,
                _password,
                "res.users",
                "create",
                new object[] { data }
            }
        },
        id = 3
    };

    var resp = await CallAsync(payload);

    // Agregar verificación para evitar error si "result" no existe
    if (resp.RootElement.TryGetProperty("result", out var result))
    {
        // Si el campo "result" está presente, obtener el valor
        return result.GetInt32();
    }
    else
    {
        // Log de la respuesta para depurar
        Console.WriteLine("Respuesta Odoo (sin 'result'):");
        Console.WriteLine(resp.RootElement.ToString());

        // Mostrar mensaje de error si no se encuentra el campo "result"
        MessageBox.Show("Error al sincronizar con Odoo. Respuesta inesperada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return -1; // O el valor adecuado según lo que esperes
    }
}

public async Task<bool> UsuarioExisteAsync(string username)
{
    var payload = new
    {
        jsonrpc = "2.0",
        method = "call",
        @params = new
        {
            service = "object",
            method = "execute_kw",
            args = new object[]
            {
                _db,
                _uid,
                _password,
                "res.users",
                "search",
                new object[]
                {
                    new object[]
                    {
                        new object[] { "login", "=", username }
                    }
                }
            }
        },
        id = 99
    };

    var resp = await CallAsync(payload);

    var result = resp.RootElement.GetProperty("result");

    return result.GetArrayLength() > 0;
}


    }
}
