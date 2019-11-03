using System;
using System.Collections.Generic;

namespace Brainpreter
{
    enum Token
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
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            if (input.Replace("[", "").Length == input.Replace("]", "").Length)
            {
            var tokens = TokenizeString(input).ToArray();
            int pointer = 0;
            long regSize = 34359738352;
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
                        register[pointer] = Convert.ToByte(Console.ReadKey().KeyChar);
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
        static List<Token> TokenizeString(string input)
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
    }
}