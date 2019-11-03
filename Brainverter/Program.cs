using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;
namespace Brainverter
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] input;
            if (args.Length != 0)
            {
                var inp = "";
                args.ToList().ForEach(x => inp += (x + " "));
                inp = inp.Substring(0, inp.Length - 1);
                input = inp.ToCharArray();
            }
            else
            {
                input = Console.ReadLine().ToCharArray();
            }
            List<int> Linted = new List<int>();
            input.ToList().ForEach(x => Linted.Add(Convert.ToInt32(x)));

            System.Console.WriteLine(ConvertToShortFudge(Linted.ToArray()));
        }
        // Old Code
        static string ConvertToFudge(int[] ints)
        {
            string fudge = "";
            foreach(int intl in ints)
            {
                for (int i = 0; i < intl; i++)
                {
                    fudge += "+";
                }
                fudge += ".>";
            }
            return fudge;
        }
        static string ConvertToShortFudge(int[] ints)
        {
            string fudge = "";
            foreach (int intl in ints)
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
            return fudge;
        }
    }
}
