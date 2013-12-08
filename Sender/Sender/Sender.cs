using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Helper;
using System.Diagnostics;

namespace ClientAppliaction
{
    public partial class Sender : Form
    {
        public static string secretKey = string.Empty;
        public static string sessionKey = string.Empty;
        EncryptDecrypt.EncryptionType enc_type;
        int version_type = 0;    //0 for flawed and 1 for fixed
        string pass = string.Empty;
        string textInRTB = string.Empty;
        public Sender()
        {
            InitializeComponent();
            cmbEncType.SelectedIndex = 0;
            cmbBoxVersion.SelectedIndex = 0;
            //LoadComboBoxEncType();
            backgroundWorker_txtAreaUpdate.WorkerReportsProgress = true;
            pass = "this is my password..it needs to be 256 bit long";
            secretKey = EncryptDecrypt.getNodeSecretKey(pass, enc_type);
            textInRTB = "CSE 535 (Fall 2013) Asynchronous Systems Final Project\n";
            textInRTB += "Needham Schroeder Symmetric Key Protocol Demo\n";
            textInRTB += "------------------------------------------------------------------------------------------------\n";
            rtb_output.Text += textInRTB;            
        }

        /*
        private void LoadComboBoxEncType()
        {            
        }
        */

        private void connect_Click(object sender, EventArgs e)
        {
            rtb_output.Text = textInRTB;
            backgroundWorker_txtAreaUpdate.RunWorkerAsync();

        }

