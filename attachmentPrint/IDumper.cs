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
    public interface IToLogOnly
    {

        void ToLogOnly(string text);
    }

    public interface IToScreenAndLog
    {
        void ToScreenAndLog(string text);
    }


    public class DumpClass : IToLogOnly, IToScreenAndLog
    {

        public void ToLogOnly(string text)
        {
            var appConfiguration = new Options();
            File.AppendAllText(appConfiguration.LogLocation, (DateTime.Now + "  " + text + Environment.NewLine));
        }

        public void ToScreenAndLog(string text)
        {
            Console.WriteLine(DateTime.Now + "  " + text);
            ToLogOnly(text);
        }
    }

}
