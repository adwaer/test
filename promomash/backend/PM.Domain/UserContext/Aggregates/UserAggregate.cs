using System;
using System.Security.Cryptography;
using CSharpFunctionalExtensions;
using PM.Models;
using SilentNotary.Cqrs.Domain;

namespace PM.Domain.UserContext.Aggregates
{
    public class UserAggregate : Aggregate<string>
    {
        private const int PasswordSaltLength = 8;

        public readonly Customer Customer;

        public UserAggregate(Customer customer)
        {
            Customer = customer;
        }

        public Result<UserAggregate> GenerateSalt()
        {
            Customer.Salt = Convert.ToBase64String(GenerateBytes(PasswordSaltLength));
            return Result.Ok(this);
        }
        
        #region private

        /// <summary>
        /// Generate salt
        /// </summary>
        /// <param name="length">Salt length</param>
        /// <returns></returns>
        private byte[] GenerateBytes(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[length];
                rng.GetNonZeroBytes(bytes);
                return bytes;
            }
        }

        #endregion
    }
}