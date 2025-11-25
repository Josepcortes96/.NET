using Xunit;
using FitData.Datos;
using System.Data;

public class DatabaseTest
{
    private const string ConnectionString =
        "Server=localhost,1433;Database=fitdata;User Id=sa;Password=C4mbiami!;TrustServerCertificate=True;";

    [Fact]
    public void TestConnection_ShouldOpenSuccessfully()
    {
    
        var db = new DatabaseConnection(ConnectionString);

        using var conn = db.GetOpenConnection();

        Assert.Equal(ConnectionState.Open, conn.State);
    }
}
