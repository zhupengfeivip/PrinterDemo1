using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo3
{
    /// <summary>
    /// 
    /// </summary>
    public class PrinterCmdUtils
    {
        /// <summary>
        /// 27(0x1B) ESC
        /// </summary>
        public const byte Esc = 27;

        /// <summary>
        /// 文本分隔符
        /// </summary>
        public const byte Fs = 28;

        /// <summary>
        /// 组分隔符
        /// </summary>
        public const byte Gs = 29;

        /// <summary>
        /// 数据连接换码
        /// </summary>
        public const byte Dle = 16;

        /// <summary>
        /// 传输结束
        /// </summary>
        public const byte Eot = 4;

        /// <summary>
        /// 询问字符
        /// </summary>
        public const byte Enq = 5;

        /// <summary>
        /// 空格
        /// </summary>
        public const byte Sp = 32;

        /// <summary>
        /// 横向列表
        /// </summary>
        public const byte Ht = 9;

        /// <summary>
        /// 打印并换行（水平定位）
        /// </summary>
        public const byte Lf = 10;

        /// <summary>
        /// 归位键
        /// </summary>
        public const byte Cr = 13;

        /// <summary>
        /// 走纸控制（打印并回到标准模式（在页模式下） ）
        /// </summary>
        public const byte Ff = 12;

        /// <summary>
        /// 作废（页模式下取消打印数据 
        /// </summary>
        public const byte Can = 24;

        /// <summary>
        /// 打印纸一行最大的字节
        /// </summary>
        private const int LINE_BYTE_SIZE = 32;

        #region ESC @初始化打印机
        /// <summary>
        /// ESC @初始化打印机
        /// </summary>
        /// <returns></returns>
        public static byte[] InitPrinter()
        {
            byte[] result = new byte[2];
            result[0] = Esc;
            result[1] = 64;//@
            return result;
        }
        #endregion

        #region ESC p m t1 t2产生钱箱控制脉冲
        /// <summary>
        /// ESC p m t1 t2产生钱箱控制脉冲
        /// </summary>
        /// <returns></returns>
        public static byte[] OpenMoneyBox()
        {
            byte[] result = new byte[5];
            result[0] = Esc;
            result[1] = 112;
            result[2] = 48;
            result[3] = 64;
            result[4] = 0;
            return result;
        }
        #endregion

        #region LF 换行
        /// <summary>
        /// LF 换行
        /// </summary>
        /// <param name="lineNum"></param>
        /// <returns></returns>
        public static byte[] NextLine(byte lineNum)
        {
            byte[] result = new byte[lineNum];
            for (int i = 0; i < lineNum; i++)
            {
                result[i] = Lf;
            }
            return result;
        }
        #endregion

        #region ESC d n打印并向前走纸N行
        /// <summary>
        /// ESC d n打印并向前走纸N行
        /// </summary>
        /// <param name="lineNum"></param>
        /// <returns></returns>
        public static byte[] NextLineNum(byte lineNum)
        {
            byte[] result = new byte[3];
            result[0] = Esc;
            result[1] = 100;
            result[2] = lineNum;
            return result;
        }
        #endregion


        #region ESC - n选择/取消下划线模式

        /// <summary>
        /// 取消绘制下划线
        /// </summary>
        /// <returns></returns>
        public static byte[] UnderlineOff()
        {
            byte[] result = new byte[3];
            result[0] = Esc;
            result[1] = 45;
            result[2] = 0;
            return result;
        }

        /// <summary>
        /// 绘制下划线（1点宽）
        /// </summary>
        /// <returns></returns>
        public static byte[] UnderlineWithOneDotWidthOn()
        {
            byte[] result = new byte[3];
            result[0] = Esc;
            result[1] = 45;
            result[2] = 1;
            return result;
        }

        /// <summary>
        /// 绘制下划线（2点宽）
        /// </summary>
        /// <returns></returns>
        public static byte[] UnderlineWithTwoDotWidthOn()
        {
            byte[] result = new byte[3];
            result[0] = Esc;
            result[1] = 45;
            result[2] = 2;
            return result;
        }

        #endregion

        #region ESC E n选择或者取消加粗模式

        /// <summary>
        /// ESC E n选择或者取消加粗模式
        /// </summary>
        /// <returns></returns>
        public static byte[] IfBold(bool bold)
        {
            byte[] result = new byte[3];
            result[0] = Esc;
            result[1] = 69;
            if (bold)
            {
                //选择加粗模式
                result[2] = 0xF;
            }
            else
            {
                //取消加粗模式
                result[2] = 0;
            }
            return result;
        }

        #endregion


        #region 14、ESC D n1..nk NUL设置横向跳格位置

        /// <summary>
        /// 14、ESC D n1..nk NUL设置横向跳格位置
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static byte[] set_HT_position(byte col)
        {
            byte[] result = new byte[4];
            result[0] = Esc;
            result[1] = 68;
            result[2] = col;
            result[3] = 0;
            return result;
        }

        #endregion

        #region 22、ESC a n选择对齐方式

        /// <summary>
        /// ESC a n选择对齐方式-左对齐
        /// </summary>
        /// <returns></returns>
        public static byte[] AlignLeft()
        {
            byte[] result = new byte[3];
            result[0] = Esc;
            result[1] = 97;
            result[2] = 0;
            return result;
        }

        /// <summary>
        /// ESC a n选择对齐方式-居中对齐
        /// </summary>
        /// <returns></returns>
        public static byte[] AlignCenter()
        {
            byte[] result = new byte[3];
            result[0] = Esc;
            result[1] = 97;
            result[2] = 1;
            return result;
        }

        /// <summary>
        /// ESC a n选择对齐方式-右对齐
        /// </summary>
        /// <returns></returns>
        public static byte[] AlignRight()
        {
            byte[] result = new byte[3];
            result[0] = Esc;
            result[1] = 97;
            result[2] = 2;
            return result;
        }

        #endregion

        ///// <summary>
        ///// 字体变大为标准的n倍
        ///// </summary>
        ///// <param name="num"></param>
        ///// <returns></returns>
        //public static byte[] FontSizeSetBig(int num)
        //{
        //    byte realSize = 0;
        //    switch (num)
        //    {
        //        case 1:
        //            realSize = 0;
        //            break;
        //        case 2:
        //            realSize = 17;
        //            break;
        //        case 3:
        //            realSize = 34;
        //            break;
        //        case 4:
        //            realSize = 51;
        //            break;
        //        case 5:
        //            realSize = 68;
        //            break;
        //        case 6:
        //            realSize = 85;
        //            break;
        //        case 7:
        //            realSize = 102;
        //            break;
        //        case 8:
        //            realSize = 119;
        //            break;
        //    }
        //    byte[] result = new byte[3];
        //    result[0] = 29;
        //    result[1] = 33;
        //    result[2] = realSize;
        //    return result;
        //}


        /// <summary>
        /// 4、ESC ! n 选择打印模式-字体取消倍宽倍高
        /// </summary>
        /// <returns></returns>
        public static byte[] FontSizeSetSmall()
        {
            byte[] result = new byte[3];
            result[0] = Esc;
            result[1] = 33;
            result[2] = 0;
            return result;
        }

        #region GS V m n裁纸

        /// <summary>
        /// 进纸并全部切割
        /// </summary>
        /// <returns></returns>
        public static byte[] FeedPaperCutAll()
        {
            byte[] result = new byte[4];
            result[0] = Gs;
            result[1] = 86;
            result[2] = 65;
            result[3] = 0;
            return result;
        }

        /// <summary>
        /// 进纸并切割（左边留一点不切）
        /// </summary>
        /// <returns></returns>
        public static byte[] FeedPaperCutPartial()
        {
            byte[] result = new byte[4];
            result[0] = Gs;
            result[1] = 86;
            result[2] = 66;
            result[3] = 0;
            return result;
        }

        #endregion

        #region ESC 3 n设置行间距
        /// <summary>
        /// ESC 3 n设置行间距
        /// </summary>
        /// <returns></returns>
        public static byte[] SetLineSpacing(byte lineSpace)
        {
            return new byte[] { Esc, 0x33, lineSpace };
        }
        #endregion

        #region ESC J n 打印并走纸
        /// <summary>
        /// ESC J n 打印并走纸
        /// </summary>
        /// <returns></returns>
        public static byte[] PrintGoPaper(byte goCount)
        {
            return new byte[] { 0x1B, 0x4A, goCount };
        }
        #endregion

        #region 41、GS k m d1..dk NUL打印条码
        /// <summary>
        /// 41、GS k m d1..dk NUL打印条码
        /// </summary>
        /// <returns></returns>
        public static byte[] PrintScanCode(string code)
        {
            byte[] result = new byte[14];
            result[0] = Gs;
            result[1] = 107;//k
            result[2] = 73;//m 条码类型
            result[3] = 10;
            result[4] = 123;
            result[5] = 66;
            result[6] = 78;
            result[7] = 111;
            result[8] = 46;
            result[9] = 123;
            result[10] = 67;
            result[11] = 12;
            result[12] = 34;
            result[13] = 56;
            return result;
        }

        /// <summary>
        /// 41、GS k m d1..dk NUL打印条码
        /// CODE128A: 标准数字和大写字母, 控制符, 特殊字符
        /// CODE128B: 标准数字和大写字母, 小写字母, 特殊字符
        /// CODE128C: [00]-[99]的数字对集合, 共100个(只能包含数字，一个条码字符代表两位数字）
        /// </summary>
        /// <returns></returns>
        public static byte[] PrintScanCode128OfA(string code)
        {
            //条码编码
            byte[] bytes = Encoding.GetEncoding("utf8").GetBytes(code.ToUpper());

            byte[] result = new byte[bytes.Length + 2 + 4];
            result[0] = Gs;
            result[1] = 107;//k
            result[2] = 73;//m 条码类型 73=CODE128
            result[3] = Convert.ToByte(bytes.Length + 2);//n，从n之后字节数
            //CODEB:{A
            result[4] = 123;
            result[5] = 65;
            //条码编码
            for (int i = 0; i < bytes.Length; i++)
            {
                result[6 + i] = bytes[i];
            }
            return result;
        }


        /// <summary>
        /// 41、GS k m d1..dk NUL打印条码
        /// CODE128A: 标准数字和大写字母, 控制符, 特殊字符
        /// CODE128B: 标准数字和大写字母, 小写字母, 特殊字符
        /// CODE128C: [00]-[99]的数字对集合, 共100个(只能包含数字，一个条码字符代表两位数字）
        /// </summary>
        /// <returns></returns>
        public static byte[] PrintScanCode128OfB(string code)
        {
            //条码编码
            byte[] bytes = Encoding.GetEncoding("utf8").GetBytes(code);

            byte[] result = new byte[bytes.Length + 2 + 4];
            result[0] = Gs;
            result[1] = 107;//k
            result[2] = 73;//m 条码类型 73=CODE128
            result[3] = Convert.ToByte(bytes.Length + 2);//n，从n之后字节数
            //CODEB:{B
            result[4] = 123;
            result[5] = 66;
            //条码编码
            for (int i = 0; i < bytes.Length; i++)
            {
                result[6 + i] = bytes[i];
            }
            return result;
        }

        /// <summary>
        /// 41、GS k m d1..dk NUL打印条码
        /// CODE128A: 标准数字和大写字母, 控制符, 特殊字符
        /// CODE128B: 标准数字和大写字母, 小写字母, 特殊字符
        /// CODE128C: [00]-[99]的数字对集合, 共100个(只能包含数字，一个条码字符代表两位数字）
        /// </summary>
        /// <returns></returns>
        public static byte[] PrintScanCode128OfC(string code)
        {
            //条码编码
            byte[] bytes = Encoding.GetEncoding("utf8").GetBytes(code);

            byte[] result = new byte[bytes.Length + 2 + 4];
            result[0] = Gs;
            result[1] = 107;//k
            result[2] = 73;//m 条码类型 73=CODE128
            result[3] = Convert.ToByte(bytes.Length + 2);//n，从n之后字节数
            //CODEB:{C
            result[4] = 123;
            result[5] = 67;
            //条码编码
            for (int i = 0; i < bytes.Length; i++)
            {
                result[6 + i] = bytes[i];
            }
            return result;
        }

        #endregion


        #region 私有方法



        /// <summary>
        /// 将bmp图片转换为字节
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static byte[] BmpToByte(Bitmap bmp)
        {
            int h = bmp.Height / 24;//24位双密度，每次打印高度是3字节，24个像素，/ 24 表示循环多少次
            if (bmp.Height % 24 > 0)
                h = h + 1;//不是24的整数位，所以要多循环一次
            int w = bmp.Width;
            List<byte[]> all = new List<byte[]>();

            // ESC * m nL nH 选择位图模式  
            byte m = 33;//24点双密度
            int nL = w % 256;//0`255
            int nH = w / 256;//0`3
            byte[] escBmp = { Esc, 0x2A, m, (byte)nL, (byte)nH };

            //循环进行打印，一次循环24个像素，相当于一次搬三层  
            for (int i = 0; i < h; i++)
            {
                all.Add(escBmp);
                for (int j = 0; j < w; j++)
                {
                    byte[] data = { 0x00, 0x00, 0x00 };//纵向的24个像素
                    for (int k = 0; k < 24; k++)
                    {
                        if (i * 24 + k < bmp.Height)
                        {
                            //获取某个像素的颜色值,ARGB,RGB=0表示黑色，255表示白色
                            Color pixelColor = bmp.GetPixel(j, i * 24 + k);
                            if (pixelColor.R == 0)//如果是黑色则打印 0表示黑色 255表示白色
                            {
                                //d各位为1,值255则打印，值为0则不打印
                                data[k / 8] += (byte)(128 >> (k % 8));
                            }
                        }
                    }
                    //d1..dk
                    all.Add(data);
                }
                //换行  
                all.Add(NextLine(1));
            }
            return ByteMerger(all);
        }


        /// <summary>
        /// 字节数组合并
        /// </summary>
        /// <param name="byte_1"></param>
        /// <param name="byte_2"></param>
        /// <returns></returns>
        public static byte[] ByteMerger(byte[] byte_1, byte[] byte_2)
        {
            byte[] byte_3 = new byte[byte_1.Length + byte_2.Length];
            System.Array.Copy(byte_1, 0, byte_3, 0, byte_1.Length);
            System.Array.Copy(byte_2, 0, byte_3, byte_1.Length, byte_2.Length);
            return byte_3;
        }

        /// <summary>
        /// 字节数组合并
        /// </summary>
        /// <param name="byteList"></param>
        /// <returns></returns>
        public static byte[] ByteMerger(byte[][] byteList)
        {
            int Length = 0;
            for (int i = 0; i < byteList.Length; i++)
            {
                Length += byteList[i].Length;
            }
            byte[] result = new byte[Length];

            int index = 0;
            for (int i = 0; i < byteList.Length; i++)
            {
                byte[] nowByte = byteList[i];
                for (int k = 0; k < byteList[i].Length; k++)
                {
                    result[index] = nowByte[k];
                    index++;
                }
            }
            return result;
        }

        /// <summary>
        /// 字节数组合并
        /// </summary>
        /// <param name="byteList"></param>
        /// <returns></returns>
        public static byte[] ByteMerger(List<byte[]> byteList)
        {
            int Length = 0;
            for (int i = 0; i < byteList.Count; i++)
            {
                Length += byteList[i].Length;
            }
            byte[] result = new byte[Length];

            int index = 0;
            for (int i = 0; i < byteList.Count; i++)
            {
                byte[] nowByte = byteList[i];
                for (int k = 0; k < byteList[i].Length; k++)
                {
                    result[index] = nowByte[k];
                    index++;
                }
            }
            return result;
        }

        /// <summary>
        /// 字节数组拆分，每20个字节一数组
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[][] Byte20Split(byte[] bytes)
        {
            int size = bytes.Length / 10 + 1;
            byte[][] result = new byte[size][];
            for (int i = 0; i < size; i++)
            {
                byte[] by = new byte[((i + 1) * 10) - (i * 10)];
                //从bytes中的第 i * 10 个位置到第 (i + 1) * 10 个位置;
                System.Array.Copy(bytes, i * 10, by, 0, (i + 1) * 10);
                result[i] = by;
            }
            return result;
        }

        #endregion
    }
}
