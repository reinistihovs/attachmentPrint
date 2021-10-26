using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImapX;


namespace attachmentPrint
{
    class getAttachments
    {

        public void DownloadAllAttachments()
        {
            var appConfiguration = new appConfiguration();

            using (var client = new ImapClient($"{appConfiguration.Host}, {appConfiguration.Port}, {appConfiguration.UseSsl}, {appConfiguration.ValidSsl}", true))
            {
                if (!client.Connect( /* optional, use parameters here */) || !client.Login(appConfiguration.Username, appConfiguration.Password))
                {
                    Console.WriteLine("access to IMAP denied");

                    return;
                }

                string path = string.Intern(Path.Combine(appConfiguration.TempDir, appConfiguration.Username));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                foreach (Folder myfolder in client.Folders)
                {
                    foreach (var message in myfolder.Search())
                    {
                        foreach (var attachment in message.Attachments)
                        {
                            attachment.Download();
                            attachment.Save(path);
                        }
                    }
                }
            }
        }

        public void DownloadUnreadAttachments()
        {
            var appConfiguration = new appConfiguration();

            using (var client = new ImapClient($"{appConfiguration.Host}", true))
            {
                if (!client.Connect( /* optional, use parameters here */) || !client.Login(appConfiguration.Username, appConfiguration.Password))
                    return;

                string path = string.Intern(Path.Combine(appConfiguration.TempDir, appConfiguration.Username));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                foreach (Folder myfolder in client.Folders)
                {
                    foreach (var message in myfolder.Search())
                    {
                        foreach (var attachment in message.Attachments)
                        {
                            attachment.Download();
                            attachment.Save(path);
                        }
                    }
                }
            }
        }

        public void DownloadTodaysAttachments()
        {
            var appConfiguration = new appConfiguration();

            using (var client = new ImapClient($"{appConfiguration.Host}", true))
            {
                if (!client.Connect( /* optional, use parameters here */) || !client.Login(appConfiguration.Username, appConfiguration.Password))
                    return;

                string path = string.Intern(Path.Combine(appConfiguration.TempDir, appConfiguration.Username));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                foreach (Folder myfolder in client.Folders)
                {
                    foreach (var message in myfolder.Search())
                    {
                        foreach (var attachment in message.Attachments)
                        {
                            attachment.Download();
                            attachment.Save(path);
                        }
                    }
                }
            }
        }
    }
}
