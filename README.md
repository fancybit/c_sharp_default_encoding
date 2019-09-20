# c_sharp_default_encoding
验证了一下C#默认的编码方式
c# .net使用的字符编码验证
所有源码见这里：

https://github.com/fancybit/c_sharp_default_encoding

C#
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
        {//打印一个字符串在内存中的十六进制数据
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
        {//输出一个字符串按照指定编码对应的十六进制数据
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
            //下面3个类所在的文件把上面几个字用不同编码格式存储
            //输出结果相同 说明不管cs源代码用什么形式编码保存 编译出的IL都是按照UTF16形式编码
            var str2312 = FileGB2312.getStr();
            var str8 = FileUTF8.getStr();
            var str16 = FileUTF16.getStr();
            PrintBin(ref str);
            PrintBin(ref str2312);
            PrintBin(ref str8);
            PrintBin(ref str16);

            //使用不同编码形式输出，比照结果可以看出c#.net对内置字符串变量使用的是utf16编码格式
            PrintEncoding(str,"utf-8");
            PrintEncoding(str, "utf-16");
            PrintEncoding(str, "gb2312");
            PrintEncoding(str, "gbk");
            PrintEncoding(str, "ASCII");
            Console.ReadKey();
        }
    }
}
运行结果是这样：



和人争论C#的“默认字符编码”是什么，这么看确实是UTF16编码了
