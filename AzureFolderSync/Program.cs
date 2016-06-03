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

            newArguments = InitArguments();

            if (args.Length > 0)
            {
                newArguments = ParseArguments.ReadParams(args, newArguments);

                if (IfAllArgsProvided(newArguments))
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

        static Arguments[] InitArguments()
        {
            Arguments[] newArguments = new Arguments[4];

            newArguments[0] = new Arguments();
            newArguments[0].paramKey = "-c";
            newArguments[0].paramDescription = "Path to config file";
            newArguments[0].isRequired = true;
            newArguments[0].isBool = false;
            newArguments[0].isDefault = true;
            newArguments[0].paramValue = "azuresync.cfg";

            newArguments[1] = new Arguments();
            newArguments[1].paramKey = "-l";
            newArguments[1].paramDescription = "Path to directory log file";
            newArguments[1].isRequired = true;
            newArguments[1].isBool = false;
            newArguments[1].isDefault = true;
            newArguments[1].paramValue = "azuresync.log";

            newArguments[2] = new Arguments();
            newArguments[2].paramKey = "-v";
            newArguments[2].paramDescription = "Verbose output";
            newArguments[2].isRequired = false;
            newArguments[2].isBool = true;
            newArguments[2].isDefault = true;
            newArguments[2].boolValue = false;

            newArguments[3] = new Arguments();
            newArguments[3].paramKey = "";
            newArguments[3].paramDescription = "Path to directory to sync";
            newArguments[3].isRequired = true;
            newArguments[3].isBool = false;
            newArguments[3].isDefault = true;
            newArguments[3].paramValue = "";


            return newArguments;
        }

        static void ShowHelp(Arguments[] arguments)

        {
            foreach (Arguments argument in arguments)
            {
                string helpLine = String.Format("{0} - {1}", argument.paramKey, argument.paramDescription);
                Console.WriteLine(helpLine);
            }
        }

        // Check if all required parameters are provided
        static bool IfAllArgsProvided(Arguments[] arguments)
        {
            bool ifAllArgsOK = true;

            foreach (Arguments argument in arguments)
            {
                if ((argument.isRequired == true) && (argument.isDefault == true))
                {
                    ifAllArgsOK = false;
                }
            }
            return ifAllArgsOK;
        }
    }
}
