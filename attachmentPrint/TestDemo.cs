
using ImapX;

namespace attachmentPrint
{

    public class TestDemo
    {
        //This is only for test demo
        public bool CheckConnection(string srv)
        {
            var client = new ImapClient(srv, true);

            if (!client.Connect())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

