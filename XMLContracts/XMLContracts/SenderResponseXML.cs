using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/* ResponseXML
<AuthenticationResponse>
<SenderID/>
<ReceiverID/>
<SenderNonce/>
<SessionKey/>
<AuthenticationToken>
    <SenderID/>
    <SessionKey/>
</AuthenticationToken>
</AuthenticationResponse>
*/

namespace XMLContracts
{
    public class authToken
    {
        public string sender_userName;
        public string sessionKey;
    }

    public class AuthenticationServerResponseXML
    {
        public string sender_userName;
        public string receiver_userName;
        public string senderNonce;
        public string sessionKey;
        public authToken AuthenticationToken;        
    }


    public class ReceiverResponseXML
    {
        public string receiverNonce;
        
    }

    public class authTokenFixed
    {
        public string sender_userName;
        public string receiver_nonce;
        public string sessionKey;
    }

    public class AuthenticationServerResponseXMLFixed
    {
        public string sender_userName;
        public string receiver_userName;
        public string senderNonce;
        public string sessionKey;
        public authTokenFixed AuthenticationToken;
    }

    public class ReceiverResponseXMLFixed
    {
        public string sender_userName;
        public string receiverNonce;

    }

}
