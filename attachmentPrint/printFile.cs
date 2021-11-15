using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PdfiumViewer;

namespace attachmentPrint
{



    abstract class PrintAttachmentFile
    {
        public Options appConfiguration = new Options();
        public DumpClass Dump = new DumpClass();
        public Dict Dic = new Dict();
        public abstract bool sendToPrinter(string printer, string paperName, string filename, int copies);
    }

    class PdfFile : PrintAttachmentFile
    {
        public override bool sendToPrinter(string printer, string paperName, string filename, int copies)
        {
            try
            {
                // Create the printer settings for our printer
                var printerSettings = new PrinterSettings
                {
                    PrinterName = printer,
                    Copies = (short)copies,
                };

                // Create our page settings for the paper size selected
                var pageSettings = new PageSettings(printerSettings)
                {
                    Margins = new Margins(0, 0, 0, 0),
                };
                foreach (PaperSize paperSize in printerSettings.PaperSizes)
                {
                    if (paperSize.PaperName == paperName)
                    {
                        pageSettings.PaperSize = paperSize;
                        break;
                    }
                }

                // Now print the PDF document
                using (var document = PdfDocument.Load(filename))
                {
                    using (var printDocument = document.CreatePrintDocument())
                    {
                        printDocument.PrinterSettings = printerSettings;
                        printDocument.DefaultPageSettings = pageSettings;
                        printDocument.PrintController = new StandardPrintController();
                        printDocument.Print();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
    class PictureFile : PrintAttachmentFile
    {

        public override bool sendToPrinter(string printer, string paperName, string filename, int copies)
        {

            Process proc = new Process();
            proc.StartInfo.FileName = "rundll32.exe";
            int copyloop = copies + 1;
            int exitcode;

            try
            {
                for (int count = 1; count < copyloop; count++)
                {
                    //Console.WriteLine(string.Format("{1} Copy{0}", count, message));
                    Dump.ToScreenAndLog($"{LogLevel.Info} {Dic.Msgs["printingimage"]} {filename}  copy: {count}");
                    proc.StartInfo.Arguments = @"C:\WINDOWS\system32\shimgvw.dll,ImageView_PrintTo """ + filename + @""" " + @"""" + printer + @"""";
                    proc.StartInfo.UseShellExecute = true;
                    proc.Start();
                    proc.WaitForExit(4000);
                    exitcode = proc.ExitCode;
                    Console.WriteLine($"  Exit code : {proc.ExitCode}");
                    //Thread.Sleep(4000);
                }
                return true;
            }
            catch (Exception e)
            {
                Dump.ToScreenAndLog($"{LogLevel.Error} {Dic.Msgs["cantprintimage"]} ERROR MSG:  {e}");
                return false;
            }

        }
    }

}

