using System;




namespace attachmentPrint
{
    class Program
    {

        static void Main(string[] args)
        {
            DumpClass Dump = new DumpClass();
            Dict Dic = new Dict();
            var appConfiguration = new appConfiguration();
            var getAttachments = new getAttachments();
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


            Console.WriteLine("Please choose action:");
            Console.WriteLine("1)  to download all unseen attachments from INBOX and move messages to Processed folder");
            Console.WriteLine("2)  to download all unseen attachments from INBOX and mark as read");

            //Console.WriteLine("3) Print unregistered attachment and register email ID in database");
            Console.WriteLine("press any other key to EXIT");
            //int Choice = 0;
            int Action = int.Parse(Console.ReadLine());
            if (Action == 1 )
            {
                Dump.ToScreenAndLog($"{LogLevel.Info}: Selecttion: 1 ");
                getAttachments.downloadAllUnseenAttchments();
            }
            else if (Action == 2)
            {
                getAttachments.downloadAllUnreadAttchments();
            }
            else
            {
                Dump.ToScreenAndLog($"{LogLevel.Warn}: Invalid action selected press ENTER to exit");
                while (Console.ReadKey().Key != ConsoleKey.Enter) {}
                return;
            }
            

        }
    }

}
