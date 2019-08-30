using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using PM.Models;

namespace PM.Identity.Dal
{
    public class PasswordHasherWithSalt : IPasswordHasher<Customer>
    {
        public string HashPassword(Customer user, string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            if (user.Salt == null)
                throw new ArgumentNullException(nameof(user.Salt));

            var encoding = Encoding.UTF8;
            using (var sha = new SHA256CryptoServiceProvider())
            {
                var hash = sha.ComputeHash(sha.ComputeHash(encoding.GetBytes(password))
                    .Concat(Convert.FromBase64String(user.Salt))
                    .ToArray());
                return Convert.ToBase64String(hash);
            }
        }

        public PasswordVerificationResult VerifyHashedPassword(Customer user, string hashedPassword, string providedPassword)
        {
            if (providedPassword == null || user.Salt == null)
                return PasswordVerificationResult.Failed;

            return hashedPassword != null && HashPassword(user, providedPassword).SequenceEqual(hashedPassword)
                ? PasswordVerificationResult.Success
                : PasswordVerificationResult.Failed;
        }
    }
}