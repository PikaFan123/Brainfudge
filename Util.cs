using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                string[] ff = null;
                if (conf.SingleChar)
                {
                    ff = bfC.ToCharArray().Select(c => c.ToString()).ToArray();
                }
                else
                {
                    ff = bfC.Split(' ');
                }
                foreach (string c in ff)
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
            string[] ff = null;
            if (conf.SingleChar)
            {
                ff = input.ToCharArray().Select(c => c.ToString()).ToArray();
            }
            else
            {
                ff = input.Split(' ');
            }
            foreach (string c in ff)
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
            -p: Print the current brainfuck definition
            -w: Write default config to config.json
            -h: Show this help
            ";
        }  
        public static string GetDefinition()
        {
            config = Config.FromJson(File.ReadAllText("config.json"));
            string retMe = "";
            foreach (KeyValuePair<string,string> x in Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,string>>(config.ToJson()))
            {
                string key = x.Key + ":";
                for (int i = x.Key.Length - 12; i < 0; i++) {key += " ";}
                retMe += $"{key} {x.Value}\n";
            }
            return retMe;
        }
        public static string WriteDefault()
        {
            File.WriteAllText("config.json", Newtonsoft.Json.JsonConvert.SerializeObject(new Config(), Newtonsoft.Json.Formatting.Indented));
            return "Done!";
        }
    }
}

