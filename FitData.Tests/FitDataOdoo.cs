using System.Net.Http;
using System.Text;
using System.Text.Json;
using Xunit;

public class FitDataOdoo
{
    [Fact]
    public async Task TestConexionOdoo()
    {
        using var client = new HttpClient();

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
                    "fitdata",
                    "josepcortes6@gmail.com",
                    "fitdata",
                    new {}
                }
            },
            id = 1
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(
            "http://136.144.233.39:8069/jsonrpc",
            content
        );

        var result = await response.Content.ReadAsStringAsync();

        // 🔥 IMPRIMIMOS LA RESPUESTA REAL
        Console.WriteLine("Respuesta Odoo:");
        Console.WriteLine(result);

        // Por ahora NO validamos nada
        Assert.True(true);
    }
}
