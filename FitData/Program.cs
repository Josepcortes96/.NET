// See https://aka.ms/new-console-template for more information
using FitData.Datos;
using FitData.Entidades;

var db = new FitDataContext();

// Prueba de lectura
Console.WriteLine("Usuarios registrados en la base de datos:");
foreach (var u in db.Usuarios)
{
    Console.WriteLine($"{u.IdUsuario} - {u.Nombre} {u.Apellido} ({u.Rol})");
}

