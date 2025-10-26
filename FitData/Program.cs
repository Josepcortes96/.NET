using FitData.Datos;
using FitData.Datos.Repositorios;
using FitData.Entidades;

var context = new FitDataContext();
var repo = new UsuarioRepository(context);

// CREATE
var nuevo = new Usuario
{
    Nombre = "David",
    Apellido = "Beaerhausen",
    Nif = "87654321B",
    Rol = "Cliente",
    Username = "dmendez",
    Password = "1234"
};
repo.Add(nuevo);
Console.WriteLine("Usuario añadido correctamente.");

// READ
Console.WriteLine("Usuarios actuales:");
foreach (var u in repo.GetAll())
    Console.WriteLine($"{u.IdUsuario} - {u.Nombre} {u.Apellido} ({u.Rol})");

// UPDATE
var primero = repo.GetAll().FirstOrDefault();
if (primero != null)
{
    primero.Rol = "Administrador";
    repo.Update(primero);
    Console.WriteLine($"Usuario {primero.Nombre} actualizado correctamente.");
}

// DELETE
var ultimo = repo.GetAll().LastOrDefault();
if (ultimo != null)
{
    repo.Delete(ultimo.IdUsuario);
    Console.WriteLine($"Usuario {ultimo.Nombre} eliminado correctamente.");
}

