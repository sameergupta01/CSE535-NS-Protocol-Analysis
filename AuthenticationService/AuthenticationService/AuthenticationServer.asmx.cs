using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Helper;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using XMLContracts;

namespace NS_AuthenticationServer
{
    /// <summary>
    /// Copyright Sameer Gupta 2013
    /// Implementing NS Protocol Authentication Server as a webservice module
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[System.ComponentModel.ToolboxItem(false)]
    public class AuthenticationServer : System.Web.Services.WebService
    {

        [WebMethod]
        //function will authenticate the client and send back a token as a response.
        public string getAuthenticationToken(string sender_userName, string receiver_userName, string sender_nonce, Helper.EncryptDecrypt.EncryptionType Encryption_Type)
        {
            try
            {
                string sender_secretKey = KeyCreator.getNodeSecretKey(sender_userName, Encryption_Type);
                AuthenticationServerResponseXML res = new AuthenticationServerResponseXML();
                res.sender_userName = EncryptDecrypt.encrptMessage(sender_userName, sender_secretKey, Encryption_Type);
                res.receiver_userName = EncryptDecrypt.encrptMessage(receiver_userName, sender_secretKey, Encryption_Type);
                res.senderNonce = EncryptDecrypt.encrptMessage(sender_nonce, sender_secretKey, Encryption_Type);
                string session_key = KeyCreator.generateSessionKey(256 / 8);
                res.sessionKey = EncryptDecrypt.encrptMessage(session_key, sender_secretKey, Encryption_Type);
                res.AuthenticationToken = new authToken();
                string receiver_key = KeyCreator.getNodeSecretKey(receiver_userName, Encryption_Type);
                string enc_userName = EncryptDecrypt.encrptMessage(sender_userName, receiver_key, Encryption_Type);
                res.AuthenticationToken.sender_userName = EncryptDecrypt.encrptMessage(enc_userName, sender_secretKey, Encryption_Type);
                //string dec = EncryptDecrypt.decryptMessage(res.AuthenticationToken.sender_userName, receiver_key, EncryptDecrypt.EncryptionType.AES);
                string enc_sessionKey = EncryptDecrypt.encrptMessage(session_key, receiver_key, Encryption_Type);
                res.AuthenticationToken.sessionKey = EncryptDecrypt.encrptMessage(enc_sessionKey, sender_secretKey, Encryption_Type);
                                
                //res.AuthenticationToken.sessionKey = EncryptDecrypt.encrptMessage(res.sessionKey, sender_secretKey, EncryptDecrypt.EncryptionType.AES);
                string resXML = XMLGenerator.getXMLForAuthenticationServer(res);
                return resXML;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        //for fixed version
        public string getAuthenticationTokenFixedVer(string sender_userName, string receiver_userName, string sender_nonce, string r_nonce_and_username, Helper.EncryptDecrypt.EncryptionType Encryption_Type)
        {

            try
            {
                string enc_r_username = XMLGenerator.getNodeValue(r_nonce_and_username, "/ReceiverResponseXMLFixed/sender_userName");
                string enc_r_nonce = XMLGenerator.getNodeValue(r_nonce_and_username, "/ReceiverResponseXMLFixed/receiverNonce");

                string sender_secretKey = KeyCreator.getNodeSecretKey(sender_userName, Encryption_Type);
                AuthenticationServerResponseXMLFixed res = new AuthenticationServerResponseXMLFixed();
                res.sender_userName = EncryptDecrypt.encrptMessage(sender_userName, sender_secretKey, Encryption_Type);
                res.receiver_userName = EncryptDecrypt.encrptMessage(receiver_userName, sender_secretKey, Encryption_Type);
                res.senderNonce = EncryptDecrypt.encrptMessage(sender_nonce, sender_secretKey, Encryption_Type);
                string session_key = KeyCreator.generateSessionKey(256 / 8);
                res.sessionKey = EncryptDecrypt.encrptMessage(session_key, sender_secretKey, Encryption_Type);
                res.AuthenticationToken = new authTokenFixed();
                string receiver_key = KeyCreator.getNodeSecretKey(receiver_userName, Encryption_Type);
                string enc_userName = EncryptDecrypt.encrptMessage(sender_userName, receiver_key, Encryption_Type);
                res.AuthenticationToken.sender_userName = EncryptDecrypt.encrptMessage(enc_userName, sender_secretKey, Encryption_Type);
                //string dec = EncryptDecrypt.decryptMessage(res.AuthenticationToken.sender_userName, receiver_key, EncryptDecrypt.EncryptionType.AES);
                string enc_sessionKey = EncryptDecrypt.encrptMessage(session_key, receiver_key, Encryption_Type);
                res.AuthenticationToken.sessionKey = EncryptDecrypt.encrptMessage(enc_sessionKey, receiver_key, Encryption_Type);
                string enc_receiver_nonce = EncryptDecrypt.encrptMessage(sender_userName, sender_secretKey, Encryption_Type);
                res.AuthenticationToken.receiver_nonce = EncryptDecrypt.encrptMessage(enc_r_nonce, sender_secretKey, Encryption_Type);
                res.AuthenticationToken.sender_userName = EncryptDecrypt.encrptMessage(enc_r_username, sender_secretKey, Encryption_Type);
                //res.AuthenticationToken.sessionKey = EncryptDecrypt.encrptMessage(res.sessionKey, sender_secretKey, EncryptDecrypt.EncryptionType.AES);
                string resXML = XMLGenerator.getXMLForAuthenticationServer(res);
                return resXML;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
