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
            "--version",
            "help", 
            "h", 
            "--help", 
            "-h",
            "-o",
            "--open"
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
                   $"USAGE:  minicel <command> <operation> <flag>\n" +
                   $"\n\tOpen file:\n" + 
                   "\t\tminicel <path>\n" +
                   "\t\tminicel <path> -f\n" +
                   "\t\tminicel -o <path>\n" +
                   "\t\tminicel --open <path>\n" +
                   "\t\tminicel -o <path> -f\n" +
                   "\t\tminicel --open <path> -f\n"
                   );
        }
        static void Version()
        {
            Console.WriteLine($"minicel version {versionString}");
        }
        static void MissingPathError()
        {
            Quit(1, "Error: Missing path.\n" +
                                    "USAGE:\n" +
                                    "\tminicel <path>\n" +
                                    "\tminicel <path> -f\n" +
                                    "\tminicel -o <path>\n" +
                                    "\tminicel --open <path>\n" +
                                    "\tminicel -o <path> -f\n" +
                                    "\tminicel --open <path> -f\n" +
                                    "");
        }


        static void OpenFile(string[] args, List<string> content)
        {
            StreamReader sr = null;
            try
            {
                string s = args[0];
                string[] vs=  s.Split(".");
                if(vs[1] == ".csv")
                { 
                    sr = new StreamReader(args[0]);
                }
                else
                {
                    Quit(1, $"\"{args[0]}\" is not of type .csv\nType minicel <path> -f to force open the file.");
                }
            }
            catch (Exception)
            {
                Quit(1, $"\"{args[0]}\" is not a valid file or file path.");
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
                        case "help": 
                        case "h":
                        case "--help":
                        case "-h":
                                Help();
                                break;
                        case "--open":
                        case "-o":
                                MissingPathError();
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
