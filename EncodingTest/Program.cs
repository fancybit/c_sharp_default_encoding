using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodingTest
{
    class Program
    {
        unsafe public static void PrintBin(ref string s)
        {
            fixed (char* fp = s)
            {
                var p = (byte*)fp;
                while(*p!=0)
                {
                    Console.Write((*p++).ToString("X"));
                }
                Console.WriteLine();
            }
        }

        public static void PrintEncoding(string str, string encoding)
        {
            Console.WriteLine($"{encoding}:");
            var bin = Encoding.GetEncoding(encoding).GetBytes(str);
            for (var i = 0; i < bin.Length; ++i)
            {
                Console.Write(bin[i].ToString("X"));
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            var str = "实践出真知";
            var str2312 = FileGB2312.getStr();
            var str8 = FileUTF8.getStr();
            var str16 = FileUTF16.getStr();
            PrintBin(ref str);
            PrintBin(ref str2312);
            PrintBin(ref str8);
            PrintBin(ref str16);

            PrintEncoding(str,"utf-8");
            PrintEncoding(str, "utf-16");
            PrintEncoding(str, "gb2312");
            PrintEncoding(str, "gbk");
            PrintEncoding(str, "ASCII");
            Console.ReadKey();
        }
    }
}