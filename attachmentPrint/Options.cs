using CommandLine;

namespace attachmentPrint
{
    public class Options
    {
        [Option('m', "mode", Required = false, HelpText = "Set mode, 1 = print unseen from inbox, 2 = print all from inbox, 3 = print all, 4 = print all unseen ")]
        public int Mode { get; set; } = 1;
        [Option('t', "host", Required = true, HelpText = "Set imap hostname.")]
        public string Host { get; set; } = "imap.gmail.com";
        [Option('f', "imapfolder", Required = false, HelpText = "Set imap folder to search for attachments")]
        public string ImapFolder { get; set; } = "INBOX";
        [Option('x', "processedfolder", Required = false, HelpText = "Where to move processed messages, works if set")]
        public string ProcessedFoler { get; set; } = "IZPRINTETIE";
        [Option('g', "movetoprocessedfolder", Required = false, HelpText = "move messages to processed folder?")]
        public bool MoveProcessedFoler { get; set; } = false;
        [Option('y', "port", Required = false, HelpText = "Set IMAP port.")]
        public int Port { get; set; } = 587;
        [Option('l', "limit", Required = false, HelpText = "Set limit for messages to be searched.")]
        public int SearchLimit { get; set; } = 100;
        [Option('s', "ssl", Required = false, HelpText = "Use ssl?, true, false")]
        public bool UseSsl { get; set; } = true;
        [Option('u', "username", Required = true, HelpText = "Imap username.")]
        public string Username { get; set; } = "attachementprintcsharp@gmail.com";
        [Option('p', "password", Required = false, HelpText = "Imap password.")]
        public string Password { get; set; } = "Latvija2021";
        [Option('q', "markread", Required = true, HelpText = "Mark as read? true, false.")]
        public bool MarkAsRead { get; set; } = true;
        [Option('d', "directory", Required = false, HelpText = "where to store files?. please use following syntax: @\"C:\\temp\" ")]
        public string Dir { get; set; } = @"C:\temp";
        [Option('o', "loglocation", Required = false, HelpText = "Set output to verbose messages.")]
        public string LogLocation { get; set; } = @"C:\temp\attachmentprint.log";
        [Option('j', "filetoprint", Required = false, HelpText = "Set output to verbose messages.")]
        public string[] FileTypesToPrint { get; set; } = { "PDF", "pdf", "JPG", "jpg", "Jpg", "Jpeg", "jpeg", "JPEG", "BMP", "PNG", "png", "bmp" };
        [Option('e', "exclude", Required = false, HelpText = "what file types to exclude?")]
        public string[] ExcludeFileNames { get; set; } = { "emot", "smile", "SMILE", "Smile", "Emot", "EMOT" };
        [Option('a', "printer", Required = false, HelpText = "Full name of printer.")]
        public string DefaultPrinter { get; set; } = "PDFCreator";
        [Option('n', "pdfextensions", Required = false, HelpText = "List of PDF extensions to print.")]
        public string[] PdfExtensions { get; set; } = { "PDF", "pdf" };
        [Option('z', "pictureextensions", Required = false, HelpText = "List of picture extensions.")]
        public string[] PictureExtensions { get; set; } = { "JPG", "jpg", "Jpg", "Jpeg", "jpeg", "JPEG", "BMP", "PNG", "png", "bmp" };
    }
}
