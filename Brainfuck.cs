using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Brainpreter;
using static System.Math;
namespace Brainpreter
{
    class BF
    {
        static Config config;
        public static void Interpret(string toInp)
        {
            config = Config.FromJson(File.ReadAllText("config.json"));
            if (Util.ProperAlignement(toInp, config))
            {
                var tokens = Util.TokenizeString(toInp, config).ToArray();
                int pointer = 0;
                long regSize = int.MaxValue / 2;
                byte[] register = new byte[regSize];
                int looppointer = 0;
                for(int i = 0; i < tokens.Length; i++)
                {
                    Token token = tokens[i];
                    switch(token)
                    {
                        case Token.ShiftLeft:
                            if (pointer == 0)
                            {
                                System.Console.WriteLine("Error");
                                throw new InvalidOperationException("Pointer tried to exceed Register <");
                            }
                            pointer--;
                            break;
                        case Token.ShiftRight:
                            if (pointer == regSize)
                            {
                                System.Console.WriteLine("Error");
                                throw new InvalidOperationException("Pointer tried to exceed Register >");
                                
                            }
                            pointer++;
                            break;
                        case Token.Plus:
                            register[pointer]++;
                            break;
                        case Token.Minus:
                            register[pointer]--;
                            break;
                        case Token.Output:
                            Console.Write((char)register[pointer]);
                            break;
                        case Token.Input:
                            register[pointer] = System.Convert.ToByte(Console.ReadKey().KeyChar);
                            break;
                        case Token.StartLoop:
                            if (register[pointer] == 0)
                            {
                                i++;
                                while (looppointer > 0 || tokens[i] != Token.EndLoop)
                                {
                                    if (tokens[i] == Token.StartLoop)
                                    {
                                        looppointer++;
                                    }
                                    if (tokens[i] == Token.EndLoop)
                                    {
                                        looppointer--;
                                    }
                                    i++;
                                }
                            }
                            break;
                        case Token.EndLoop:
                            i--;
                            while (looppointer > 0 || tokens[i] != Token.StartLoop)
                            {
                                if (tokens[i] == Token.EndLoop)
                                {
                                    looppointer++;
                                }
                                if (tokens[i] == Token.StartLoop)
                                {
                                    looppointer--;
                                }
                                i--;
                            }
                            i--;
                            break;
                    }
                }
            }
            else
            {
                System.Console.WriteLine("Error");
            }
        }
        public static void Convert(string toConv)
        {
            config = Config.FromJson(File.ReadAllText("config.json"));
            List<int> Linted = new List<int>();
            toConv.ToList().ForEach(x => Linted.Add(System.Convert.ToInt32(x)));
            string fudge = "";
            string sepc = " ";
            if (config.SingleChar)
                sepc = "";
            foreach (int intl in Linted.ToArray())
            {
                // This is just a variable to control which Divider returns a short program
                int Divider = 8;
                int multOfDiv = DivRem(intl, Divider, out int rem);
                string mdivPlus = "";
                string remPlus = "";
                string divPlus = "";
                for (int i = 0; i < multOfDiv; i++)
                    mdivPlus += $"{config.Plus}{sepc}";
                for (int i = 0; i < rem; i++)
                    remPlus += $"{config.Plus}{sepc}";
                for (int i = 0; i < Divider; i++)
                    divPlus += $"{config.Plus}{sepc}";
                fudge += $"{config.ShiftRight}{sepc}{mdivPlus}{config.StartLoop}{sepc}{config.ShiftLeft}{sepc}{divPlus}{config.ShiftRight}{sepc}{config.Minus}{sepc}{config.EndLoop}{sepc}{config.ShiftLeft}{sepc}{remPlus}{config.Write}{sepc}{config.ShiftRight}{sepc}";
            }
            System.Console.WriteLine(fudge);
        }
    }
}