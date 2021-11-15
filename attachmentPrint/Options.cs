using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace attachmentPrint
{
    public class Options
    {
        [Option('m', "mode", Required = false, HelpText = "Set mode, 1 = print unseen from inbox, 2 = print all from inbox, 3 = print all, 4 = print all unseen ")]
        public int Mode { get; set; } = 1;
        [Option('t', "host", Required = true, HelpText = "Set imap hostname.")]
        public string Host { get; set; } = "imap.gmail.com";
        [Option('f', "imapfolder", Required = false, HelpText = "Set imap folder to search for attachments")]
        public string ImapFolder { get; set; } = "INBOX";
        [Option('x', "processedfolder", Required = false, HelpText = "Where to move processed messages, works if set")]
        public string ProcessedFoler { get; set; } = "IZPRINTETIE";
        [Option('y', "port", Required = false, HelpText = "Set IMAP port.")]
        public int Port { get; set; } = 587;
        [Option('l', "limit", Required = false, HelpText = "Set limit for messages to be searched.")]
        public int SearchLimit { get; set; } = 100;
        [Option('s', "ssl", Required = false, HelpText = "Use ssl?, true, false")]
        public bool UseSsl { get; set; } = true;
        [Option('u', "username", Required = true, HelpText = "Imap username.")]
        public string Username { get; set; } = "attachementprintcsharp@gmail.com";
        [Option('p', "password", Required = false, HelpText = "Imap password.")]
        public string Password { get; set; } = "Latvija2021";
        [Option('q', "markread", Required = true, HelpText = "Mark as read? true, false.")]
        public bool MarkAsRead { get; set; } = true;
        [Option('d', "directory", Required = false, HelpText = "where to store files?. please use following syntax: @\"C:\\temp\" ")]
        public string Dir { get; set; }  = @"C:\temp";
        [Option('o', "loglocation", Required = false, HelpText = "Set output to verbose messages.")]
        public string LogLocation { get; set; } = @"C:\temp\attachmentprint.log";
        [Option('j', "filetoprint", Required = false, HelpText = "Set output to verbose messages.")]
        public string[] FileTypesToPrint { get; set; } = { "PDF", "pdf", "jpg", "JPEG" };
        [Option('e', "exclude", Required = false, HelpText = "what file types to exclude?")]
        public string[] ExcludeFileNames { get; set; } = { "emot", "smile", "SMILE", "Smile", "Emot", "EMOT" };
        [Option('a', "printer", Required = false, HelpText = "Full name of printer.")]
        public string DefaultPrinter { get; set; } = "Microsoft Print to PDF";
        [Option('n', "pdfextensions", Required = false, HelpText = "List of PDF extensions to print.")]
        public string[] PdfExtensions { get; set; } = { "PDF", "pdf" };
        [Option('z', "pictureextensions", Required = false, HelpText = "List of picture extensions.")]
        public string[] PictureExtensions { get; set; } = { "JPG", "jpg", "Jpg", "Jpeg", "jpeg", "JPEG", "BMP", "PNG", "png", "bmp" };


    }
}

