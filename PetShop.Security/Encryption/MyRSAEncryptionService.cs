using System.Security.Cryptography;
using System.Text;

namespace PetShop.Security.Encryption
{
    public class MyRSAEncryptionService
    {
        private RSACryptoServiceProvider _rsa;
        private bool _hasPublicKey;
        private bool _hasPrivateKey;

        public MyRSAEncryptionService()
        {
            _rsa = new RSACryptoServiceProvider();
            _rsa.KeySize = 1024;
            _rsa.PersistKeyInCsp = true;
            _hasPublicKey = true;
            _hasPrivateKey = !_rsa.PublicOnly;
        }

        public MyRSAEncryptionService(string rsaParams)
        {
            _rsa = new RSACryptoServiceProvider();
            _rsa.FromXmlString(rsaParams);
            _hasPublicKey = true;
            _hasPrivateKey = !_rsa.PublicOnly;
        }

        public MyRSAEncryptionService(CspParameters cspParameters)
        {
            _rsa = new RSACryptoServiceProvider(cspParameters);
            _rsa.PersistKeyInCsp = true;
            _hasPublicKey = true;
            _hasPrivateKey = !_rsa.PublicOnly;
        }

        public byte[] encryptMessage(string message)
        {
            byte[] clearText = Encoding.UTF8.GetBytes(message);
            byte[] encryptedBytes = _rsa.Encrypt(clearText, RSAEncryptionPadding.Pkcs1);
            return encryptedBytes;
        }

        public string decryptMessage(byte[] message)
        {
            byte[] clearBytes = _rsa.Decrypt(message, RSAEncryptionPadding.Pkcs1);
            string clearText = Encoding.UTF8.GetString(clearBytes);
            return clearText;
        }

        public string GetPublicRsaParameters()
        {
            return _rsa.ToXmlString(false);
        }

        public string GetPublicAndPrivateRsaParameters()
        {
            return _rsa.ToXmlString(true);
        }


    }
}