using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIODemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string strtest = Console.ReadLine();
            byte[] arrBytes = System.Text.Encoding.UTF8.GetBytes(strtest);
            //Binary mode writing
            FileStream fs = new FileStream("C:\\Users\\Administrator\\Documents\\temp\\test.txt", FileMode.OpenOrCreate,FileAccess.Write);
            fs.Write(arrBytes, 0, arrBytes.Length);
            fs.Close();
            //Binary mode reading
            FileStream fs2 = new FileStream("C:\\Users\\Administrator\\Documents\\temp\\test.txt", FileMode.Open, FileAccess.Read);
            byte[] arrBytes2 = new byte[1024];
            fs2.Read(arrBytes2, 0, arrBytes2.Length);
            fs2.Close();
            string strContent = Encoding.UTF8.GetString(arrBytes2);
            Console.WriteLine(strContent);

            //Text mode writing
            //StreamWriter writer = new StreamWriter("C:\\Users\\Administrator\\Documents\\temp\\test.txt");
            //writer.WriteLine("Hello World");
            //writer.Close();
            //StreamReader sr =File.OpenText("C:\\Users\\Administrator\\Documents\\temp\\test.txt");

            //Text mode reading
            //StreamReader reader = new StreamReader("C:\\Users\\Administrator\\Documents\\temp\\test.txt");
            //string strContent = reader.ReadToEnd();
            //Console.WriteLine(strContent);
            
            //while (true)
            //{
            //    string line = reader.ReadLine();
            //    if (line == null)
            //    {
            //        break;
            //    }
            //    Console.WriteLine(line);
            //}
            //reader.Close();
        }
    }
}
