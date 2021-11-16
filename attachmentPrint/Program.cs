using ImapX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;




namespace attachmentPrint
{
    class Program
    {

        static void Main(string[] args)
        {
            DumpClass Dump = new DumpClass();
            Dict Dic = new Dict();
            var Options = new Options();
            var email = new Email();
            var getAttachments = new TestDemo();
            bool searchLimit;
            bool unseenOnly;
            bool searchInAllFolders;


            Console.WriteLine($"{Dic.Msgs["chkcon"]}");
            bool isConnected = getAttachments.CheckConnection("imap.gmail.com");
            if (!isConnected)
            {
                Dump.ToScreenAndLog($"{LogLevel.Error}: {Dic.Msgs["cntcon"]}");
                //Console.WriteLine("Press Enter  to Exit... ");
                //while (Console.ReadKey().Key != ConsoleKey.N) { }
                return;
            }
            else
            {
                Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["tstcon"]} ");
            }


            static int Choice()
            {
                Dict Dic = new Dict();
                int choice = 0;
                Console.Clear();
                Console.WriteLine($"{Dic.Msgs["chact"]}");
                Console.WriteLine($"{Dic.Msgs["actone"]}");
                Console.WriteLine($"{Dic.Msgs["acttwo"]}");
                Console.WriteLine($"{Dic.Msgs["actthree"]}");
                Console.WriteLine($"{Dic.Msgs["actfour"]}");
                Console.WriteLine($"{Dic.Msgs["actother"]}");
                choice = int.Parse(Console.ReadLine());
                if (choice > 0 && choice <= 5)
                {
                    Console.WriteLine($"{Dic.Msgs["ltsgo"]}");
                }
                else
                {
                    Console.WriteLine($"{Dic.Msgs["invsel"]}");
                    return 5;
                }
                return choice;
            }
            int Action = Choice();
            if (Action == 1)
            {
                Dump.ToLogOnly($"{LogLevel.Info}: Selection: 1 ");
                searchLimit = true;
                unseenOnly = true;
                searchInAllFolders = false;
            }
            else if (Action == 2)
            {
                Dump.ToLogOnly($"{LogLevel.Info}: Selection: 2 ");
                searchLimit = false;
                unseenOnly = false;
                searchInAllFolders = false;
            }
            else if (Action == 3)
            {
                Dump.ToLogOnly($"{LogLevel.Info}: Selection: 3 ");
                searchLimit = false;
                unseenOnly = false;
                searchInAllFolders = true;
            }
            else if (Action == 4)
            {
                Dump.ToLogOnly($"{LogLevel.Info}: Selection: 4 ");
                searchLimit = false;
                unseenOnly = true;
                searchInAllFolders = true;
            }
            else
            {
                Dump.ToScreenAndLog($"{LogLevel.Error}: {Dic.Msgs["noselgoodbye"]}");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                return;
            }

            var processedMessages = new List<Message>();
            //Dump.ToScreenAndLog($"{LogLevel.Info}: Please wait, searching for attachments ");
            Message[] emailMessages = email.GetMessages(searchLimit, unseenOnly, searchInAllFolders);


            if (emailMessages.Any())
            {
                foreach (var message in emailMessages)
                {
                    Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["procmsg"]}... UID: {message.UId}, FROM: {message.From}, TO: {String.Join(", ", message.To.Select(t => t.Address))}, SUBJECT: {message.Subject}");
                    if (message.Attachments.Any() || message.EmbeddedResources.Any())
                    {
                        // Save and print attached attachments
                        foreach (var attachment in message.Attachments)
                        {
                            message.Seen = true;
                            if (Options.FileTypesToPrint.All(attachment.FileName.Contains))
                            {
                                attachment.Download();
                                var fileName = String.Format("{0}-{2}{1}", Path.GetFileNameWithoutExtension(attachment.FileName), Path.GetExtension(attachment.FileName), Guid.NewGuid());
                                attachment.Save(Options.Dir, fileName);
                                string fileDir = $"{Options.Dir}\\{fileName}";
                                if (Options.PdfExtensions.All(attachment.FileName.Contains))
                                {
                                    PdfFile myPdf = new PdfFile();
                                    myPdf.SendToPrinter(Options.DefaultPrinter, "A4", fileDir);
                                    Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["attsave"]}: {fileName}");
                                }
                                else if (Options.PictureExtensions.All(attachment.FileName.Contains))
                                {
                                    PictureFile myPict = new PictureFile();
                                    myPict.SendToPrinter(Options.DefaultPrinter, "A4", fileDir);
                                    Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["attsave"]}: {fileName}");
                                }
                            }


                        }
                        // Save and print Inline embedded attachments
                        foreach (var embedded in message.EmbeddedResources)
                        {
                            message.Seen = true;
                            if (Options.FileTypesToPrint.Any(embedded.FileName.Contains))
                            {  
                                embedded.Download();                    
                                var fileName = String.Format("{0}-{2}{3}{1}", Path.GetFileNameWithoutExtension(embedded.FileName), Path.GetExtension(embedded.FileName), Guid.NewGuid(), "-Embedded");
                                embedded.Save(Options.Dir, fileName);
                                string fileDir = $"{Options.Dir}\\{fileName}";
                                if (Options.PdfExtensions.Any(embedded.FileName.Contains))
                                {     
                                    PdfFile myPdf = new(); // Create abstract
                                    myPdf.SendToPrinter(Options.DefaultPrinter, "A4", fileDir);
                                    Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["attsave"]}: {fileName}");
                                }
                                else if (Options.PictureExtensions.Any(embedded.FileName.Contains))
                                {   
                                    PictureFile myPict = new(); // Create abstract
                                    myPict.SendToPrinter(Options.DefaultPrinter, "A4", fileDir); 
                                    Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["attsave"]}: {fileName}");
                                }
                            }
                        }
                    }
                    else
                    {
                        Dump.ToScreenAndLog($"{LogLevel.Info} {Dic.Msgs["nonewmsg"]}");
                        

                    }

                    

                }
            }
            else
            {
                Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["nounpromsg"]}");
                
            }



        }
    }

}
