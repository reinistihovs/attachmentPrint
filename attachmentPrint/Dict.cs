using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attachmentPrint
{
    public class Dict
    {
        public Dictionary<string, string> Msgs { get; } = new Dictionary<string, string>()
        {
            { "cantconnectserver", "Cant connect to server" },
            { "cantlogin", "Cant login to server" },
            { "noinbox", "Inbox folder not found, try to change name in config." },
            { "procmsg", "Proccessing message." },
            { "attsave", "Attachment saved" },
            { "nonewmsg", "There are new messages, but no attachments were found." },
            { "nounpromsg", "No unprocessed messages found." },
            { "msg", "Message" },
            { "mved", "moved to" },
            { "folder", "folder" }
        };
    }
}
