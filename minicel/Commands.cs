using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace minicel
{
    static public class CommandHandler
    {
        static public bool TryCommand(char[] command, out Commands.Command resCmd)
        {
            resCmd = null;
            return false;
        }static public bool TryFunction(char[] command, out Commands.Function resFunc)
        {
            resFunc = null;
            return false;
        }
        static public void ThrowError(char[] command)
        {

        }
        static public void Excecute(Commands.Command command)
        {

        } 
        static public void ExcecuteFunction(Commands.Function function)
        {

        }
    }
    public static class Commands
    {
        public delegate void Command(List<object> cells);
        public delegate void Function(List<object> cells);
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
