using System;
using System.Text;

namespace Helper
{
    public class TextHandler
    {
        public static byte[] GetBytes(string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }


        public static string GetBytes(string input, int requiredLength)
        {
            byte[] inputByteData = Encoding.UTF8.GetBytes(input);

            if (inputByteData.Length < requiredLength)
            {
                string message = string.Format("Key size is invalid.Please increase the password size.");

                throw new ApplicationException(message);
        
            }
                var requiredBytes = new byte[requiredLength];
                Array.Copy(inputByteData, requiredBytes, requiredLength);

                return Helper.TextHandler.GetBase64EncodedString(requiredBytes);
        
        
        }

        public static byte[] GetBytesData(string input, int requiredLength)
        {
            byte[] inputByteData = Encoding.UTF8.GetBytes(input);

            if (inputByteData.Length < requiredLength)
            {
                string message = string.Format("Key size is invalid.Please increase the password size.");

                throw new ApplicationException(message);

            }
            var requiredBytes = new byte[requiredLength];
            Array.Copy(inputByteData, requiredBytes, requiredLength);

            return requiredBytes;


        }




        public static string GetBase64EncodedString(byte[] inputBytes)
        {
            return Convert.ToBase64String(inputBytes);// Encoding.UTF8.GetString(inputBytes);
        }

        public static byte[] GetBytesFromBase64String(string inputString)
        {
            return Convert.FromBase64String(inputString); //Encoding.UTF8.GetBytes(inputString);
        }
    }
}