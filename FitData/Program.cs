using FitData.Datos;
using FitData.Datos.Repositorios;
using FitData.Entidades;

var context = new FitDataContext();
var repo = new UsuarioRepository(context);

bool salir = false;

while (!salir)
{
    Console.Clear();
    Console.WriteLine("=== MENÚ GENTEFIT ===");
    Console.WriteLine("1. Añadir usuario");
    Console.WriteLine("2. Mostrar usuarios");
    Console.WriteLine("3. Actualizar usuario");
    Console.WriteLine("4. Eliminar usuario");
    Console.WriteLine("5. Salir");
    Console.Write("\nElige una opción: ");
    var opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine() ?? "";

            Console.Write("Apellido: ");
            string apellido = Console.ReadLine() ?? "";

            Console.Write("NIF: ");
            string nif = Console.ReadLine() ?? "";

            Console.Write("Rol (Cliente, Administrador, etc.): ");
            string rol = Console.ReadLine() ?? "";

            Console.Write("Username: ");
            string username = Console.ReadLine() ?? "";

            Console.Write("Password: ");
            string password = Console.ReadLine() ?? "";

            var nuevo = new Usuario
            {
                Nombre = nombre,
                Apellido = apellido,
                Nif = nif,
                Rol = rol,
                Username = username,
                Password = password
            };

            repo.Add(nuevo);
            Console.WriteLine("✅ Usuario añadido correctamente.");
            break;

        case "2":
            Console.WriteLine("\nUsuarios actuales:");
            foreach (var u in repo.GetAll())
                Console.WriteLine($"{u.IdUsuario} - {u.Nombre} {u.Apellido} ({u.Rol})");
            break;

        case "3":
            Console.Write("ID del usuario a actualizar: ");
            int idUp = int.Parse(Console.ReadLine() ?? "0");
            var usuarioUp = repo.GetById(idUp);
            if (usuarioUp != null)
            {
                Console.Write("Nuevo rol: ");
                usuarioUp.Rol = Console.ReadLine() ?? usuarioUp.Rol;
                repo.Update(usuarioUp);
                Console.WriteLine("✅ Usuario actualizado correctamente.");
            }
            else Console.WriteLine("⚠️ Usuario no encontrado.");
            break;

        case "4":
            Console.Write("ID del usuario a eliminar: ");
            int idDel = int.Parse(Console.ReadLine() ?? "0");
            repo.Delete(idDel);
            Console.WriteLine("✅ Usuario eliminado correctamente.");
            break;

        case "5":
            salir = true;
            break;

        default:
            Console.WriteLine("⚠️ Opción no válida.");
            break;
    }

    Console.WriteLine("\nPresiona una tecla para continuar...");
    Console.ReadKey();
}
