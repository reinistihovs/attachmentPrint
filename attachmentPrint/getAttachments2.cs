using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ImapX;

namespace attachmentPrint
{

    public class getAttachments2
    {
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

        public void downloadAllAttachments()
        {
            

            var appConfiguration = new appConfiguration();
            var getAttachments = new getAttachments();

            var server = appConfiguration.Host;
            var login = appConfiguration.Username;
            var password = appConfiguration.Password;

            var attachmentsPath = appConfiguration.TempDir;
            var inboxFolderName = appConfiguration.ImapFoler;
            var processedFolderName = appConfiguration.ProcessedFoler;

            var processedMessages = new List<Message>();

            var client = new ImapClient(appConfiguration.Host, true);

            if (!client.Connect())
            {
                return;
            }

            if (!client.Login(login, password))
            {
                return;
            }

            var inboxFolder = client.Folders.FirstOrDefault(f => f.Name == inboxFolderName);
            if (inboxFolder == null)
            {
                return;
            }

            inboxFolder.Messages.Download();

            if (inboxFolder.Messages.Any())
            {
                foreach (var message in inboxFolder.Messages)
                {
                    String.Format("Processing message... UID: {0}, FROM: {1}, TO: {2}, SUBJECT: {3}",
                        //message.UId, message.From, String.Join(", ", message.To.Select(t => t.Address)), message.Subject).Dump();
                         message.UId, message.From, String.Join(", ", message.To.Select(t => t.Address)), message.Subject);

                    message.Seen = true;

                    if (message.Attachments != null && message.Attachments.Any())
                    {
                        foreach (var attachment in message.Attachments)
                        {
                            attachment.Download();

                            var fileName = String.Format("{0}-{2}{1}", Path.GetFileNameWithoutExtension(attachment.FileName), Path.GetExtension(attachment.FileName), Guid.NewGuid());

                            attachment.Save(attachmentsPath, fileName);

                            //String.Format("Attachment saved: {0}", fileName).Dump();
                            //message.UId, message.From, String.Join(", ", message.To.Select(t => t.Address)), message.Subject).Dump();
                            Console.WriteLine(String.Format("Attachment saved: {0}", fileName));
                           // message.UId, message.From, String.Join(", ", message.To.Select(t => t.Address)), message.Subject;
                        }
                    }
                    else
                    {
                        //"No attachments found.".Dump();
                        Console.WriteLine("No attachments found.");
                    }

                    processedMessages.Add(message);

                    //"".Dump();
                }
            }
            else
            {
                //"No unprocessed messages found.".Dump();
                Console.WriteLine("No unprocessed messages found.");
            }

            if (processedMessages.Any())
            {
                var processedFolder = client.Folders.FirstOrDefault(f => f.Name == processedFolderName);

                if (processedFolder == null)
                {
                    processedFolder = client.Folders.Add(processedFolderName);
                }

                foreach (var message in processedMessages)
                {
                    message.MoveTo(processedFolder);

                    Console.WriteLine( String.Format("Message {0} moved to {1} folder.", message.UId, processedFolderName));
                    //String.Format("Message {0} moved to {1} folder.", message.UId, processedFolderName).Dump();
                }
            }
        }

    }
}

