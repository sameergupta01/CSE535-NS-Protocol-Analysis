using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Helper
{
    public class EncryptDecrypt
    {        
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";


        public enum EncryptionType
        {
            AES,    //0
            DES,    //1
            OpenSSL //2
        }

        public static string getNodeSecretKey(string password, Helper.EncryptDecrypt.EncryptionType EncryptionType)
        {
            try
            {
                string key = password;
                string secretKey = string.Empty;
                if (!key.Equals(string.Empty))
                {
                    if (EncryptionType == Helper.EncryptDecrypt.EncryptionType.AES)
                    {
                        secretKey = Helper.TextHandler.GetBytes(key, 256 / 8); //max key size allowed is 256 for AES
                    }
                    else
                    {
                        secretKey = Helper.TextHandler.GetBytes(key, 64 / 8);   //64 for DES
                    }
                }
                return secretKey;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("UserName/Password Incorrect.");
            }
        }

        public static string encrptMessage(string request, string secretKey, EncryptionType encType)
        {
            try
            {

                // Encrypt the string to an array of bytes. 
                string encrypted = string.Empty;

                switch (encType)
                {
                    case EncryptionType.AES: //for AES
                        encrypted = Encrypt_AES(request, secretKey);                        
                        break;
                    case EncryptionType.DES: //for DES
                        encrypted = Encrypt_DES(request, secretKey);
                        break;
                }
                return encrypted;
            }
            catch (Exception e)
            {

            }
            return string.Empty;
        }

        public static string decryptMessage(string request, string Key, EncryptionType encType)
        {
            try
            {

                // Encrypt the string to an array of bytes. 

                string decrypted = string.Empty;
                switch (encType)
                {
                    case EncryptionType.AES: //for AES
                        decrypted = Decrypt_AES(request, Key);
                        break;
                    case EncryptionType.DES: //for DES
                        decrypted = Decrypt_DES(request, Key);
                        break;
                }
                return decrypted;
            }
            catch (Exception e)
            {

            }
            return string.Empty;
        }

        public static string Decrypt_AES(string encryptedText, string PasswordHash)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        public static string Encrypt_AES(string plainText, string PasswordHash)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Encrypt_DES(string originalString, string PasswordHash)
        {
            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException
                       ("The string which needs to be encrypted can not be null.");
            }
            byte[] bytes = Helper.TextHandler.GetBytesData(PasswordHash, 8);
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        public static string Decrypt_DES(string cryptedString, string PasswordHash)
        {
            if (String.IsNullOrEmpty(cryptedString))
            {
                throw new ArgumentNullException
                   ("The string which needs to be decrypted can not be null.");
            }
            byte[] bytes = Helper.TextHandler.GetBytesData(PasswordHash, 8);
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream
                    (Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }

    }
}
