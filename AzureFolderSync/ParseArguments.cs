using System;


namespace AzureFolderSync
{
    public class ParseArguments

    {
        public ParseArguments()
        {
        }

        // Compare command line arguments with based parameters class
        public static Arguments[] ReadParams(string[] args, Arguments[] basedParams)
        {
            int argNumber = 0;
            bool valueIsFound = false;

            do
            {
                // We found a key started with '-'
                if (args[argNumber][0] == '-') 
                {

                    for (int paramNumber = 0; paramNumber < basedParams.Length; paramNumber++)
                    {
                        if (args[argNumber].ToLower() == basedParams[paramNumber].paramKey)
                        {
                            // Boolean parameter
                            if (basedParams[paramNumber].isBool == true)
                            {
                                if (basedParams[paramNumber].isDefault == true)
                                {

                                    basedParams[paramNumber].boolValue = true;
                                    basedParams[paramNumber].isDefault = false;
                                    break;
                                }
                                else
                                {
                                    throw new ArgumentOutOfRangeException("Duplicated argument " + basedParams[paramNumber].paramKey);
                                }
                            }
                            else
                            {
                                // String parameter
                                if (argNumber < args.Length - 1)   // If the value is exists ..
                                {
                                    if (args[argNumber + 1][0] == '-')  // If the next argument is also a key - exception!
                                    {
                                        throw new ArgumentOutOfRangeException("Missed value for the key" + basedParams[paramNumber].paramKey);
                                        
                                    }
                                    else
                                    {
                                        if (basedParams[paramNumber].isDefault == true)
                                        {
                                            basedParams[paramNumber].paramValue = args[argNumber + 1];
                                            basedParams[paramNumber].isDefault = false;
                                            valueIsFound = true;
                                            break;
                                        }
                                        else
                                        {
                                            throw new ArgumentOutOfRangeException("Duplicated argument " + basedParams[paramNumber].paramKey);
                                        }

                                    }
                                }
                                else           
                                {
                                    throw new ArgumentOutOfRangeException("Missed value for the key" + basedParams[paramNumber].paramKey);
                                    
                                }
                            }
                        }
                    }

                }
                else                   // We found an argument without '-'. 
                {
                    if (! valueIsFound)    // If it is not a value for previously found key - it's maybe a value for empty key
                    {
                        for (int paramNumber = 0; paramNumber < basedParams.Length; paramNumber++)
                        {
                            // Looking for a parameter with empty key
                            if (basedParams[paramNumber].paramKey == "")
                            {
                                // Check if the value is already found
                                if (basedParams[paramNumber].isDefault == true)
                                {

                                    basedParams[paramNumber].paramValue = args[argNumber];
                                    basedParams[paramNumber].isDefault = false;
                                }
                                else
                                {
                                    throw new ArgumentOutOfRangeException("Duplicated argument "+ basedParams[paramNumber].paramKey);
                                }

                            }
                        }
                    }
                    valueIsFound = false;
                }

                argNumber++;
            }
            while (argNumber < args.Length);

            return basedParams;

        }

    }
}
