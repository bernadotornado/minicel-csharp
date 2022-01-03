using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minicel
{
    class MinicelApplication
    {

        struct CellPos
        {
            public int column;
            public int row;
            public CellPos(int _col, int _row)
            {
                column = _col;
                row = _row;
                
            }
        }
        CellPos renderingCell;
        

        int colPos = 0;
        int rowPos = 0;

        string last = "";

        enum State
        {
            CommandMode,
            InsertMode,
            FunctionMode,
            MovementMode,
            VisualMode
            
        }
        char inputChar = ' ';
        State currentstate = State.MovementMode;
        Lexer lexer = new Lexer();
        List<List<string>> cells = new List<List<string>>();
        List<CellPos> selectedCells = new List<CellPos>();
        bool isFirstLoop = true;


        ConsoleKeyInfo consoleKey;


        void DrawRows()
        {

            
            for (int row = 0; row < 26; row++)
            {
                renderingCell.row = row;
                ConsoleAppearance.GreenBGBlackFG(); 
                string rowf = row.ToString();
                string res = "";
                int length = 3;
                for (int i = 0; i < length - rowf.Length; i++)
                {
                    res += " ";
                }
                res += row.ToString();
                Console.Write(res);
                ConsoleAppearance.ResetColors();
                try
                {
                    for (int i = 0; i < cells[renderingCell.row].Count; i++)
                    {
                        if (i > 12)
                            break;
                        renderingCell.column = i;

                        Console.Write("       ");
                        Console.Write(cells[renderingCell.row][renderingCell.column]);
                        if(cells[renderingCell.row][renderingCell.column].Length== 0)
                        {
                            Console.Write(" ");
                        }
                        Console.Write(" ");
                    }
                }
                catch (Exception)
                {

                }
                
                Console.Write("\n");
                
            }
        }
        void DrawCols()
        {
            ConsoleAppearance.ResetColors();
            ConsoleAppearance.GreenBGBlackFG();
            Console.Write("   ");
            for (int column = 65; column < 78; column++)
            {
                Console.Write((char)(column) + "       " + (column != 77 ? "|" : ""));
            }
            Console.Write("\n");
        }

        void DrawMode() {
            ConsoleAppearance.ResetColors();
            string s = currentstate switch
            {
                State.InsertMode => "--INSERT--",
                State.CommandMode => "--COMMAND--",
                State.MovementMode => "--MOVE--",
                State.FunctionMode => "--FN--",

            };
            ConsoleAppearance.BlackBGGreenFG();
            if (s != last)
                Console.WriteLine("\t" + s);
            last = s;
        }


        void GetInput()
        {
            consoleKey = Console.ReadKey();
            inputChar = consoleKey.KeyChar;
        }
        
        void ClearSelection()
        {

            throw new NotImplementedException();
        }
        void SelectCell(int row, int col)
        {
            selectedCells.Add(new CellPos(colPos, rowPos));
        }
        void SetCurrentCell(string s)
        {
            throw new NotImplementedException();
        }

        bool CheckForEsc()
        {
            if (consoleKey.Key == ConsoleKey.Escape)
            {
                lexer.Clear();
                currentstate = State.MovementMode;
                return true;

            }
            return false;
        }
        void UpdateState()
        {
            switch (currentstate)
            {
                case State.CommandMode:
                    if (CheckForEsc())
                        break;

                    if (consoleKey.Key != ConsoleKey.Enter)
                        lexer.KeepTrack(inputChar);
                    else {
                        if (CommandHandler.TryCommand(lexer.AsCommand(), out Commands.Command command))
                            CommandHandler.Excecute(command);
                        else
                            CommandHandler.ThrowError(lexer.AsCommand()); 
                        lexer.Clear();
                        currentstate = State.MovementMode; 
                    }
                    break;
                case State.InsertMode:
                    
                    if (CheckForEsc())
                        break;

                    if (consoleKey.Key != ConsoleKey.Enter)
                        lexer.KeepTrack(inputChar);
                    else {
                        SetCurrentCell(lexer.Dump());
                        lexer.Clear();
                    }
                    break;
                case State.VisualMode:
                    throw new NotImplementedException();
                    break;
                case State.MovementMode:
                    
                    Console.Clear();
                    switch (consoleKey.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            rowPos--;
                            break;
                        case ConsoleKey.UpArrow:
                            colPos--;
                            break;
                        case ConsoleKey.RightArrow:
                            rowPos++;
                            break;
                        case ConsoleKey.DownArrow:
                            colPos++;
                            break;
                        default:
                            break;
                    }
                    switch (consoleKey.KeyChar)
                    {
                        case 'h':
                            rowPos--;
                            break;
                        case 'j':
                            colPos--;
                            break;
                        case 'l':
                            rowPos++;
                            break;
                        case 'k':
                            colPos++;
                            break;
                        case ':':
                            currentstate = State.CommandMode;
                            break;
                        case 'i':
                            currentstate = State.InsertMode;
                            break;
                        case '=':
                            currentstate = State.FunctionMode;
                            break;
                        case 'v':
                            currentstate = State.VisualMode;
                            break;
                        default:
                            break;

                    }

                    SelectCell(rowPos, colPos);
                    DrawCols();
                    DrawRows();
                    break;
                case State.FunctionMode:
                    if (CheckForEsc())
                        break;

                    if (consoleKey.Key != ConsoleKey.Enter)
                        lexer.KeepTrack(inputChar);
                    else
                    {
                        if(CommandHandler.TryFunction(lexer.AsFunction(), out Commands.Function function)){
                           SetCurrentCell( CommandHandler.ExcecuteFunction(function));
                        }
                        else
                            CommandHandler.ThrowError(lexer.AsFunction());
                        lexer.Clear();
                        currentstate = State.MovementMode;
                    }
                    break;
                default:
                    break;
            }





            //if (currentstate != State.CommandMode)
            //{
            //    switch (inputChar)
            //    {
            //        case 'q':
            //            ConsoleAppearance.ResetColors();
            //            Console.Clear();
            //            Environment.Exit(0);
            //            break;
            //        case ':':
            //            currentstate = State.CommandMode;
            //            break;
            //        case 'i':
            //        case '=':
            //            currentstate = State.InsertMode;
            //            break;
            //        case 'h':
            //        case 'j':
            //        case 'k':
            //        case 'l':
            //            Console.Clear();
            //            currentstate = State.MovementMode;
            //            break;

            //    }
            //}
            //else
            //{
            //    if (consoleKey.Key == ConsoleKey.Enter)
            //    {

            //    }
            //}
        }

        void Loop()
        {
            while (true)
            {
                if (!isFirstLoop)
                {
                    GetInput();
                }
                ConsoleAppearance.ResetColors();
                if (currentstate == State.MovementMode)
                { 
                    
                } 
                UpdateState();
                    Console.Write("\n");
                    ConsoleAppearance.GreenBGBlackFG();
                DrawMode();
                isFirstLoop = false;
            }
        }

        void FillCells(List<string> content)
        {
            foreach (var item in content)
            {
                string[] s = item.Split(';');
                List<string> sl = s.ToList<string>();
                cells.Add(sl);
            }
        }

        void UpdateCells() {
            throw new NotImplementedException();
        }
        public MinicelApplication(List<string> content)
        {
            FillCells(content);
            Loop(); 
        }
    }
}
