using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myhello
{
    internal class Program
    {
        static void Main(String[] args)
        {
            string filePath = "example.txt";

            // Write sample data to the file
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Line 1: Hello, StreamReader!");
                writer.WriteLine("Line 2: This is a sample file.");
                writer.WriteLine("Line 3: Have a great day!");
            }

            // Read from the file using StreamReader
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}