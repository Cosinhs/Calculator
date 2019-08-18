using System;

namespace CalculatorCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            const string WELCOME = "Welcome to Calculator. Feel free to type any expression you want.";
            const string PROMPT = ">> ";
            Console.Out.Write(WELCOME + Environment.NewLine);
            while(true)
            {
                Console.Out.Write(PROMPT);
                try
                {
                    var input = Console.In.ReadLine();
                    if(string.Compare(input, "exit", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        break;
                    }
                    Console.Out.Write(input);
                    Console.Out.Write(Environment.NewLine);
                }
                catch(Exception e)
                {
                    Console.Out.Write("Oops! Something seems to go wrong." + Environment.NewLine);
                    Console.Out.Write(e.Message);
                    Console.Out.Write(Environment.NewLine);
                }
               
            }
        }
    }
}
