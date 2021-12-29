using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minicel
{
    static class ConsoleAppearance
    {

        public static ConsoleColor foreground = Console.ForegroundColor;
        public static ConsoleColor background = Console.BackgroundColor;
        static public void GreenBGBlackFG()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Green;
        }
        static public void BlackBGGreenFG()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        static public void ResetColors()
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
        }
    }
}
