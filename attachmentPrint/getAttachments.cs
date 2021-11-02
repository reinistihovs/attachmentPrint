using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ImapX;

namespace attachmentPrint
{

    public class getAttachments
    {
        DumpClass Dump = new DumpClass();
        Dict Dic = new Dict();
        Array imapConnect(Array i)
        {

            var appConfiguration = new appConfiguration();

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
                Dump.ToScreenAndLog($"{LogLevel.Fail} {Dic.Msgs["cantconnectserver"]}");

                return i; // exits programm
            }

            if (!client.Login(login, password))
            {
                Dump.ToScreenAndLog($"{LogLevel.Fail} {Dic.Msgs["cantlogin"]}");
                return i; // exits programm
            }

            var inboxFolder = client.Folders.FirstOrDefault(f => f.Name == inboxFolderName);
            if (inboxFolder == null)
            {
                Dump.ToScreenAndLog($"{LogLevel.Fail} {Dic.Msgs["noinbox"]}");
                return i; // exits programm
            }
                        var numberOfMessagesLimit = 100;
            var messages = inboxFolder.Search("ALL", client.Behavior.MessageFetchMode, numberOfMessagesLimit);
            i = messages;
            return i;
        }


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

        private void downloadAttachments()
        {

        }
        public void downloadAllUnreadAttchments()
        {
            //imapConnect();


            foreach (var m in imapConnect) {
                if (!m.Seen) {
                    var myattach = m.Attachments.ToList();
                    myattach.AddRange(m.EmbeddedResources.ToList());
                    foreach (var r in myattach) {
                        var fileName = String.Format("{0}-{2}{1}", Path.GetFileNameWithoutExtension(r.FileName), Path.GetExtension(r.FileName), Guid.NewGuid());
                        r.Download();
                        r.Save(attachmentsPath, fileName);
                     }
                 // foreach (var r in m.EmbeddedResources) {
                 //   var fileName = String.Format("{0}-{2}{1}", Path.GetFileNameWithoutExtension(r.FileName), Path.GetExtension(r.FileName), Guid.NewGuid());
//
  //                  r.Download();
    //                r.Save(attachmentsPath, fileName);
      //            }

                }
//#mark messages as read
         //    m.Flags.Add("\SEEN")
 

            }
        }
        public void downloadAllUnseenAttchments()
        {



            inboxFolder.Messages.Download();

            if (inboxFolder.Messages.Any())
            {
                foreach (var message in inboxFolder.Messages)
                {
                    Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["procmsg"]}... UID: {message.UId}, FROM: {message.From}, TO: {String.Join(", ", message.To.Select(t => t.Address))}, SUBJECT: {message.Subject}");

                    message.Seen = true;

                    if (message.Attachments != null && message.Attachments.Any())
                    {
                        foreach (var attachment in message.Attachments)
                        {
                            attachment.Download();

                            var fileName = String.Format("{0}-{2}{1}", Path.GetFileNameWithoutExtension(attachment.FileName), Path.GetExtension(attachment.FileName), Guid.NewGuid());

                            attachment.Save(attachmentsPath, fileName);
                            Process.Start($"LPR -S {appConfiguration.DefaultPrinter} -P raw {attachmentsPath + fileName}");
                            Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["attsave"]}: {fileName}");
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

                    Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["msg"]} {message.UId} {Dic.Msgs["mved"]} {processedFolderName} {Dic.Msgs["folder"]}.");
                }
            }
        }

    }
}

