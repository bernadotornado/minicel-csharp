using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace minicel
{
    class Program
    {

        static string title = "" +
            "       _       _             _   \n" +
            "      (_)     (_)           | |  \n" +
            " ____  _ ____  _  ____ _____| |  \n" +
            "|    \\| |  _ \\| |/ ___) ___ | |  \n" +
            "| | | | | | | | ( (___| ____| |_ \n" +
            "|_|_|_|_|_| |_|_|\\____)_____)___)" ;
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
            "--open",
            "new"
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
            Console.Write(" To create a ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("new file"); 
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" type "); 
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("minicel new <filename>.csv\n"); 
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" USAGE:  minicel <command> <operation> <flag>\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(
                   $"\t Note: When a file is opened, commands are not executed in a UNIX terminal style way.");
            Console.ForegroundColor = cr;
            Console.Write(
                   $"\n\t Help:\n" +
                   "\t\t minicel\n" +
                   "\t\t minicel h\n" +
                   "\t\t minicel help\n" +
                   "\t\t minicel -h\n" +
                   "\t\t minicel -help\n" +
                   $"\n\t Check version:\n" +
                   "\t\t minicel --version\n" +
                   "\t\t minicel -v\n " +
                   $"\n\t Open file:\n" + 
                   "\t\t minicel <path>\n" +
                   "\t\t minicel <path> -f\n" +
                   "\t\t minicel -o <path>\n" +
                   "\t\t minicel --open <path>\n" +
                   "\t\t minicel -o <path> -f\n" +
                   "\t\t minicel --open <path> -f\n"
                   );
        }
        static void Version()
        {
            Console.WriteLine($"minicel version {versionString}");
        }
        static void NewFile(string[] args, List<string> content,string s)
        {

            string[] argf = new string[]
            {
                s
            };
            
            FileStream fs= File.Create(s);
            fs.Close();
           
            OpenFile(argf, content);
        }
        static void MissingPathError(bool isCreatingFile = false)
        {
            if (isCreatingFile)
            {
                Quit(1, "Error: No filename was provided.\n" +
                                    "USAGE:\n" +
                                    "\tminicel new <filename>.csv");
            }
            else
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
                    Quit(1, $"\"{args[0]}\" is not of type .csv\nType minicel <path> -f to force open the file.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Quit(1, $"\"{args[0]}\" is not a valid file or file path.");

            }
            finally 
            {
                string line;
                
                while ((line = sr.ReadLine()) != null)
                {
                    content.Add(line);
                }
            }
        }

        static void Main(string[] args)
        {
            List<string> content = new List<string>();
            if(args.Length > 0)
            {
                if(args.Length == 2)
                {
                    switch (args[0])
                    {
                        case "new":
                            NewFile(args, content,args[1]);
                              break;
                        default:
                            break;
                    }
                }

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
                        case "new":
                                MissingPathError(true);
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
            MinicelApplication app = new MinicelApplication(content);
        }
    }
}
