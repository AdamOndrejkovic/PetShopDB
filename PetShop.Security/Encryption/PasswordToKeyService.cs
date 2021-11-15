using System;
using System.Security.Cryptography;

namespace PetShop.Security.Encryption
{
    public class PasswordToKeyService
    {
        public byte[] GetKey(string password, out byte[] salt, int keyLength)
        {
            if (keyLength % 8 != 0)
            {
                throw new ArgumentException("Key length must be a multiple of 8");
            }

            using (var derivedBytes = new Rfc2898DeriveBytes(password, keyLength))
            {
                salt = derivedBytes.Salt;
                return derivedBytes.GetBytes(keyLength / 8);
            }
        }

        public byte[] GetKey(string password, byte[] salt, int keyLength)
        {
            if (keyLength % 8 != 0)
            {
                throw new ArgumentException("Key length must be a multiple of 8");
            }

            using (var derivedBytes = new Rfc2898DeriveBytes(password, salt))
            {
                return derivedBytes.GetBytes(keyLength / 8);
            }
            
        }
    }
}