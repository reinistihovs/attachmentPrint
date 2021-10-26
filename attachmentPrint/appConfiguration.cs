using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attachmentPrint
{
    public class appConfiguration
    {
        public string Host { get; set; } = "imap.gmail.com";
        public string ImapFoler { get; set; } = "INBOX";
        public string ProcessedFoler { get; set; } = "IZPRINTETIE";
        public int Port { get; set; } = 587;
        public bool UseSsl { get; set; } = true;
        public bool ValidSsl { get; set; } = true;
        public string Username { get; set; } = "attachementprintcsharp@gmail.com";
        public string Password { get; set; } = "Latvija2021";
        public bool MarkAsRead { get; set; } = true;
        public string TempDir { get; set; }  = "C:\temp";
        public string[] FileTypesToPrint { get; set; } = { "PDF", "pdf", "jpg", "JPEG" };
        public string[] ExcludeFileNames { get; set; } = { "emot", "smile", "SMILE", "Smile", "Emot", "EMOT" };
        public string DefaultPrinter { get; set; } = "Microsoft Print to PDF";
    }
}
