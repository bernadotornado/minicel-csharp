using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace minicel
{
    public static class Commands
    {
        public delegate void Command(List<object> cells);
        //public class CommandItem
        //{
        //    string _commandName;
        //    public string CommandName { get { return _commandName; } }
        //    Command _effect;
        //    public Command Effect { get { return _effect; } }
        //    public CommandItem(string commandName, Command effect)
        //    {
        //        _effect = effect;
        //        _commandName = commandName;
        //    }
        //}

        static Dictionary<string, Command> commandList = new Dictionary<string, Command>()
        {
            {"w", content => {
                StreamWriter sw = new StreamWriter((string)(content[0]));
                
            }}
        };

    }
}