/*
Paper sizes
A2	66	
A2 paper (420 mm by 594 mm).

A3	8	
A3 paper (297 mm by 420 mm).

A3Extra	63	
A3 extra paper (322 mm by 445 mm).

A3ExtraTransverse	68	
A3 extra transverse paper (322 mm by 445 mm).

A3Rotated	76	
A3 rotated paper (420 mm by 297 mm).

A3Transverse	67	
A3 transverse paper (297 mm by 420 mm).

A4	9	
A4 paper (210 mm by 297 mm).

A4Extra	53	
A4 extra paper (236 mm by 322 mm). This value is specific to the PostScript driver and is used only by Linotronic printers to help save paper.

A4Plus	60	
A4 plus paper (210 mm by 330 mm).

A4Rotated	77	
A4 rotated paper (297 mm by 210 mm). Requires Windows NT 4.0 or later.

A4Small	10	
A4 small paper (210 mm by 297 mm).

A4Transverse	55	
A4 transverse paper (210 mm by 297 mm).

A5	11	
A5 paper (148 mm by 210 mm).

A5Extra	64	
A5 extra paper (174 mm by 235 mm).

A5Rotated	78	
A5 rotated paper (210 mm by 148 mm).

A5Transverse	61	
A5 transverse paper (148 mm by 210 mm).

A6	70	
A6 paper (105 mm by 148 mm). Requires Windows NT 4.0 or later.

A6Rotated	83	
A6 rotated paper (148 mm by 105 mm). Requires Windows NT 4.0 or later.

APlus	57	
SuperA/SuperA/A4 paper (227 mm by 356 mm).

B4	12	
B4 paper (250 mm by 353 mm).

B4Envelope	33	
B4 envelope (250 mm by 353 mm).

B4JisRotated	79	
JIS B4 rotated paper (364 mm by 257 mm). Requires Windows NT 4.0 or later.

B5	13	
B5 paper (176 mm by 250 mm).

B5Envelope	34	
B5 envelope (176 mm by 250 mm).

B5Extra	65	
ISO B5 extra paper (201 mm by 276 mm).

B5JisRotated	80	
JIS B5 rotated paper (257 mm by 182 mm). Requires Windows NT 4.0 or later.

B5Transverse	62	
JIS B5 transverse paper (182 mm by 257 mm).

B6Envelope	35	
B6 envelope (176 mm by 125 mm).

B6Jis	88	
JIS B6 paper (128 mm by 182 mm). Requires Windows NT 4.0 or later.

B6JisRotated	89	
JIS B6 rotated paper (182 mm by 128 mm). Requires Windows NT 4.0 or later.

BPlus	58	
SuperB/SuperB/A3 paper (305 mm by 487 mm).

C3Envelope	29	
C3 envelope (324 mm by 458 mm).

C4Envelope	30	
C4 envelope (229 mm by 324 mm).

C5Envelope	28	
C5 envelope (162 mm by 229 mm).

C65Envelope	32	
C65 envelope (114 mm by 229 mm).

C6Envelope	31	
C6 envelope (114 mm by 162 mm).

CSheet	24	
C paper (17 in. by 22 in.).

Custom	0	
The paper size is defined by the user.

DLEnvelope	27	
DL envelope (110 mm by 220 mm).

DSheet	25	
D paper (22 in. by 34 in.).

ESheet	26	
E paper (34 in. by 44 in.).

Executive	7	
Executive paper (7.25 in. by 10.5 in.).

Folio	14	
Folio paper (8.5 in. by 13 in.).

GermanLegalFanfold	41	
German legal fanfold (8.5 in. by 13 in.).

GermanStandardFanfold	40	
German standard fanfold (8.5 in. by 12 in.).

InviteEnvelope	47	
Invitation envelope (220 mm by 220 mm).

IsoB4	42	
ISO B4 (250 mm by 353 mm).

ItalyEnvelope	36	
Italy envelope (110 mm by 230 mm).

JapaneseDoublePostcard	69	
Japanese double postcard (200 mm by 148 mm). Requires Windows NT 4.0 or later.

JapaneseDoublePostcardRotated	82	
Japanese rotated double postcard (148 mm by 200 mm). Requires Windows NT 4.0 or later.

JapaneseEnvelopeChouNumber3	73	
Japanese Chou #3 envelope. Requires Windows NT 4.0 or later.

JapaneseEnvelopeChouNumber3Rotated	86	
Japanese rotated Chou #3 envelope. Requires Windows NT 4.0 or later.

JapaneseEnvelopeChouNumber4	74	
Japanese Chou #4 envelope. Requires Windows NT 4.0 or later.

JapaneseEnvelopeChouNumber4Rotated	87	
Japanese rotated Chou #4 envelope. Requires Windows NT 4.0 or later.

JapaneseEnvelopeKakuNumber2	71	
Japanese Kaku #2 envelope. Requires Windows NT 4.0 or later.

JapaneseEnvelopeKakuNumber2Rotated	84	
Japanese rotated Kaku #2 envelope. Requires Windows NT 4.0 or later.

JapaneseEnvelopeKakuNumber3	72	
Japanese Kaku #3 envelope. Requires Windows NT 4.0 or later.

JapaneseEnvelopeKakuNumber3Rotated	85	
Japanese rotated Kaku #3 envelope. Requires Windows NT 4.0 or later.

JapaneseEnvelopeYouNumber4	91	
Japanese You #4 envelope. Requires Windows NT 4.0 or later.

JapaneseEnvelopeYouNumber4Rotated	92	
Japanese You #4 rotated envelope. Requires Windows NT 4.0 or later.

JapanesePostcard	43	
Japanese postcard (100 mm by 148 mm).

JapanesePostcardRotated	81	
Japanese rotated postcard (148 mm by 100 mm). Requires Windows NT 4.0 or later.

Ledger	4	
Ledger paper (17 in. by 11 in.).

Legal	5	
Legal paper (8.5 in. by 14 in.).

LegalExtra	51	
Legal extra paper (9.275 in. by 15 in.). This value is specific to the PostScript driver and is used only by Linotronic printers in order to conserve paper.

Letter	1	
Letter paper (8.5 in. by 11 in.).

LetterExtra	50	
Letter extra paper (9.275 in. by 12 in.). This value is specific to the PostScript driver and is used only by Linotronic printers in order to conserve paper.

LetterExtraTransverse	56	
Letter extra transverse paper (9.275 in. by 12 in.).

LetterPlus	59	
Letter plus paper (8.5 in. by 12.69 in.).

LetterRotated	75	
Letter rotated paper (11 in. by 8.5 in.).

LetterSmall	2	
Letter small paper (8.5 in. by 11 in.).

LetterTransverse	54	
Letter transverse paper (8.275 in. by 11 in.).

MonarchEnvelope	37	
Monarch envelope (3.875 in. by 7.5 in.).

Note	18	
Note paper (8.5 in. by 11 in.).

Number10Envelope	20	
#10 envelope (4.125 in. by 9.5 in.).

Number11Envelope	21	
#11 envelope (4.5 in. by 10.375 in.).

Number12Envelope	22	
#12 envelope (4.75 in. by 11 in.).

Number14Envelope	23	
#14 envelope (5 in. by 11.5 in.).

Number9Envelope	19	
#9 envelope (3.875 in. by 8.875 in.).

PersonalEnvelope	38	
6 3/4 envelope (3.625 in. by 6.5 in.).

Prc16K	93	
16K paper (146 mm by 215 mm). Requires Windows NT 4.0 or later.

Prc16KRotated	106	
16K rotated paper (146 mm by 215 mm). Requires Windows NT 4.0 or later.

Prc32K	94	
32K paper (97 mm by 151 mm). Requires Windows NT 4.0 or later.

Prc32KBig	95	
32K big paper (97 mm by 151 mm). Requires Windows NT 4.0 or later.

Prc32KBigRotated	108	
32K big rotated paper (97 mm by 151 mm). Requires Windows NT 4.0 or later.

Prc32KRotated	107	
32K rotated paper (97 mm by 151 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber1	96	
#1 envelope (102 mm by 165 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber10	105	
#10 envelope (324 mm by 458 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber10Rotated	118	
#10 rotated envelope (458 mm by 324 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber1Rotated	109	
#1 rotated envelope (165 mm by 102 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber2	97	
#2 envelope (102 mm by 176 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber2Rotated	110	
#2 rotated envelope (176 mm by 102 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber3	98	
#3 envelope (125 mm by 176 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber3Rotated	111	
#3 rotated envelope (176 mm by 125 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber4	99	
#4 envelope (110 mm by 208 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber4Rotated	112	
#4 rotated envelope (208 mm by 110 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber5	100	
#5 envelope (110 mm by 220 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber5Rotated	113	
Envelope #5 rotated envelope (220 mm by 110 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber6	101	
#6 envelope (120 mm by 230 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber6Rotated	114	
#6 rotated envelope (230 mm by 120 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber7	102	
#7 envelope (160 mm by 230 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber7Rotated	115	
#7 rotated envelope (230 mm by 160 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber8	103	
#8 envelope (120 mm by 309 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber8Rotated	116	
#8 rotated envelope (309 mm by 120 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber9	104	
#9 envelope (229 mm by 324 mm). Requires Windows NT 4.0 or later.

PrcEnvelopeNumber9Rotated	117	
#9 rotated envelope (324 mm by 229 mm). Requires Windows NT 4.0 or later.

Quarto	15	
Quarto paper (215 mm by 275 mm).

Standard10x11	45	
Standard paper (10 in. by 11 in.).

Standard10x14	16	
Standard paper (10 in. by 14 in.).

Standard11x17	17	
Standard paper (11 in. by 17 in.).

Standard12x11	90	
Standard paper (12 in. by 11 in.). Requires Windows NT 4.0 or later.

Standard15x11	46	
Standard paper (15 in. by 11 in.).

Standard9x11	44	
Standard paper (9 in. by 11 in.).

Statement	6	
Statement paper (5.5 in. by 8.5 in.).

Tabloid	3	
Tabloid paper (11 in. by 17 in.).

TabloidExtra	52	
Tabloid extra paper (11.69 in. by 18 in.). This value is specific to the PostScript driver and is used only by Linotronic printers in order to conserve paper.

USStandardFanfold	39	
US standard fanfold (14.875 in. by 11 in.).
*/
