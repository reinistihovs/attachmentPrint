using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attachmentPrint
{

    public enum LogLevel
    {
        Info,
        Warn,
        Debug,
        Error,
        Fail
    }
    public interface IDumpToLog
    {

        void dumpToLog(string text);
    }

    public interface IDumpToScreen
    {
        void dumpToScreen(string text);
    }


    public class DumpClass : IDumpToLog, IDumpToScreen
    {

        public void dumpToLog(string text)
        {
            var appConfiguration = new appConfiguration();
            File.AppendAllText(appConfiguration.LogLocation, (DateTime.Now + "  " + text));
        }

        public void dumpToScreen(string text)
        {
            Console.WriteLine(DateTime.Now + "  " + text);
        }
    }

}
