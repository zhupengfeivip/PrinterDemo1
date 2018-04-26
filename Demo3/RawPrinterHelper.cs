using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Demo3
{
    /// <summary>
    /// 
    /// </summary>
    public class RawPrinterHelper
    {
        /// <summary>
        /// 打印机
        /// </summary>
        private readonly string _printerName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="printerName"></param>
        public RawPrinterHelper(string printerName)
        {
            _printerName = printerName;
        }

        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class Docinfoa
        {
            /// <summary>
            /// 打印文件名称
            /// </summary>
            [MarshalAs(UnmanagedType.LPStr)] public string pDocName;

            /// <summary>
            /// 输出文件路径包含文件名，填写后将指令输出到此文件中，但不知道用什么可以打开
            /// </summary>
            [MarshalAs(UnmanagedType.LPStr)] public string pOutputFile;

            /// <summary>
            /// 文件类型 "RAW"
            /// </summary>
            [MarshalAs(UnmanagedType.LPStr)] public string pDataType;
        }

        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)]Docinfoa di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten); // SendBytesToPrinter()



        /// <summary>
        /// When the function is given a printer name and an unmanaged array
        /// of bytes, the function sends those bytes to the print queue.
        /// Returns true on success, false on failure.
        /// </summary>
        /// <param name="pBytes"></param>
        /// <param name="dwCount"></param>
        /// <returns></returns>
        public bool SendBytesToPrinter(IntPtr pBytes, Int32 dwCount, out Int32 dwError)
        {
            dwError = 0;
            Int32 dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            Docinfoa di = new Docinfoa();
            bool bSuccess = false;
            // Assume failure unless you specifically succeed.        
            di.pDocName = "leyou";
            di.pDataType = "RAW";
            //di.pOutputFile = @"D:\Project\PrinterDemo1\Demo3\bin\Debug\out.txt";
            // 打开打印机
            if (OpenPrinter(_printerName, out hPrinter, IntPtr.Zero))
            {
                // 开始文档
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // 开始页
                    if (StartPagePrinter(hPrinter))
                    {
                        // 写比特流
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }

                    EndDocPrinter(hPrinter);
                }

                ClosePrinter(hPrinter);
            }

            // 如果不成功，写错误原因
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }

            return bSuccess;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="szFileName"></param>
        /// <returns></returns>
        public bool SendFileToPrinter(string szFileName)
        {
            // Open the file.
            FileStream fs = new FileStream(szFileName, FileMode.Open);
            // Create a BinaryReader on the file.
            BinaryReader br = new BinaryReader(fs);
            // Dim an array of bytes big enough to hold the file's contents.
            Byte[] bytes = new Byte[fs.Length];
            bool bSuccess;
            // Your unmanaged pointer.
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength;
            nLength = Convert.ToInt32(fs.Length);
            // Read the contents of the file into the array.
            bytes = br.ReadBytes(nLength);
            // Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            // Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
            // Send the unmanaged bytes to the printer.
            Int32 dwError;
            bSuccess = SendBytesToPrinter(pUnmanagedBytes, nLength, out dwError);
            // Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            return bSuccess;
        }

        /// <summary>
        /// 输入字符串指令到打印机
        /// </summary>
        /// <param name="szString"></param>
        /// <returns></returns>
        public bool SendStringToPrinter(string szString, out int dwError)
        {
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // 转换ANSIbyte码       
            //pBytes = Marshal.StringToCoTaskMemUni(szString);
            byte[] Bytes = System.Text.Encoding.Default.GetBytes(szString);
            pBytes = Marshal.AllocCoTaskMem(Bytes.Length);
            Marshal.Copy(Bytes, 0, pBytes, Bytes.Length);
            // Send the converted ANSI string to the printer.
            bool opResult = SendBytesToPrinter(pBytes, Bytes.Length, out dwError);
            Marshal.FreeCoTaskMem(pBytes);
            return opResult;
        }

        /// <summary>
        /// 输入字符串指令到打印机
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="dwError"></param>
        /// <returns></returns>
        public bool SendBytesToPrinter(byte[] bytes, out int dwError)
        {
            IntPtr pBytes;
            pBytes = Marshal.AllocCoTaskMem(bytes.Length);
            Marshal.Copy(bytes, 0, pBytes, bytes.Length);
            bool opResult = SendBytesToPrinter(pBytes, bytes.Length, out dwError);
            Marshal.FreeCoTaskMem(pBytes);
            return opResult;
        }


    }
}
