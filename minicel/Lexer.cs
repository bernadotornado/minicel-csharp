using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minicel
{
    public class Lexer
    {
        List<char> content = new List<char>();
        Stack<List<char>> cache = new Stack<List<char>>();


        public char[] AsCommand()
        {
            if (content.Contains('q'))
            {
                Program.Quit(0);
            }
            return null;
        }public char[] AsFunction()
        {
            return null;
        }
        public  void KeepTrack(char c)
        {
            content.Add(c);
        }
        public string Dump()
        {
            string s = "";
            foreach (var item in content)
            {
                s += item;

            }

            return s ;
        }
        public void Clear()
        {

        }
    }
}
