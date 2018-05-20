using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace PasswordBox.Common
{
    class Crypto
    {
        #region 对称加密，用于item加密

        // Love's Labour's Lost
        private static string secret = "honorificabilitudinitatibus";

        private static SymmetricKeyAlgorithmProvider provider =
            SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.DesCbcPkcs7);

        private static CryptographicKey Key => 
            provider.CreateSymmetricKey(CryptographicBuffer.ConvertStringToBinary(secret, BinaryStringEncoding.Utf8));

        public static string Encrypt(string data)
        {
            string encryptedData = null;
            IBuffer dataBytes = CryptographicBuffer.ConvertStringToBinary(data, BinaryStringEncoding.Utf8);

            IBuffer cryptBuffer = CryptographicEngine.Encrypt(Key, dataBytes, null);
            byte[] encryptedBytes = cryptBuffer.ToArray();

            encryptedData = Convert.ToBase64String(encryptedBytes);
            return encryptedData;
        }

        public static string Decrypt(string data)
        {
            string decryptedData = null;
            byte[] dataBytes = Convert.FromBase64String(data);
            IBuffer dataBuffer = dataBytes.AsBuffer();

            IBuffer decryptedBuffer = CryptographicEngine.Decrypt(Key, dataBuffer, null);

            decryptedData = CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, decryptedBuffer);
            return decryptedData;
        }

        #endregion

        #region 单向加密，用于App密码

        public static string Hash(string data)
        {
            HashAlgorithmProvider hashProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer dataBuffer = CryptographicBuffer.ConvertStringToBinary(data, BinaryStringEncoding.Utf8);
            IBuffer res = hashProvider.HashData(dataBuffer);
            return CryptographicBuffer.EncodeToHexString(res);
        }

        public static bool TestEqual(string expected, string actual)
        {
            return Hash(actual) == expected;
        }

        #endregion
    }
}
