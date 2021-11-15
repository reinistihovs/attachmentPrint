using ImapX;
using ImapX.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace attachmentPrint
{
    class email
    {
        Options Options = new Options();
        DumpClass Dump = new DumpClass();
        Dict Dic = new Dict();

        private ImapClient ImapClient { get; set; }
        private ImapClient connectImap()
        {
            ImapClient = null;
            try
            {
                bool connected = false;
                try
                {
                    // Configure IMAP client
                    if (ImapClient == null) ImapClient = new ImapClient(Options.Host, Options.UseSsl);

                    // Set SSL protocol to 1.2 standart
                    if (Options.UseSsl)
                    {
                        ImapClient.SslProtocol = System.Security.Authentication.SslProtocols.Tls12;
                    }
                    else
                    {

                    }
                    // Connect to IMAP server
                    if (!ImapClient.IsConnected)
                        ImapClient.Connect();

                    // Login to IMAP account
                    if (ImapClient.IsConnected)
                    {
                        Dump.ToScreenAndLog($"{LogLevel.Info} {Dic.Msgs["connectedtoservertryingtologin"]}");

                        if (!ImapClient.Login(Options.Username, Options.Password))
                        {
                            Dump.ToScreenAndLog($"{LogLevel.Fail}: {Dic.Msgs["cantlogin"]}");
                            connected = false;

                        }
                        else
                        {
                            Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["loginsuccess"]}");
                            connected = true;
                        }

                    }

                }
                catch (Exception e)
                {
                    Dump.ToScreenAndLog($"{LogLevel.Error} {Dic.Msgs["cantconnectserver"]} Server: {Options.Host}  SSL_ENABLED: {Options.UseSsl}  PORT:{Options.Port}  ERROR MSG:  {e}");
                    connected = false;
                }
            }
            catch (Exception e)
            {
                Dump.ToScreenAndLog($"{LogLevel.Error} {Dic.Msgs["cantlogin"]} Server: {Options.Host}  SSL_ENABLED: {Options.UseSsl}  PORT:{Options.Port} Username: {Options.Username} ERROR MSG:{e} ");

            }
            return ImapClient;
        }

        public Message[] GetMessages(bool searchLimit, bool unseenOnly, bool searchInAllFolders)
        {
            Message[] messages = null;
            ImapClient = connectImap();
            try
            {

                if (ImapClient.IsAuthenticated)
                {
                    var inboxFolder = ImapClient.Folders.FirstOrDefault(f => f.Name == Options.ImapFolder);
                    if (!searchInAllFolders && searchLimit && unseenOnly)
                    {
                        messages = inboxFolder.Search("UNSEEN", MessageFetchMode.Full, Options.SearchLimit);
                        Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["getunseeninboxlimit"]}");
                    }
                    else if (searchInAllFolders && !searchLimit && unseenOnly)
                    {
                        messages = ImapClient.Folders.All.Search("UNSEEN", MessageFetchMode.Full);
                        Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["getunseen"]}");
                    }
                    else if (searchInAllFolders && !searchLimit && !unseenOnly)
                    {
                        messages = ImapClient.Folders.All.Search("ALL", MessageFetchMode.Full);
                        Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["getany"]}");
                    }
                    else if (!searchInAllFolders && searchLimit && !unseenOnly)
                    {
                        messages = inboxFolder.Search("ALL", MessageFetchMode.Full, Options.SearchLimit);
                        Dump.ToScreenAndLog($"{LogLevel.Info}: {Dic.Msgs["getinboxany"]}");
                    }
                    else
                    {
                        messages = inboxFolder.Search("UNSEEN", MessageFetchMode.Full, Options.SearchLimit);
                        Dump.ToScreenAndLog($"{LogLevel.Warn}: {Dic.Msgs["nooption"]}");
                    }

                }
            }
            catch (Exception e)
            {
                Dump.ToScreenAndLog($"{LogLevel.Error} {Dic.Msgs["cantgetmessages"]} Server: {Options.Host}  SSL_ENABLED: {Options.UseSsl}  PORT:{Options.Port} Username: {Options.Username} ERROR MSG:{e} ");
            }
            return messages;

        }
    }
}
   

