using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Helper;

namespace WebService1
{
    public class KeyCreator
    {

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
    }
}
