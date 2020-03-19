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
            foreach (string c in input.Split(' '))
            {
                switch(c)
                {
                    case "pipi":
                        Tokens.Add(Token.ShiftRight);
                        break;
                    case "pichu":
                        Tokens.Add(Token.ShiftLeft);
                        break;
                    case "pi":
                        Tokens.Add(Token.Plus);
                        break;
                    case "ka":
                        Tokens.Add(Token.Minus);
                        break;
                    case "pikachu":
                        Tokens.Add(Token.Output);
                        break;
                    case "pikapi":
                        Tokens.Add(Token.Input);
                        break;
                    case "pika":
                        Tokens.Add(Token.StartLoop);
                        break;
                    case "chu":
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
            Pikafudge Help

            -c: Convert a string to pikalang code
            -i: Interpret and execute pikalang code
            -h: Show this help
            ";
        }    
    }
}