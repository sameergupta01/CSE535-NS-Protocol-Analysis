using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Helper;
using System.Security.Cryptography;
using System.Resources;

namespace NS_AuthenticationServer
{
    public class KeyCreator
    {   
        private static string GetPassword(string userName)
        {

            string result =  PasswordFile.ResourceManager.GetString(userName).ToString();
            return result;
        }

        public static string getNodeSecretKey(string userName, EncryptDecrypt.EncryptionType Encryption_Type)
        {
            try
            {
                string key = GetPassword(userName);
                string secretKey = string.Empty;
                if (!key.Equals(string.Empty))
                {
                    if (Encryption_Type == Helper.EncryptDecrypt.EncryptionType.AES)
                    {
                        secretKey = Helper.TextHandler.GetBytes(key, 256 / 8); //max key size allowed is 256 for AES
                    }
                    else
                    {
                        secretKey = Helper.TextHandler.GetBytes(key, 8);   //8 for DES
                    }
                }
                return secretKey;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("UserName/Password Incorrect.");
            }
        }

        public static string generateSessionKey(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return TextHandler.GetBase64EncodedString(buff);
        }
    }
}
