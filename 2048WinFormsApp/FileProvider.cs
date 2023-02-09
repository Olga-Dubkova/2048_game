using System;
using System.IO;
using System.Text;

namespace _2048WinFormsApp

{
    public class FileProvider
    {
        
        public static void Replace(string fileName, string value)
        {
            StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8);
            writer.WriteLine(value);
            writer.Close();
        }

        internal static bool Exists(string path)
        {
            return File.Exists(path);   
        }

        public static string GetValue(string fileName)
        {
            StreamReader reader = new StreamReader(fileName, Encoding.UTF8);
            var value = reader.ReadToEnd(); 
            reader.Close();
            return value;   
        }
    }

}
