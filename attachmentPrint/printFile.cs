using Microsoft.Win32;
using PdfiumViewer;
using System;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Threading;

namespace attachmentPrint
{



    abstract class PrintAttachmentFile
    {
        public Options appConfiguration = new Options();
        public DumpClass Dump = new DumpClass();
        public Dict Dic = new Dict();
        public abstract bool SendToPrinter(string printer, string paperName, string filename);
    }

    class PdfFile_Pdfium : PrintAttachmentFile
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
                Dump.ToScreenAndLog($"{LogLevel.Error} {Dic.Msgs["cantprintpdf"]} {filename} ERROR MSG:  {e}");

                return false;
            }
        }
    }
    class PdfFile : PrintAttachmentFile
    {
        public override bool SendToPrinter(string printer, string paperName, string filename)
        {
            Process p = new Process();
            string uncFilename = "@" + "\"" + filename + "\"";
            p.StartInfo.Verb = "PrintTo";
            p.StartInfo.FileName = filename;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.Arguments = "\"" + printer + "\"";
            


            try
            {
                p.Start();
                Thread.Sleep(5000);
                p.CancelErrorRead();
                p.Kill();
                //File.Delete(uncFilename);
                return true;
            }
            catch (Exception e)
            {
                Dump.ToScreenAndLog($"{LogLevel.Error} {Dic.Msgs["cantprintpdf"]} {filename} ERROR MSG:  {e}");

                return false;
            }
        }
    }

    class PdfFileToDefault : PrintAttachmentFile
    {
        public override bool SendToPrinter(string printer, string paperName, string filename)
        {
            string uncFilename = "@" + "\"" + filename + "\"";
            try
            {
                Process.Start(
                   Registry.LocalMachine.OpenSubKey(
                        @"SOFTWARE\Microsoft\Windows\CurrentVersion" +
                        @"\App Paths\AcroRd32.exe").GetValue("").ToString(),
                   string.Format("/h /t \"{0}\" \"{1}\"", filename, printer));
                return true;
            }
            catch (Exception e) 
            { 
                Dump.ToScreenAndLog($"{LogLevel.Error} {Dic.Msgs["cantprintpdf"]} {filename} ERROR MSG:  {e}"); 
            }
            return false;
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
                string uncFilename = "@" + "\"" + filename + "\"";
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
                //File.Delete(uncFilename);
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

