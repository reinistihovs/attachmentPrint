using System;
using ImapX;




namespace attachmentPrint
{
    class Program
    {

        static void Main(string[] args)
        {
            var appConfiguration = new appConfiguration();
            var getAttachments2 = new getAttachments2();
            Console.WriteLine("Please choose action:");
            Console.WriteLine("1) Print all attachments in INBOX");
            Console.WriteLine("2) Print Unread attachments in INBOX and mark as read");
            Console.WriteLine("3) Print unregistered attachment and register email ID in database");
            Console.WriteLine("press any other key to EXIT");
            //int Choice = 0;
            int Action = int.Parse(Console.ReadLine());
            //if (Action != 1 || Action != 2 || Action != 3)
            //{
            //    return;

            //}
            
            //if (Action == 1)
           // {
                getAttachments2.downloadAllAttachments();

           // }

        }
    }
}
