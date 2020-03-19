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
        private static readonly Tuple<string,string> pair = new Tuple<string,string>("ayay","yaya");
        public static bool ProperAlignement(string bfC)
        {
            Stack<string> bracks = new Stack<string>();
            try 
            {    
                foreach (string c in bfC.Split(" "))
                {
                    if (pair.Item1 == c)
                        bracks.Push(c);
                    else if (pair.Item2 == c)
                        if ("ayay" == bracks.Peek())
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
            foreach (string c in input.Split(" "))
            {
                switch(c)
                {
                    case "y":
                        Tokens.Add(Token.ShiftRight);
                        break;
                    case "a":
                        Tokens.Add(Token.ShiftLeft);
                        break;
                    case "ay":
                        Tokens.Add(Token.Plus);
                        break;
                    case "ya":
                        Tokens.Add(Token.Minus);
                        break;
                    case "ayaya":
                        Tokens.Add(Token.Output);
                        break;
                    case "aya":
                        Tokens.Add(Token.Input);
                        break;
                    case "ayay":
                        Tokens.Add(Token.StartLoop);
                        break;
                    case "yaya":
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
            AyaLang Help

            -c: Convert a string to ayalang code
            -i: Interpret and execute ayalang code
            -h: Show this help
            ";
        }    
    }
}