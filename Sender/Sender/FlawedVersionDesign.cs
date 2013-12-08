using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helper;
using XMLContracts;
using ClientApplication.AuthenticationServer;
using ClientApplication.Receiver;

namespace ClientAppliaction
{
    class FlawedVersionDesign
    {
        public static string requestSessionKey(string secretKey, string sender_userName, string receiver_userName, string sender_nonce, EncryptDecrypt.EncryptionType Encryption_Type)
        {
            AuthenticationServerSoapClient serverInstance = new AuthenticationServerSoapClient();
            string res = serverInstance.getAuthenticationToken(sender_userName, receiver_userName, sender_nonce, (ClientApplication.AuthenticationServer.EncryptionType)Encryption_Type);
            return res;

        }

        public static string requestAuthentication(string secretKey, string sessionKey, string enc_sessionKey, string enc_sender_userName, Helper.EncryptDecrypt.EncryptionType Encryption_Type)
            {
                ReceiveServiceSoapClient instance = new ReceiveServiceSoapClient();         

                authToken authToken = new authToken();

                authToken.sender_userName = EncryptDecrypt.decryptMessage(enc_sender_userName, secretKey, Encryption_Type);
                authToken.sessionKey = EncryptDecrypt.decryptMessage(enc_sessionKey, secretKey, Encryption_Type);
                string resXML = XMLGenerator.getXMLForAuthToken(authToken);


                //string authenticationToken_d = EncryptDecrypt.decryptMessage(resXML, secretKey, EncryptDecrypt.EncryptionType.AES);
                string enc_nonce_XML = instance.authenticateSender(resXML, (ClientApplication.Receiver.EncryptionType)Encryption_Type);
                string enc_r_nonce = XMLGenerator.getNodeValue(enc_nonce_XML, "/ReceiverResponseXML/receiverNonce");
                return enc_r_nonce;
            }

        public static string ackVerificationAndAlive(string sessionKey, string enc_receiver_nonce, Helper.EncryptDecrypt.EncryptionType Encryption_Type)
            {
                ReceiveServiceSoapClient instance = new ReceiveServiceSoapClient();                                         
                string res = instance.VerificationAndAlive(enc_receiver_nonce, (ClientApplication.Receiver.EncryptionType)Encryption_Type);
                if (res == "true")
                    return "Success";
                else
                    return "Failure";
            }


    }
}
