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

        public void Pop()
        {

            content.RemoveAt(content.Count-1);
            // char a = content.Last();
            // content.Remove(a);
            //string a = Dump();
            ////Clear();
            //char[]d = a.ToCharArray();
            //foreach (var item in d)
            //{
            //    Console.WriteLine(item);
            //}
        }
        public char[] AsCommand()
        {
            if (content.Contains('q'))
            {
                Commands.commandList["q"].Invoke(null);
            }
            if(content.Contains('w'))
            {
                List<object> objs = new List<object>();
                foreach (var cell in Program.App.Cells)
                {
                    objs.Add(cell);
                }
                Commands.commandList["w"].Invoke(objs
                    );
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
            cache.Push(content);
            content.Clear();
        }
    }
}
