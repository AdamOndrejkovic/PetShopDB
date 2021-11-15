using System;
using PetShop.Core.Helpers;
using PetShop.Security.Encryption;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        private static AuthenticationHelper _helper;
        
        static void Main(string[] args)
        {
            FunWithAESandKeys();
            WorkingBackwards();
            PlayingWithMyRSAEncryptionService();
            playingWithMyAESService();
            HashPasswordExample("P@$$WORD");
            funWithTokens();
            funWithEncryption("Hi Bob, it's Alice (h)");
            FunWithAsyncEncryption();
        }
        

        private static void FunWithAESandKeys()
        {
            PasswordToKeyService keyService = new PasswordToKeyService();

            String password = "Password123";
            byte[] salt = CreateRandomSalt(5);
            byte[] key = keyService.GetKey(password, out salt, 256);

            Console.WriteLine("Key: " + Convert.ToBase64String(key));
            Console.WriteLine("Salt: " + Convert.ToBase64String(salt));

            byte[] iv;
            MyAESEncryptionService aesEncryptionService = new MyAESEncryptionService(key, out iv);
            Console.WriteLine("IV: " + Convert.ToBase64String(iv));

            String secretText = "This has to work!";
            byte[] result = aesEncryptionService.EncryptMessage(secretText);

            Console.WriteLine("Encrypted text: " + Convert.ToBase64String(result));

            String decrypted = aesEncryptionService.DecryptMessage(result);
            Console.WriteLine("Original: " + decrypted);
        }
        
        private static void WorkingBackwards()
        {
            string password = "Password123";
            string salt = "8Eye+X/wPcdTUOhOr2hCbKfPeLnVGAJNzNZKMoLYLD2pGIC4BfADlNMkh0B41JqQG3LTYxIgzfLwGYOjN1+Wkg7Bpm4uwx2eBw7aFUQ8vpx1rQp40FTLwsCTbq2XOmGhgcTl+5lVhkHQrweEYsrzfn5lVY7bOEQcjc480iq1a+BLUn5Mwji2XSwql7FlAxGWzkGXVl9Gq0eREIcSOggzZAe5dK09nUmfAZJ5Wq5h9jOItk3v7l5YE0jEDZByn4flzF0Ub8cYRpso3dG2UeT+/zfBddhkYv2Fx/ZTohAWP1i7jGDYFrctjpAJBHxfd6y8azdrfJ+kHvZ7eLSU1LHz0Q==";
            byte[] key = new PasswordToKeyService().GetKey(password, Convert.FromBase64String(salt), 256);

            string iv = "wfLfqUKDlZGQ90Rhl7zv/w==";
            MyAESEncryptionService encryptionService = new MyAESEncryptionService(key, Convert.FromBase64String(iv));

            string encryptedText = "wGMoD04DsIFA9fWzRmIDihNeEx4qy6UacEG/+KCcub0=";
            byte[] byteEncryptedText = Convert.FromBase64String(encryptedText);
            string clearText = encryptionService.DecryptMessage(byteEncryptedText);
            Console.WriteLine("The message is: " + clearText);
        }

        private static void PlayingWithMyRSAEncryptionService()
        {
            MyRSAEncryptionService encryptionServiceAlice = new MyRSAEncryptionService();

            string alicesPublicKey= encryptionServiceAlice.GetPublicRsaParameters();
            Console.WriteLine("Alice public key: " + alicesPublicKey);

            string alicesPublicAndPrivateKey = encryptionServiceAlice.GetPublicAndPrivateRsaParameters();
            Console.WriteLine("Alice public and private key: " + alicesPublicAndPrivateKey);
            
            MyRSAEncryptionService encryptionServiceBob = new MyRSAEncryptionService(alicesPublicKey);

            string bobMessageToAlice = "Hi Alice. I think you are really nice";

            byte[] secret = encryptionServiceBob.encryptMessage(bobMessageToAlice);

            string alicesMessageFromBob = encryptionServiceAlice.decryptMessage(secret);

            Console.WriteLine(alicesMessageFromBob);
        }

        private static void playingWithMyAESService()
        {
            string aKey = "glZXcwfK2eYmfb8drr1ObHn5hXUvl2kXBrOmbvxf8Ow=";
            string aIv = "tx8FgtXX8jCYKQDxBICUlw==";
            MyAESEncryptionService encryptionService = new MyAESEncryptionService(aKey, aIv);

            String message = "This is a new secret text";
            byte[] secret = encryptionService.EncryptMessage(message);

            string base64secret = Convert.ToBase64String(secret);

            Console.WriteLine(base64secret);
            
            string aSecret = "8XBmpevEDM5fTwvk+zoi+g==";
            byte[] byteSecret = Convert.FromBase64String(aSecret);

            string messageTwo = encryptionService.DecryptMessage(byteSecret);
            Console.WriteLine(messageTwo);
        }

        private static void HashPasswordExample(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                Console.WriteLine(hash);
            }
        }

        private static void funWithTokens()
        {
            Byte[] secretBytes = new byte[40];
            Random random = new Random();
            random.NextBytes(secretBytes);
            _helper = new AuthenticationHelper(secretBytes);

            string password = "Pa$$w0rd";
            byte[] passwordHash;
            byte[] salt;
            _helper.CreatePasswordHash(password, out passwordHash, out salt);

            string pwHash = BitConverter.ToString(passwordHash);
            string pwSalt = BitConverter.ToString(salt);

            Console.WriteLine("Hash: " + pwHash);
            Console.WriteLine("Salt: " + pwSalt);

            if (_helper.VerifyPasswordHash(password, passwordHash, salt))
            {
                Console.WriteLine("We have a match");
            }
            else
            {
                Console.WriteLine("It doesn't match");
            }
        }

        private static void funWithEncryption(string message)
        {
            byte[] pwd = Encoding.Unicode.GetBytes("p@$$w0rd");
            byte[] salt = CreateRandomSalt(512 / 8);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            try
            {
                Console.WriteLine("Creating a key with PasswordDeriveBytes");

                PasswordDeriveBytes pdb = new PasswordDeriveBytes(pwd, salt);

                tdes.Key = pdb.CryptDeriveKey("TripleDES", "SHA512", 0, tdes.IV);
                Console.WriteLine("Operation Complete.");
                byte[] encrypted;

                ICryptoTransform encryptor = tdes.CreateEncryptor();

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(message);
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }

                string encryptedText = Convert.ToBase64String(encrypted);
                Console.WriteLine("Encrypted: " + encryptedText);

                string clearText;
                ICryptoTransform decrypter = tdes.CreateDecryptor();
                using (MemoryStream msDecrypt = new MemoryStream(encrypted))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decrypter, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            clearText = srDecrypt.ReadToEnd();
                        }
                    }
                }

                Console.WriteLine("Decrypted " + clearText);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                ClearBytes(pwd);
                ClearBytes(salt);
                
                tdes.Clear();
            }

            Console.ReadLine();
        }
        private static void FunWithAsyncEncryption(){}

        private static byte[] CreateRandomSalt(int length)
        {
            byte[] randBytes;

            if (length >= 1)
            {
                randBytes = new byte[length];
            }
            else
            {
                randBytes = new byte[1];
            }

            RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
            
            rand.GetBytes(randBytes);
            
            return randBytes;
        }

        public static void ClearBytes(byte[] buffer)
        {
            
        }
    }
}