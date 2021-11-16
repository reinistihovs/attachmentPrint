using System.Collections.Generic;

namespace attachmentPrint
{
    public class Dict
    {
        public Dictionary<string, string> Msgs { get; } = new Dictionary<string, string>()
        {
            { "cantconnectserver", "Cant connect to server" },
            { "chkcon", "Checking internet connection" },
            { "cntcon", "cant connect to gmail, do you have internet?" },
            { "tstcon", "test connection to imap.gmail.com succesfull..." },
            { "chact", "Please choose action:" },
            { "actone", "1)  to print unseen attachments from INBOX "},
            { "acttwo", "2)  to print all attachments from INBOX" },
            { "actthree", "3)  to print all attachments from entire mailbox" },
            { "actfour", "4)  to print all unseen attachments from entire mailbox" },
            { "actother", "(any other)  exit" },
            { "ltsgo", "Lets go! "},
            { "invsel", "Invalid selection, exiting" },
            { "noselgoodbye", "No selection, Goodbye!" },
            { "plswait", "Please wait, searching for attachments" },
            { "cantlogin", "Cant login to server" },
            { "noinbox", "Inbox folder not found, try to change name in config." },
            { "procmsg", "Proccessing message." },
            { "attsave", "Attachment saved" },
            { "nonewmsg", "Message has no attachment." },
            { "nounpromsg", "No unprocessed messages found." },
            { "msg", "Message" },
            { "mved", "moved to" },
            { "connectedtoservertryingtologin", "Connected to server trying to login" },
            { "loginsuccess", "Login successfull" },
            { "cantgetmessages", "Cant get messages, is mailbox empty?" },
            { "nooption", "No option was selected, falling back to default function, download from inbox, limited messages." },
            { "getunseen", "Print all unseen attachments in mailbox." },
            { "genany", "Print all attachments." },
            { "gtnanyunseen", "Print all unseen attachments." },
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
