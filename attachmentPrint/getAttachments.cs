
using ImapX;

namespace attachmentPrint
{
    
    public class testDemo
    {
        //This is only for test demo
        public bool checkConnection(string srv)
        {
            var client = new ImapClient(srv, true);
            
            if (!client.Connect())
            {
                return false;
            } else
            {
                return true;
            }
        }
    }
}

