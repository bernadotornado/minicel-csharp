using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace minicel
{
    class Program
    {

        static string title = "" +
            "       _       _             _ _  \n" +
            "      (_)     (_)           | | | \n" +
            " ____  _ ____  _  ____ _____| | | \n" +
            "|    \\| |  _ \\| |/ ___) ___ | | | \n" +
            "| | | | | | | | ( (___| ____| | | \n" +
            "|_|_|_|_|_| |_|_|\\____)_____)\\_)_)" ;
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
            ConsoleColor cr = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{title}\n");
            Console.ForegroundColor = cr;
            Console.Write($" version ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{versionString} \n\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" USAGE:  minicell <command> <operation> <flag>\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(
                   $"\t Note: When a file is opened, commands are not executed in a UNIX terminal style way.");
            Console.ForegroundColor = cr;
            Console.Write(
                   $"\n\t Help:\n" +
                   "\t\t minicell\n" +
                   "\t\t minicell h\n" +
                   "\t\t minicell help\n" +
                   "\t\t minicell -h\n" +
                   "\t\t minicell -help\n" +
                   $"\n\t Check version:\n" +
                   "\t\t minicell --version\n" +
                   "\t\t minicell -v\n " +
                   $"\n\t Open file:\n" + 
                   "\t\t minicell <path>\n" +
                   "\t\t minicell <path> -f\n" +
                   "\t\t minicell -o <path>\n" +
                   "\t\t minicell --open <path>\n" +
                   "\t\t minicell -o <path> -f\n" +
                   "\t\t minicell --open <path> -f\n"
                   );
        }
        static void Version()
        {
            Console.WriteLine($"minicell version {versionString}");
        }
        static void MissingPathError()
        {
            Quit(1, "Error: Missing path.\n" +
                                    "USAGE:\n" +
                                    "\tminicell <path>\n" +
                                    "\tminicell <path> -f\n" +
                                    "\tminicell -o <path>\n" +
                                    "\tminicell --open <path>\n" +
                                    "\tminicell -o <path> -f\n" +
                                    "\tminicell --open <path> -f\n" +
                                    "");
        }


        static void OpenFile(string[] args, List<string> content, bool force = false)
        {
            if(args[args.Length-1] == "-f")
            {
                force = true;
            }

            StreamReader sr = null;
            try
            {
                string[] vs = new string[] { "debug_none" };
                if (!force)
                {
                    string s = args[0];
                    vs = s.Split(".");
                    foreach (string item in vs)
                    {
                        Console.WriteLine(item);
                    }
                }
                if(vs[1] == "csv" || force)
                { 
                    sr = new StreamReader(args[0]);
                }
                else
                {
                    Quit(1, $"\"{args[0]}\" is not of type .csv\nType minicell <path> -f to force open the file.");
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
                else if(args.Length >= 2)
                {
                    switch (args[0])
                    {
                        case "--open":
                        case "-o":
                            string[] argf = new string[args.Length - 1];
                            for (int i = 0; i < argf.Length; i++)
                            {
                                argf[i] = args[i + 1];
                            }
                            OpenFile(argf, content);
                            break;
                    }
                }
                else if(args.Length == 3)
                {

                }
            }
            else
            {
                Help();
            }
        }
    }
}
