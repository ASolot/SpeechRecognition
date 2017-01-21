using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleCloudSamples;

namespace IdentifyWords
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            string newFileName = "";
           
            Console.WriteLine("Enter folder path: ");
            path = Console.ReadLine();

            foreach (string file in Directory.GetFiles(path, "*.wav").Select(Path.GetFileName))
            {
                Console.Write("Current converting file: ");
                Console.WriteLine(file);

                Transcribe.translate(path + "\\" + file);
                

                // TODO: rename files with the word discovered
                /*try
                {
                    if (File.Exists(path + "\\" + newFileName))
                    {
                        System.IO.File.Delete(path + "\\" + newFileName);
                    }
                    System.IO.File.Move(path + "\\" + file, path + "\\" + newFileName);

                    Console.WriteLine("File renamed to: " + newFileName);
                }
                catch(Exception e)
                {

                }
                finally
                {
                    
                }*/

                
            }

            Console.ReadLine();
        }
    }
}
