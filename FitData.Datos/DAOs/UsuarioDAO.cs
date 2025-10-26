using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using FitData.Entidades;

namespace FitData.Datos.DAOs
{
    public class UsuarioDAO
    {
        private readonly DatabaseConnection _db;

        public UsuarioDAO(DatabaseConnection db)
        {
            _db = db;
        }

        public List<Usuario> GetAll()
        {
            var usuarios = new List<Usuario>();

            using (var conn = _db.GetOpenConnection())
            {
                var cmd = new SqlCommand("SELECT idUsuario, nombre, apellido, nif, rol, Username, Password FROM Usuario", conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    usuarios.Add(new Usuario
                    {
                        IdUsuario = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Nif = reader.GetString(3),
                        Rol = reader.GetString(4)
                        Username = reader.GetString(5)
                        Password = reader.GetString(6)
                    });
                }
            }

            return usuarios;
        }

        public void Insert(Usuario u)
        {
            using (var conn = _db.GetOpenConnection())
            {
                var cmd = new SqlCommand("INSERT INTO Usuario (nombre, apellido, nif, rol, Username, Password) VALUES (@n, @a, @nif, @rol, @username, @password)", conn);
                cmd.Parameters.AddWithValue("@n", u.Nombre);
                cmd.Parameters.AddWithValue("@a", u.Apellido);
                cmd.Parameters.AddWithValue("@nif", u.Nif);
                cmd.Parameters.AddWithValue("@rol", u.Rol);
                cmd.Parameters.AddWithValue("@Username", u.Username);
                cmd.Parameters.AddWithValue("@Password", u.Password);
                cmd.ExecuteNonQuery();
            }
        }
    }
}