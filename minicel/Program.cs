using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace minicel
{
    class Program
    {
        static string versionString = "1.0.0 indev";
        static List<string> arg1commands = new List<string>
        {
            "-v", 
            "--version"
        };

        static void Quit(int exitCode, string errorMessage = "none") 
        {
            if(exitCode != 0 && errorMessage != "none")
            {
                Console.WriteLine(errorMessage);
            }
            Environment.Exit(exitCode); 
        }
        static void Help()
        {
            Console.Write($"minicel version {versionString} \n\n" +
                   $"USAGE:  minicel <command> <operation>\n" +
                   $"\t<commnand>:" +
                   $"\t\t");
        }
        static void Version()
        {
            Console.WriteLine($"minicel version {versionString}");
        }


        static void OpenFile(string[] args, List<string> content)
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(args[0]);
            }
            catch (Exception)
            {
                Quit(1, $"{args[0]} is not a valid file or file path.");
            }
            finally
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    content.Add(line);
                    Console.WriteLine(line);
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine(args.Length);
            List<string> content = new List<string>();
            if(args.Length > 0)
            {
                if(args.Length == 1)
                {
                    if(!arg1commands.Contains(args[0]))
                        OpenFile(args, content);
                    else switch (args[0])
                    {
                        case "-v":
                        case "--version":
                                Version();
                                break;
                    }
                }
            }
            else
            {
                Help();
            }

        }
    }
}
