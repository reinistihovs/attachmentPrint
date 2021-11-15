using System;
using System.Collections.Generic;
using System.Configuration;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ImapX;




namespace attachmentPrint
{
    class Program
    {

        static void Main(string[] args)
        {

            

            DumpClass Dump = new DumpClass();
            Dict Dic = new Dict();
            var Options = new Options();
            var email = new email();
            var getAttachments = new testDemo();
            Console.WriteLine("Checking internet connection");
            bool isConnected = getAttachments.checkConnection("imap.gmail.com");
            if (!isConnected) {
                Dump.ToScreenAndLog($"{LogLevel.Error}: cant connect to gmail, do you have internet?");
                Console.WriteLine("Press Enter N to Exit... ");
                while (Console.ReadKey().Key != ConsoleKey.N) { }
                return;

            } else
            { 
                Dump.ToScreenAndLog($"{LogLevel.Info}: test connection to imap.gmail.com succesfull... ");

            }

            bool searchLimit;
            bool unseenOnly;
            bool searchInAllFolders;

            Console.WriteLine("Please choose action:");
            Console.WriteLine("1)  to print unseen attachments from INBOX and mark as read, limited count");
            Console.WriteLine("2)  to print all attachments from INBOX");
            Console.WriteLine("3)  to print all attachments from entire mailbox");
            Console.WriteLine("4)  to print all unseen attachments from entire mailbox");

            //Console.WriteLine("3) Print unregistered attachment and register email ID in database");
            Console.WriteLine("press any other key to EXIT");
            //int Choice = 0;
            int Action = int.Parse(Console.ReadLine());
            if (Action == 1)
            {
                Dump.ToScreenAndLog($"{LogLevel.Info}: Selection: 1 ");
                searchLimit = true;
                unseenOnly = true;
                searchInAllFolders = false;
            }
            else if (Action == 2)
            {
                Dump.ToScreenAndLog($"{LogLevel.Info}: Selection: 2 ");
                searchLimit = false;
                unseenOnly = false;
                searchInAllFolders = false;
            } 
            else if (Action == 3)
            {
                Dump.ToScreenAndLog($"{LogLevel.Info}: Selection: 3 ");
                searchLimit = false;
                unseenOnly = false;
                searchInAllFolders = true;
            }
            else if (Action == 4)
            {
                Dump.ToScreenAndLog($"{LogLevel.Info}: Selection: 3 ");
                searchLimit = false;
                unseenOnly = true;
                searchInAllFolders = true;
            }
            else { 

                Dump.ToScreenAndLog($"{LogLevel.Warn}: Invalid action selected press ENTER to exit, please start again");
                while (Console.ReadKey().Key != ConsoleKey.Enter) {}
                return;
            }

            var processedMessages = new List<Message>();

            Message[] emailMessages = email.GetMessages(searchLimit, unseenOnly, searchInAllFolders);


            if (emailMessages.Any())
            {
                foreach (var message in emailMessages)
                {
                    Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["procmsg"]}... UID: {message.UId}, FROM: {message.From}, TO: {String.Join(", ", message.To.Select(t => t.Address))}, SUBJECT: {message.Subject}");

                    //message.Seen = true;

                    if (message.Attachments.Any() || message.EmbeddedResources.Any())
                    {
                        // Save attachments
                        foreach (var attachment in message.Attachments)
                        {
                            //Download & Save
                            if (Options.FileTypesToPrint.Any(attachment.FileName.Contains)) {
                                attachment.Download();
                                var fileName = String.Format("{0}-{2}{1}", Path.GetFileNameWithoutExtension(attachment.FileName), Path.GetExtension(attachment.FileName), Guid.NewGuid());                               
                                attachment.Save(Options.Dir, fileName);
                                string fileDir = $"{Options.Dir}\\{fileName}";
                                if (Options.PdfExtensions.Any(attachment.FileName.Contains))
                                {
                                    PdfFile myPdf = new PdfFile(); // Create abstract object
                                    myPdf.sendToPrinter(Options.DefaultPrinter, "A4", fileDir, 0);  // Call the abstract method
                                    Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["attsave"]}: {fileName}");
                                }
                                else if (Options.PictureExtensions.Any(attachment.FileName.Contains))
                                {
                                    PictureFile myPict = new PictureFile(); // Create abstract object
                                    myPict.sendToPrinter(Options.DefaultPrinter, "A4", fileDir, 0);  // Call the abstract method
                                    Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["attsave"]}: {fileName}");
                                }
                            }
                                

                        }
                        // Save Inline embedded attachments
                        foreach (var embedded in message.EmbeddedResources)
                        {
                            if (Options.FileTypesToPrint.Any(embedded.FileName.Contains))
                            {
                                embedded.Download();

                                var fileName = String.Format("{0}-{2}{3}{1}", Path.GetFileNameWithoutExtension(embedded.FileName), Path.GetExtension(embedded.FileName), Guid.NewGuid(), "-Embedded");

                                embedded.Save(Options.Dir, fileName);
                                string fileDir = $"{Options.Dir}\\{fileName}";
                                if (Options.PdfExtensions.Any(embedded.FileName.Contains))
                                {
                                    PdfFile myPdf = new PdfFile(); // Create abstract object
                                    myPdf.sendToPrinter(Options.DefaultPrinter, "A4", fileDir, 0);  // Call the abstract method
                                    Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["attsave"]}: {fileName}");
                                }
                                else if (Options.PictureExtensions.Any(embedded.FileName.Contains))
                                {
                                    PictureFile myPict = new PictureFile(); // Create abstract object
                                    myPict.sendToPrinter(Options.DefaultPrinter, "A4", fileDir, 0);  // Call the abstract method
                                    Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["attsave"]}: {fileName}");
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        Dump.ToScreenAndLog($"{LogLevel.Info} {Dic.Msgs["nonewmsg"]}");
                        Dump.ToLogOnly($"{LogLevel.Info}: {Dic.Msgs["nonewmsg"]}");

                    }

                    processedMessages.Add(message);

                }
            }
            else
            {
                Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["nounpromsg"]}");
                Dump.ToLogOnly($"{LogLevel.Info}: {Dic.Msgs["nounpromsg"]}");
            }


        }
    }

}
