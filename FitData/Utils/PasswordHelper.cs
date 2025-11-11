using System;
using System.Security.Cryptography;

namespace FitData.Utils
{
    public static class PasswordHelper
    {
        // Genera un hash que contiene versión(1 byte) + salt(16 bytes) + hash(32 bytes) -> total 49 bytes -> Base64
        public static string HashPassword(string password)
        {
            var salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(32);

            var result = new byte[1 + salt.Length + hash.Length];
            result[0] = 0; // versión
            Buffer.BlockCopy(salt, 0, result, 1, salt.Length);
            Buffer.BlockCopy(hash, 0, result, 1 + salt.Length, hash.Length);

            return Convert.ToBase64String(result);
        }

        public static bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var bytes = Convert.FromBase64String(hashedPassword);

            if (bytes.Length != 1 + 16 + 32) return false; // formato inesperado

            var salt = new byte[16];
            Buffer.BlockCopy(bytes, 1, salt, 0, 16);

            using var pbkdf2 = new Rfc2898DeriveBytes(providedPassword, salt, 100_000, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(32);

            for (int i = 0; i < 32; i++)
                if (bytes[1 + 16 + i] != hash[i]) return false;

            return true;
        }
    }
}

