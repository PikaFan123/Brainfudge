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
        static Config config;
        private static Tuple<string,string> pair;
        public static bool ProperAlignement(string bfC, Config conf)
        {
            config = conf;
            pair = new Tuple<string,string>(config.StartLoop,config.EndLoop);
            Stack<string> bracks = new Stack<string>();
            try 
            {    
                foreach (string c in bfC.Split(" "))
                {
                    if (pair.Item1 == c)
                        bracks.Push(c);
                    else if (pair.Item2 == c)
                        if (config.StartLoop == bracks.Peek())
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
        public static List<Token> TokenizeString(string input, Config conf)
        {
            config = conf;
            List<Token> Tokens = new List<Token>();
            foreach (string c in input.Split(" "))
            {
                if (c == config.ShiftLeft)
                {
                    Tokens.Add(Token.ShiftLeft);
                    continue;
                }
                else if (c == config.ShiftRight)
                {
                    Tokens.Add(Token.ShiftRight);
                    continue;
                }
                else if (c == config.Plus)
                {
                    Tokens.Add(Token.Plus);
                    continue;
                }
                else if (c == config.Minus)
                {
                    Tokens.Add(Token.Minus);
                    continue;
                }
                else if (c == config.Write)
                {
                    Tokens.Add(Token.Output);
                    continue;
                }
                else if (c == config.Input)
                {
                    Tokens.Add(Token.Input);
                    continue;
                }
                else if (c == config.StartLoop)
                {
                    Tokens.Add(Token.StartLoop);
                    continue;
                }
                else if (c == config.EndLoop)
                {
                    Tokens.Add(Token.EndLoop);
                    continue;
                }
            }
            return Tokens;
        }
        public static string GetHelp()
        {
            return 
            @"
            Brainpreter Help

            This Version loads its Lang from config.json

            -c: Convert a string to brainfuck code
            -i: Interpret and execute brainfuck code
            -h: Show this help
            ";
        }    
    }
}