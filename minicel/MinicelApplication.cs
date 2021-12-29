using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minicel
{
    class MinicelApplication
    {

        int xPos = 0;
        int yPos = 0;

        string last = "";

        ConsoleColor foreground = Console.ForegroundColor;
        ConsoleColor background = Console.BackgroundColor;
        enum State
        {
            CommandMode, 
            InsertMode,
            MovementMode
        }
        char inputChar = ' ';
        State currentstate = State.MovementMode;
        List<List<string>> cells = new List<List<string>>();
        bool isFirstLoop = true;

        public void GreenBGBlackFG()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Green;
        }public void BlackBGGreenFG()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void ResetColors()
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
        }
        ConsoleKeyInfo consoleKey;
        public MinicelApplication(List<string> content)
        {
            foreach (var item in content)
            {
                string[] s = item.Split(',');
                List<string> sl = s.ToList<string>();
                cells.Add(sl);
            }
            while (true)
            {
                if (!isFirstLoop)
                {
                    consoleKey = Console.ReadKey();
                    inputChar = consoleKey.KeyChar;
                }
                ResetColors();
                if(currentstate == State.MovementMode)
                {

                Console.Clear();
                ResetColors();
                GreenBGBlackFG();
                Console.Write("   ");
                for (int column = 65; column < 78; column++)
                {
                    Console.Write((char)(column)+"       "+(column != 77 ? "|": ""));
                }

                
                Console.Write("\n");
                GreenBGBlackFG();
                for (int row = 0;  row< 26; row++)
                {
                    string rowf = row.ToString();
                    string res = "";
                    int length = 3;
                    for(int i = 0; i< length-rowf.Length; i++)
                    {
                        res += " ";
                    }
                    res += row.ToString();
                    Console.Write(res+"\n");
                }
                }


                if (currentstate != State.CommandMode)
                {
                    switch (inputChar)
                    {
                        case 'q':
                            ResetColors();
                            Console.Clear();
                            Environment.Exit(0);
                            break;
                        case ':':
                            currentstate = State.CommandMode;
                            break;
                        case 'i':
                        case '=':
                            currentstate = State.InsertMode;
                            break;
                        case 'h':
                        case 'j':
                        case 'k':
                        case 'l':
                            Console.Clear();
                            currentstate = State.MovementMode;
                            break;

                    }
                }
                else
                {
                    if (consoleKey.Key == ConsoleKey.Enter)
                    {

                    }
                }

                ResetColors();
                string s = currentstate switch
                {
                    State.InsertMode => "--INSERT--",
                    State.CommandMode => "--COMMAND--",
                    State.MovementMode => "--MOVE--",
                   // _ => "--MOVE--"

                };
                BlackBGGreenFG();
                if(s != last)
                    Console.WriteLine("\t" + s);
                last = s;
                isFirstLoop = false;
            }
        }

    }
}
