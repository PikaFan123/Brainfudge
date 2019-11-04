using System;
using System.Collections.Generic;
using System.Linq;
using Brainpreter;
using static System.Math;
namespace Brainpreter
{
    class BF
    {
        public static void Interpret(string toInp)
        {  
            if (Util.ProperAlignement(toInp))
            {
                var tokens = Util.TokenizeString(toInp).ToArray();
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
                                throw new InvalidOperationException();
                            }
                            pointer--;
                            break;
                        case Token.ShiftRight:
                            if (pointer == regSize)
                            {
                                System.Console.WriteLine("Error");
                                throw new InvalidOperationException();
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
            List<int> Linted = new List<int>();
            toConv.ToList().ForEach(x => Linted.Add(System.Convert.ToInt32(x)));
            string fudge = "";
            foreach (int intl in Linted.ToArray())
            {
                // This is just a variable to control which Divider returns a short program
                int Divider = 8;
                int multOfDiv = DivRem(intl, Divider, out int rem);
                string mdivPlus = "";
                string remPlus = "";
                string divPlus = "";
                for (int i = 0; i < multOfDiv; i++)
                    mdivPlus += "+";
                for (int i = 0; i < rem; i++)
                    remPlus += "+";
                for (int i = 0; i < Divider; i++)
                    divPlus += "+";
                fudge += $">{mdivPlus}[<{divPlus}>-]<{remPlus}.>";
            }
            System.Console.WriteLine(fudge);
        }
    }
}