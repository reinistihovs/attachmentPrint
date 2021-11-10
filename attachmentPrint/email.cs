using ImapX;
using ImapX.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attachmentPrint
{
    class email
    {
        appConfiguration appConfiguration = new appConfiguration();
        DumpClass Dump = new DumpClass();
        Dict Dic = new Dict();

        public ImapX.ImapClient ImapClient
        {
            get; set;
        }
        public Message[] LoadMessages()
        {
            Message[] messages = null;
            try
            {
                bool connected = false;
                try
                {
                    var ImapClient = new ImapX.ImapClient(appConfiguration.Host, appConfiguration.Port, appConfiguration.UseSsl);

                    if (!ImapClient.IsConnected)
                        ImapClient.Connect();

                    if (ImapClient.IsConnected)
                    {
                        if (!ImapClient.IsAuthenticated)
                            ImapClient.Login(appConfiguration.Username, appConfiguration.Password);
                        if (ImapClient.IsAuthenticated)
                        {
                            connected = true;
                        }

                    }

                }
                catch (Exception e)
                {
                    Dump.ToScreenAndLog($"{LogLevel.Error} {Dic.Msgs["cantconnectserver"]} Server: {appConfiguration.Host}  SSL_ENABLED: {appConfiguration.UseSsl}  PORT:{appConfiguration.Port}  ERROR MSG:{e}");
                    connected = false;
                }

                if (connected)
                {
                    var folder = ImapClient.Folders.Inbox;
                    messages = folder.Search("UNSEEN", MessageFetchMode.Basic);

                }
            }
            catch (Exception e)
            {
                Dump.ToScreenAndLog($"{LogLevel.Error} {Dic.Msgs["cantlogin"]} Server: {appConfiguration.Host}  SSL_ENABLED: {appConfiguration.UseSsl}  PORT:{appConfiguration.Port} Username: {appConfiguration.Username} ERROR MSG:{e} ");

            }
            ImapClient = null;
            return messages;
        }
    }
}
