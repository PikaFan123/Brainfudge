using System;
using System.Linq;
namespace Brainpreter
{
    class Program
    { 
        static void Main(string[] args)
        {
            try 
            {
                if (args.Length >= 0)
                {
                    var par = args.Skip(1).ToArray();
                    string arg = "";
                    foreach(string s in par) {arg = arg + $"{s} "; }
                    arg = arg.Remove(arg.Length - 1);
                    if (args[0] == "-i")
                    {
                        BF.Interpret(arg);
                    }
                    else if (args[0] == "-c")
                    {
                        BF.Convert(arg);
                    }
                    else if (args[0] == "-h")
                    {
                        System.Console.WriteLine(Util.GetHelp());
                    }
                }
                else 
                {
                    System.Console.WriteLine(Util.GetHelp());
                }
            }
            catch(ArgumentOutOfRangeException) 
            {
                System.Console.WriteLine(Util.GetHelp());
            }
            
        }
    }
}
