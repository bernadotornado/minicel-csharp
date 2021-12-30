using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minicel
{
    public class Lexer
    {
        List<char> cache = new List<char>(); 

        public char[] AsCommand()
        {
            return null;
        }public char[] AsFunction()
        {
            return null;
        }
        public  void KeepTrack(char c)
        {
            cache.Add(c);
        }
        public string Dump()
        {
            string s = "";
            foreach (var item in cache)
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
