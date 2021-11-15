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
            { "connectedtoservertryingtologin", "Connected to server trying to login" },
            { "loginsuccess", "Login successfull" },
            { "cantgetmessages", "Cant get messages, is mailbox empty?" },
            { "nooption", "No option was selected, falling back to default function, download from inbox, limited messages." },
            { "getunseen", "Print all unseen attachments in mailbox." },
            { "gtnany", "Print all attachments." },
            { "getunseeninboxlimit", "Print unseen attachments from Inbox, limited count." },
            { "getinboxany", "Print all from inbox." },
            { "printingimage", "Printing image." },
            { "cantprintimage", "Cant print image." },
            { "printingpdf", "Printing PDF." },
            { "cantprintpdf", "Cant print PDF." },
            { "folder", "folder" }
        };
    }
}
