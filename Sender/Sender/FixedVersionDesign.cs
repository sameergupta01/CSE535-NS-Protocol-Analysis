using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helper;
using XMLContracts;
using ClientApplication.AuthenticationServer;
using ClientApplication.Receiver;

namespace ClientApplication
{
    class FixedVersionDesign
    {
        public static string GetNonceForAuthentication(string sender_userName, Helper.EncryptDecrypt.EncryptionType Encryption_Type)
        {
            ReceiveServiceSoapClient instance = new ReceiveServiceSoapClient();
            string res = instance.GetNonceForAuthentication(sender_userName, (ClientApplication.Receiver.EncryptionType)Encryption_Type);
            return res;    
        }    

        public static string requestSessionKey(string secretKey, string sender_userName, string receiver_userName, string sender_nonce,string sender_nonce_and_username, EncryptDecrypt.EncryptionType Encryption_Type)
        {
            //string s = XMLGenerator.getNodeValue(sender_nonce_and_username, "/ReceiverResponseXMLFixed/sender_userName");
            //string sa = XMLGenerator.getNodeValue(sender_nonce_and_username, "/ReceiverResponseXMLFixed/receiverNonce");

            AuthenticationServerSoapClient serverInstance = new AuthenticationServerSoapClient();
            string res = serverInstance.getAuthenticationTokenFixedVer(sender_userName, receiver_userName, sender_nonce,sender_nonce_and_username, (ClientApplication.AuthenticationServer.EncryptionType)Encryption_Type);
            return res;

        }

        public static string requestAuthentication(string secretKey, string sessionKey, string enc_sessionKey, string enc_sender_userName,string enc_r_nonce, Helper.EncryptDecrypt.EncryptionType Encryption_Type)
        {
            ReceiveServiceSoapClient instance = new ReceiveServiceSoapClient();

            authTokenFixed authToken = new authTokenFixed();

            authToken.sender_userName = EncryptDecrypt.decryptMessage(enc_sender_userName, secretKey, Encryption_Type);
            authToken.sessionKey = EncryptDecrypt.decryptMessage(enc_sessionKey, secretKey, Encryption_Type);
            authToken.receiver_nonce = EncryptDecrypt.decryptMessage(enc_r_nonce, secretKey, Encryption_Type);
            string resXML = XMLGenerator.getXMLForAuthToken(authToken);


            //string authenticationToken_d = EncryptDecrypt.decryptMessage(resXML, secretKey, EncryptDecrypt.EncryptionType.AES);
            string enc_nonce_XML = instance.authenticateSenderFixed(resXML, (ClientApplication.Receiver.EncryptionType)Encryption_Type);
            string enc_nonce = XMLGenerator.getNodeValue(enc_nonce_XML, "/ReceiverResponseXML/receiverNonce");
            //string receiver_nonce = EncryptDecrypt.decryptMessage(enc_nonce, sessionKey, Encryption_Type);
            return enc_nonce;
        }

        public static string ackVerificationAndAlive(string sessionKey, string enc_receiver_nonce, Helper.EncryptDecrypt.EncryptionType Encryption_Type)
        {
            ReceiveServiceSoapClient instance = new ReceiveServiceSoapClient();            
            string res = instance.VerificationAndAliveFixed(enc_receiver_nonce, (ClientApplication.Receiver.EncryptionType)Encryption_Type);
            if (res == "true")
                return "Success";
            else
                return "Failure";
        }


    
    }
}
