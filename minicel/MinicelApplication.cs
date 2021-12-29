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

        ConsoleColor foreground = Console.ForegroundColor;
        ConsoleColor background = Console.BackgroundColor;
        enum State
        {
            CommandMode, 
            InsertMode,
            MovementMode
        }


        public void GreenFont()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Green;
        }
        public void ResetFont()
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
        }
        public MinicelApplication(List<string> content)
        {
            //foreach (var item in content)
            //{
            //    Console.WriteLine(item);
            //}
            while (true)
            {
                ResetFont();
                Console.Clear();
                ResetFont();
                GreenFont();
                Console.Write("   ");
                for (int column = 65; column < 78; column++)
                {
                    Console.Write((char)(column)+"       |");
                }

                ResetFont();
                Console.Write("\n");
                GreenFont();
                for (int row = 0;  row< 11; row++)
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

                char inputChar = Console.ReadKey().KeyChar;
                if(inputChar == 'q')
                {
                    ResetFont();
                    Console.Clear();
                    Environment.Exit(0);
                }
            }
        }

    }
}
