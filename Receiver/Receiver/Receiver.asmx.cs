using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Helper;
using XMLContracts;

namespace Receiver
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ReceiveService : System.Web.Services.WebService
    {
        //private static Dictionary<string, string> session_keys = new Dictionary<string,string>();
        private static string session_key = string.Empty;
        private static string receiver_nonce = string.Empty;
        private static string receiver_nonce_dash = string.Empty;
        private static string password = "this is sbu password...increasse the size";
        private static string secretKey = string.Empty;

        [WebMethod]
        //for flawed version
        public string authenticateSender(string authenticationTokenXML,Helper.EncryptDecrypt.EncryptionType enc_type)
        {
            secretKey = EncryptDecrypt.getNodeSecretKey(password, enc_type);
            string enc_sender_userName = Helper.XMLGenerator.getNodeValue(authenticationTokenXML, "/authToken/sender_userName");
            string sender_userName = EncryptDecrypt.decryptMessage(enc_sender_userName, secretKey, enc_type);
            string enc_session_key = Helper.XMLGenerator.getNodeValue(authenticationTokenXML, "/authToken/sessionKey");
            session_key = EncryptDecrypt.decryptMessage(enc_session_key, secretKey, enc_type);
            Random rnd = new Random();
            receiver_nonce = rnd.Next(0, Int32.MaxValue).ToString();
            ReceiverResponseXML response = new ReceiverResponseXML();
            response.receiverNonce = EncryptDecrypt.encrptMessage(receiver_nonce, session_key, enc_type);
            string resXML = XMLGenerator.getXMLForReceiver(response);
            return resXML;
            
        }
        [WebMethod]
        //for flawed version
        public string VerificationAndAlive(string dataXML, Helper.EncryptDecrypt.EncryptionType enc_type)
        {
            //string enc_sender_userName = Helper.XMLGenerator.getNodeValue(dataXML, "/AuthenticationToken/sender_userName");
            //string enc_receiver_nonce = Helper.XMLGenerator.getNodeValue(dataXML, "/AuthenticationToken/receiver_nonce");
            //string sender_userName = EncryptDecrypt.decryptMessage(enc_sender_userName, sessionKey, EncryptDecrypt.EncryptionType.AES);
            string r_nonce = EncryptDecrypt.decryptMessage(dataXML, session_key, enc_type);
            if (Convert.ToInt32(r_nonce) == Convert.ToInt32(receiver_nonce)-1)
                return "true";
            else
                return "false";            
        }

        [WebMethod]
        //for fixed version
        public string GetNonceForAuthentication(string sender_userName, Helper.EncryptDecrypt.EncryptionType enc_type)
        {
            secretKey = EncryptDecrypt.getNodeSecretKey(password, enc_type);
            Random rnd = new Random();
            receiver_nonce_dash = rnd.Next(0, Int32.MaxValue).ToString();
            ReceiverResponseXMLFixed response = new ReceiverResponseXMLFixed();
            response.sender_userName = EncryptDecrypt.encrptMessage(sender_userName, secretKey, enc_type);
            response.receiverNonce = EncryptDecrypt.encrptMessage(receiver_nonce_dash, secretKey, enc_type);
            string resXML = XMLGenerator.getXMLForReceiver(response);
            return resXML;
        }


        [WebMethod]
        //for fixed version
        public string authenticateSenderFixed(string authenticationTokenXML, Helper.EncryptDecrypt.EncryptionType enc_type)
        {
            secretKey = EncryptDecrypt.getNodeSecretKey(password, enc_type);
            string enc_sender_userName = Helper.XMLGenerator.getNodeValue(authenticationTokenXML, "/authTokenFixed/sender_userName");
            string sender_userName = EncryptDecrypt.decryptMessage(enc_sender_userName, secretKey, enc_type);
            string enc_session_key = Helper.XMLGenerator.getNodeValue(authenticationTokenXML, "/authTokenFixed/sessionKey");
            session_key = EncryptDecrypt.decryptMessage(enc_session_key, secretKey, enc_type);
            string enc_r_nonce = Helper.XMLGenerator.getNodeValue(authenticationTokenXML, "/authTokenFixed/receiver_nonce");
            string r_nonce = EncryptDecrypt.decryptMessage(enc_r_nonce, secretKey, enc_type);
            if (r_nonce != receiver_nonce_dash)
                return "";
            Random rnd = new Random();
            receiver_nonce = rnd.Next(0, Int32.MaxValue).ToString();
            ReceiverResponseXML response = new ReceiverResponseXML();
            response.receiverNonce = EncryptDecrypt.encrptMessage(receiver_nonce, session_key, enc_type);
            string resXML = XMLGenerator.getXMLForReceiver(response);
            return resXML;

        }
        [WebMethod]
        //for fixed version
        public string VerificationAndAliveFixed(string dataXML, Helper.EncryptDecrypt.EncryptionType enc_type)
        {
            //string enc_sender_userName = Helper.XMLGenerator.getNodeValue(dataXML, "/AuthenticationToken/sender_userName");
            //string enc_receiver_nonce = Helper.XMLGenerator.getNodeValue(dataXML, "/AuthenticationToken/receiver_nonce");
            //string sender_userName = EncryptDecrypt.decryptMessage(enc_sender_userName, sessionKey, EncryptDecrypt.EncryptionType.AES);
            string r_nonce = EncryptDecrypt.decryptMessage(dataXML, session_key, enc_type);
            if (Convert.ToInt32(r_nonce) == Convert.ToInt32(receiver_nonce) - 1)
                return "true";
            else
                return "false";
        }


    }
}
