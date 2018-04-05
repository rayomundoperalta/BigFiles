using System;
using System.IO;
using System.Collections.Generic;

namespace BigFiles
{
    class FileSizes
    {
        public long size;
        public string path;

        public FileSizes(long size, string path)
        {
            this.size = size;
            this.path = path;
        }
    }

    static class Program
    {
        static void Main(string[] args)
        {
            List<string> AllFiles = new List<string>();
            List<FileSizes> SizeList = new List<FileSizes>();


            void ParsePath(string path, List<string> FileList)
            {
                try
                {
                    string[] SubDirs = Directory.GetDirectories(path);
                    FileList.AddRange(SubDirs);
                    FileList.AddRange(Directory.GetFiles(path));
                    foreach (string subdir in SubDirs)
                        ParsePath(subdir, FileList);
                } catch (UnauthorizedAccessException exc)
                {
                    Console.WriteLine("Acceso no autorizado; " + path);
                }
                
            }
            ParsePath(@"C:\", AllFiles); // C:\Users\raymu\AppData       D:\Banxico      
            FileInfo info;
            long media = 0;
            foreach (string entry in AllFiles)
            {
                info = new FileInfo(entry);
                if ((info.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    // Console.WriteLine("Directorio - " + entry);
                }
                else
                {
                    media += info.Length;
                    long size = info.Length / (1024 * 1024);
                    SizeList.Add(new FileSizes(size, entry));
                }
            }
            Console.WriteLine("# Files: " + SizeList.Count.ToString() + ", Tamaño medio: " + media / SizeList.Count + " bytes"); ;
            Console.WriteLine(SizeList.Count.ToString());
            foreach (FileSizes item in SizeList)
            { 
                if (item.size > 100)
                    Console.WriteLine(item.size.ToString() + " MB - " + item.path);
            }
            Console.WriteLine("# Files: " + SizeList.Count.ToString() + ", Tamaño medio: " + media / SizeList.Count + " bytes");
            Console.WriteLine("FIN");
            Console.ReadKey();
        }
    }      
}
