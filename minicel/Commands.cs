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
        static public string ExcecuteFunction(Commands.Function function)
        {
            return null;
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

        public static Dictionary<string, Command> commandList = new Dictionary<string, Command>()
        {
            {"w", content => {
                List<List<string>> cells =(List<List<string>>) content[1];
                StreamWriter sw = new StreamWriter((string)(content[0]));
                for (int y = 0; y < cells.Count; y++)
                {
                    string res = "";
                    for (int x = 0; x < cells[y].Count; x++)
                    {
                        res+= cells[y][x]+";";
			        }
                    sw.WriteLine(res);

			    }
                
                sw.Close();
            }},
            {"q", content =>
            {
                Program.Quit(0);
            } }
        };

    }
}
