using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PdfiumViewer;
using System.Windows;

namespace attachmentPrint
{



    abstract class PrintAttachmentFile
    {
        public Options appConfiguration = new Options();
        public DumpClass Dump = new DumpClass();
        public Dict Dic = new Dict();
        public abstract bool SendToPrinter(string printer, string paperName, string filename);
    }

    class PdfFile : PrintAttachmentFile
    {
        public override bool SendToPrinter(string printer, string paperName, string filename)
        {
            try
            {
                Dump.ToScreenAndLog($"{LogLevel.Info} {Dic.Msgs["printingpdf"]} {filename}");
                var printerSettings = new PrinterSettings();
                printerSettings.PrinterName = printer;

                var pageSettings = new PageSettings(printerSettings);
                pageSettings.Margins = new Margins(0, 0, 0, 0);

                foreach (PaperSize paperSize in printerSettings.PaperSizes)
                {
                    if (paperSize.PaperName == paperName)
                    {
                        pageSettings.PaperSize = paperSize;
                        break;
                    }
                }
                //string uncFilename = "@" + "\"" + filename + "\"";
                Console.WriteLine($"{@filename}");
                using (var document = PdfDocument.Load(@"C:\temp\Intelektualas_sist_un_tehnologijas-a6fd3d4d-d151-4f6b-89b0-79b098cd4e02-Embedded.pdf"))
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
                Dump.ToScreenAndLog($"{LogLevel.Error} {Dic.Msgs["cantprintpdf"]} {filename} ERROR MSG:  {e}");

                return false;
            }
        }
    }
    class PictureFile : PrintAttachmentFile
    {

        public override bool SendToPrinter(string printer, string paperName, string filename)
        {
            Dump.ToScreenAndLog($"{LogLevel.Info} {Dic.Msgs["printingimage"]} {filename} {printer} {paperName}");
            try
            {
                Dump.ToScreenAndLog($"{LogLevel.Info} {Dic.Msgs["printingimage"]} {filename} , {printer}");
                Process proc = new Process();
                proc.StartInfo.FileName = "rundll32.exe";
                proc.StartInfo.Arguments = @"C:\WINDOWS\system32\shimgvw.dll,ImageView_PrintTo """ + filename + @""" " + @"""" + printer + @"""";
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
                Thread.Sleep(8000);
                if (!proc.HasExited)
                {
                    proc.WaitForExit(4000);
                }
                return true;
            }
            catch (Exception e)
            {
                Dump.ToScreenAndLog($"{LogLevel.Error} {Dic.Msgs["cantprintimage"]} {filename} ERROR MSG:  {e}");
                return false;
            }

        }
    }

}

