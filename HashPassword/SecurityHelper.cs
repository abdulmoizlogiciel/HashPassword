using System;
using System.Security.Cryptography;

namespace HashPassword
{
    public class SecurityHelper
    {
        private const int nSalt = 8;
        private const int nHash = 32;

        public static string GenerateSalt()
        {
            var saltBytes = new byte[nSalt];

            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetNonZeroBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes);
        }

        public static string HashPassword(string password, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);

            using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(nHash));
        }
    }
}
