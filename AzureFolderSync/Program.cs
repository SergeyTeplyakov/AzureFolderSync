using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AzureFolderSync
{
    class Program
    {
        static void Main(string[] args)
        {
            Arguments[] newArguments = new Arguments[3];

            newArguments = Arguments.InitArguments();

            if (args.Length > 0)
            {
                newArguments = ParseArguments.ReadParams(args, newArguments);

                if (Arguments.IfAllArgsProvided(newArguments))
                {

                    foreach (Arguments newArg in newArguments)
                    {
                        Console.WriteLine(String.Format(" Key {0}, String Value {1},  Bool value {2}", newArg.paramKey, newArg.paramValue, newArg.boolValue));
                    }
                }
                else
                {
                    Console.WriteLine("Not all required parameters are provided!");
                }
            }
            else
            {
                Console.WriteLine("Help");

                ShowHelp(newArguments);
            }

            
            Console.ReadLine();
        }

        static void ShowHelp(Arguments[] arguments)

        {
            foreach (Arguments argument in arguments)
            {
                string helpLine = String.Format("{0} - {1}", argument.paramKey, argument.paramDescription);
                Console.WriteLine(helpLine);
            }
        }


    }
}
