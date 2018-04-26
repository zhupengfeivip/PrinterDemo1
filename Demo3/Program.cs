using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo3
{
    class Program
    {
        static byte[] b_init = PrinterCmdUtils.InitPrinter();
        static byte[] b_nextLine = PrinterCmdUtils.NextLineNum(1);
        static byte[] b_next2Line = PrinterCmdUtils.NextLineNum(2);
        static byte[] b_next3Line = PrinterCmdUtils.NextLineNum(3);
        static byte[] b_center = PrinterCmdUtils.AlignCenter();
        static byte[] b_left = PrinterCmdUtils.AlignLeft();
        static byte[] b_breakPartial = PrinterCmdUtils.FeedPaperCutPartial();

        static void Main(string[] args)
        {
            Test1();
        }
        static void Test1()
        {
            RawPrinterHelper print = new RawPrinterHelper("XP-58");
            //new RawPrinterHelper("XP-58").OpenMoneyBox();
            byte[] bytes = new byte[10];
            bytes[0] = 10;
            byte[] b_way = Encoding.GetEncoding("gbk").GetBytes("你好");
            byte[] line = Encoding.GetEncoding("gbk").GetBytes("__________");
            byte[] white = Encoding.GetEncoding("gbk").GetBytes("       ");
            int index = 0;
            bytes[index++] = 27;
            bytes[index++] = 64;

            bytes[index++] = b_way[0];
            bytes[index++] = b_way[1];
            bytes[index++] = b_way[2];
            bytes[index++] = b_way[3];
            //bytes[index++] = b_way[4];

            bytes[index++] = 10;



            //Bitmap bitmap = new Bitmap("XprintLogo.bmp");
            Bitmap bitmap = new Bitmap("24像素测试.bmp");
            //Bitmap bitmap = new Bitmap("logo.bmp");
            //Bitmap bitmap = new Bitmap("XinYe.bmp");
            byte[] logBytes = PrinterCmdUtils.BmpToByte(bitmap);

            byte[][] charge_0 = {
                PrinterCmdUtils.InitPrinter(),
                PrinterCmdUtils.AlignCenter(), b_way,  b_next2Line,
                b_left,
                line,     b_nextLine,
                line,     b_next2Line,
                line,     b_next3Line,
                b_way,  b_nextLine,
                b_way,    b_nextLine,
                b_way,   b_nextLine,
                b_way,     b_nextLine,
                PrinterCmdUtils.SetLineSpacing(0),
                PrinterCmdUtils.AlignCenter(),
                logBytes, b_next2Line,
                b_left,
                b_next3Line,
                b_way,     b_nextLine,
                b_way,     b_nextLine,
                b_way,     b_nextLine,
                logBytes, b_next2Line,
                b_next3Line,
                b_next3Line,
                b_next3Line,
                b_next3Line,
                b_next3Line,
                b_next3Line,
                PrinterCmdUtils.PrintGoPaper(3),b_nextLine,
                white,b_nextLine,
                white,b_nextLine,
                white,b_nextLine,
                //PrinterCmdUtils.open_money()
            };
            bytes = PrinterCmdUtils.ByteMerger(charge_0);
            int dwError;
            bool operResult = print.SendBytesToPrinter(bytes, out dwError);
            Console.WriteLine(operResult);
            Console.WriteLine(dwError);
            Console.ReadLine();
        }

        static void Test2()
        {
            RawPrinterHelper print = new RawPrinterHelper("XP-58");
            //new RawPrinterHelper("XP-58").OpenMoneyBox();
            byte[] bytes = new byte[10];
            bytes[0] = 10;
            byte[] b_way = Encoding.GetEncoding("gbk").GetBytes("No.123456");
            byte[] line = Encoding.GetEncoding("gbk").GetBytes("__________");
            byte[] white = Encoding.GetEncoding("gbk").GetBytes("       ");
            int index = 0;
            bytes[index++] = 27;
            bytes[index++] = 64;

            bytes[index++] = b_way[0];
            bytes[index++] = b_way[1];
            bytes[index++] = b_way[2];
            bytes[index++] = b_way[3];
            //bytes[index++] = b_way[4];

            bytes[index++] = 10;



            //Bitmap bitmap = new Bitmap("XprintLogo.bmp");
            //Bitmap bitmap = new Bitmap("tail_image.bmp");
            //Bitmap bitmap = new Bitmap("logo.bmp");
            Bitmap bitmap = new Bitmap("XinYe.bmp");
            byte[] logBytes = PrinterCmdUtils.BmpToByte(bitmap);

            byte[][] charge_0 = {
                PrinterCmdUtils.InitPrinter(),
                line,     b_nextLine,
                PrinterCmdUtils.PrintScanCode(""),
                b_nextLine,
                b_center,
                b_way, b_nextLine,
            };
            bytes = PrinterCmdUtils.ByteMerger(charge_0);
            int dwError;
            bool operResult = print.SendBytesToPrinter(bytes, out dwError);
            Console.WriteLine(operResult);
            Console.WriteLine(dwError);
            Console.ReadLine();
        }

        static void Test3()
        {
            RawPrinterHelper print = new RawPrinterHelper("XP-58");
            //new RawPrinterHelper("XP-58").OpenMoneyBox();
            byte[] bytes = new byte[10];
            bytes[0] = 10;
            byte[] b_way = Encoding.GetEncoding("gbk").GetBytes("No.123456");
            byte[] line = Encoding.GetEncoding("gbk").GetBytes("__________");
            byte[] white = Encoding.GetEncoding("gbk").GetBytes("       ");
            int index = 0;
            bytes[index++] = 27;
            bytes[index++] = 64;

            bytes[index++] = b_way[0];
            bytes[index++] = b_way[1];
            bytes[index++] = b_way[2];
            bytes[index++] = b_way[3];
            //bytes[index++] = b_way[4];

            bytes[index++] = 10;



            //Bitmap bitmap = new Bitmap("XprintLogo.bmp");
            //Bitmap bitmap = new Bitmap("tail_image.bmp");
            //Bitmap bitmap = new Bitmap("logo.bmp");
            //Bitmap bitmap = new Bitmap("XinYe.bmp");
            //byte[] logBytes = PrinterCmdUtils.BmpToByte(bitmap);
            List<byte[]> all = new List<byte[]>();
            all.Add(PrinterCmdUtils.InitPrinter());
            all.Add(b_center);
            all.Add(b_way);
            all.Add(b_nextLine);

            bytes = PrinterCmdUtils.ByteMerger(all);
            int dwError;
            bool operResult = print.SendBytesToPrinter(bytes, out dwError);
            Console.WriteLine(operResult);
            Console.WriteLine(dwError);
            Console.ReadLine();
        }
    }
}