        private void backgroundWorker_txtAreaUpdate_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            rtb_output.Text += e.UserState as string;
         
        }
        private void backgroundWorker_txtAreaUpdate_DoWork(object sender, DoWorkEventArgs e)
        {            
            Stopwatch sw = Stopwatch.StartNew();
            if (version_type == 0)  //flawed
            {
                string res = string.Empty;
                Random rnd = new Random();
                string sender_nonce = rnd.Next(0, Int32.MaxValue).ToString();

                backgroundWorker_txtAreaUpdate.ReportProgress(0,
                    "A sends a message to the S identifying herself and B, telling the server she wants to communicate with B\n");

                backgroundWorker_txtAreaUpdate.ReportProgress(0,
                                string.Format(" A  ->S : A [{0}], B [{1}], N_a [{2}]\n\n", txtSenderUser.Text, txtRecUser.Text, sender_nonce));

                res = FlawedVersionDesign.requestSessionKey(secretKey, txtSenderUser.Text, txtRecUser.Text, sender_nonce, enc_type);

                string en_s_nonce = XMLGenerator.getNodeValue(res, "/AuthenticationServerResponseXML/senderNonce");

                string e_sessionKey = XMLGenerator.getNodeValue(res, "/AuthenticationServerResponseXML/sessionKey");
                sessionKey = EncryptDecrypt.decryptMessage(e_sessionKey, secretKey, enc_type);

                string e_sessionKey_receiver = XMLGenerator.getNodeValue(res, "/AuthenticationServerResponseXML/AuthenticationToken/sessionKey");
                string e_sender_userName_receiver = XMLGenerator.getNodeValue(res, "/AuthenticationServerResponseXML/AuthenticationToken/sender_userName");
                string e_r_userName = XMLGenerator.getNodeValue(res, "/AuthenticationServerResponseXML/receiver_userName");

                backgroundWorker_txtAreaUpdate.ReportProgress(0, "Replay from S\n");
                backgroundWorker_txtAreaUpdate.ReportProgress(0, string.Format("S ->A : {{N_a [{0}], K_ab [{1}], B [{2}], {{K_ab [{3}], A [{4}]}}K_bs}}K_as\n\n", en_s_nonce, e_sessionKey, e_r_userName, e_sessionKey_receiver, e_sender_userName_receiver));

                
                backgroundWorker_txtAreaUpdate.ReportProgress(0, "A forwards the key to B who can decrypt it with the key he shares with the server, thus authenticating the data\n");
                backgroundWorker_txtAreaUpdate.ReportProgress(0, string.Format("A ->B : {{K_ab [{0}], A [{1}]}}K_bs  \n\n", e_sessionKey_receiver, e_sender_userName_receiver));

                string en_nonce = FlawedVersionDesign.requestAuthentication(secretKey, sessionKey, e_sessionKey_receiver, e_sender_userName_receiver, enc_type);
                string receiver_nonce = EncryptDecrypt.decryptMessage(en_nonce, sessionKey, enc_type);
                backgroundWorker_txtAreaUpdate.ReportProgress(0, "B sends A a nonce encrypted under {K_{AB}} to show that he has the key.\n");
                backgroundWorker_txtAreaUpdate.ReportProgress(0, string.Format("B ->A : {{N_b [{0}]}}K_ab\n\n", en_nonce));


                string enc_receiver_nonce = EncryptDecrypt.encrptMessage((Convert.ToInt32(receiver_nonce) - 1).ToString(), sessionKey, enc_type);

                backgroundWorker_txtAreaUpdate.ReportProgress(0, "A performs a simple operation on the nonce, re-encrypts it and sends it back verifying that she is still alive and that she holds the key.\n");
                backgroundWorker_txtAreaUpdate.ReportProgress(0, string.Format("A ->B : {{N_b-1 [{0}]}}K_ab\n\n", enc_receiver_nonce));

                string finalOutput = FlawedVersionDesign.ackVerificationAndAlive(sessionKey, enc_receiver_nonce, enc_type);
                backgroundWorker_txtAreaUpdate.ReportProgress(0, finalOutput + "\n");
                sw.Stop();
                backgroundWorker_txtAreaUpdate.ReportProgress(0, "Total time: " + sw.Elapsed.TotalSeconds.ToString());
            }
            else
            {

                string res = string.Empty;
                Random rnd = new Random();
                string sender_nonce = rnd.Next(0, Int32.MaxValue).ToString();

                backgroundWorker_txtAreaUpdate.ReportProgress(0,
                    "A sends to B a request.\n");

                backgroundWorker_txtAreaUpdate.ReportProgress(0,
                                string.Format("A ->B : A [{0}]\n\n", txtSenderUser.Text));



                backgroundWorker_txtAreaUpdate.ReportProgress(0,
                    "B responds with a nonce encrypted under his key with the Server.\n");

                res = ClientApplication.FixedVersionDesign.GetNonceForAuthentication(txtSenderUser.Text, enc_type);
                string enc_r_username = XMLGenerator.getNodeValue(res, "/ReceiverResponseXMLFixed/sender_userName");
                string enc_r_nonce = XMLGenerator.getNodeValue(res, "/ReceiverResponseXMLFixed/receiverNonce");
                                
                backgroundWorker_txtAreaUpdate.ReportProgress(0,
                                string.Format("B ->A : {{ A [{0}], N_b [{1}]}}K_bs \n\n", enc_r_username, enc_r_nonce));



                backgroundWorker_txtAreaUpdate.ReportProgress(0,
                    "A sends a message to the S identifying herself and B, telling the server she wants to communicate with B\n");

                backgroundWorker_txtAreaUpdate.ReportProgress(0,
                                string.Format("A ->S : A [{0}], B [{1}], N_a [{2}], {{A [{3}], N_b [{4}]}}K_bs\n\n", txtSenderUser.Text, txtRecUser.Text, sender_nonce,enc_r_username,enc_r_nonce));

                
                res = ClientApplication.FixedVersionDesign.requestSessionKey(secretKey, txtSenderUser.Text, txtRecUser.Text, sender_nonce, res, enc_type);


                string en_s_nonce = XMLGenerator.getNodeValue(res, "/AuthenticationServerResponseXMLFixed/senderNonce");

                string e_sessionKey_receiver = XMLGenerator.getNodeValue(res, "/AuthenticationServerResponseXMLFixed/AuthenticationToken/sessionKey");
                string e_sender_userName_receiver = XMLGenerator.getNodeValue(res, "/AuthenticationServerResponseXMLFixed/AuthenticationToken/sender_userName");
                string en_r_nonce = XMLGenerator.getNodeValue(res, "/AuthenticationServerResponseXMLFixed/AuthenticationToken/receiver_nonce");



                string e_sessionKey = XMLGenerator.getNodeValue(res, "/AuthenticationServerResponseXMLFixed/sessionKey");
                sessionKey = EncryptDecrypt.decryptMessage(e_sessionKey, secretKey, enc_type);

                string e_r_userName = XMLGenerator.getNodeValue(res, "/AuthenticationServerResponseXMLFixed/receiver_userName");
                
                backgroundWorker_txtAreaUpdate.ReportProgress(0, "Replay from S\n");
                backgroundWorker_txtAreaUpdate.ReportProgress(0, string.Format("S ->A : {{N_a [{0}], K_ab [{1}], B [{2}], {{K_ab [{3}], A [{4}], N_b [{5}]}}K_bs}}K_as\n\n", en_s_nonce, e_sessionKey, e_r_userName, e_sessionKey_receiver, e_sender_userName_receiver, enc_r_nonce));


                backgroundWorker_txtAreaUpdate.ReportProgress(0, "A forwards the key to B who can decrypt it with the key he shares with the server, thus authenticating the data\n");
                backgroundWorker_txtAreaUpdate.ReportProgress(0, string.Format("A ->B : {{K_ab [{0}], A [{1}]}}K_bs  \n\n", e_sessionKey_receiver, e_sender_userName_receiver));


                string en_nonce = ClientApplication.FixedVersionDesign.requestAuthentication(secretKey, sessionKey, e_sessionKey_receiver, e_sender_userName_receiver, en_r_nonce, enc_type);

                string receiver_nonce = EncryptDecrypt.decryptMessage(en_nonce, sessionKey, enc_type);
                backgroundWorker_txtAreaUpdate.ReportProgress(0, "B sends A a nonce encrypted under {K_{AB}} to show that he has the key.\n");
                backgroundWorker_txtAreaUpdate.ReportProgress(0, string.Format("B ->A : {{N_b [{0}]}}K_ab\n\n", en_nonce));


                string enc_receiver_nonce = EncryptDecrypt.encrptMessage((Convert.ToInt32(receiver_nonce) - 1).ToString(), sessionKey, enc_type);

                backgroundWorker_txtAreaUpdate.ReportProgress(0, "A performs a simple operation on the nonce, re-encrypts it and sends it back verifying that she is still alive and that she holds the key.\n");
                backgroundWorker_txtAreaUpdate.ReportProgress(0, string.Format("A ->B : {{N_b-1 [{0}]}}K_ab\n\n", enc_receiver_nonce));

                string output = ClientApplication.FixedVersionDesign.ackVerificationAndAlive(sessionKey, enc_receiver_nonce, enc_type);
                backgroundWorker_txtAreaUpdate.ReportProgress(0, output + "\n");
                sw.Stop();
                backgroundWorker_txtAreaUpdate.ReportProgress(0, "Total time: " + sw.Elapsed.TotalSeconds.ToString());
            }


        }

        private void cmbEncType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string enc = cmbEncType.Text;
            if (enc == "AES")
                enc_type = EncryptDecrypt.EncryptionType.AES;
            else
                enc_type = EncryptDecrypt.EncryptionType.DES;

            secretKey = EncryptDecrypt.getNodeSecretKey(pass, enc_type);
            
        }

        private void cmbBoxVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            version_type = cmbBoxVersion.SelectedIndex;
        }

        
        
    }
}
