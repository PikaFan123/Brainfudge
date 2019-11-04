using System;
using System.Collections.Generic;

namespace Brainpreter
{
    public enum Token
    {
        ShiftRight,
        ShiftLeft,
        Plus,
        Minus,
        Output,
        Input,
        StartLoop,
        EndLoop

    }
    public static class Util
    {
        private static readonly Tuple<char,char> pair = new Tuple<char,char>('[',']');
        public static bool ProperAlignement(string bfC)
        {
            Stack<char> bracks = new Stack<char>();
            try 
            {    
                foreach (char c in bfC)
                {
                    if (pair.Item1 == c)
                        bracks.Push(c);
                    else if (pair.Item2 == c)
                        if ('[' == bracks.Peek())
                            bracks.Pop();
                        else
                            return false;
                    else
                        continue;
                }
            }
            catch{return false;}
            return bracks.Count == 0 ? true : false;
        }
        public static List<Token> TokenizeString(string input)
        {
            List<Token> Tokens = new List<Token>();
            foreach (char c in input)
            {
                switch(c)
                {
                    case '>':
                        Tokens.Add(Token.ShiftRight);
                        break;
                    case '<':
                        Tokens.Add(Token.ShiftLeft);
                        break;
                    case '+':
                        Tokens.Add(Token.Plus);
                        break;
                    case '-':
                        Tokens.Add(Token.Minus);
                        break;
                    case '.':
                        Tokens.Add(Token.Output);
                        break;
                    case ',':
                        Tokens.Add(Token.Input);
                        break;
                    case '[':
                        Tokens.Add(Token.StartLoop);
                        break;
                    case ']':
                        Tokens.Add(Token.EndLoop);
                        break;
                    default:
                        break;
                }
            }
            return Tokens;
        }
        public static string GetHelp()
        {
            return 
            @"
            Brainpreter Help

            -c: Convert a string to brainfuck code
            -i: Interpret and execute brainfuck code
            -h: Show this help
            ";
        }    
    }
}