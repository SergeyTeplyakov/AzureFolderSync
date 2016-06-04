using System.Configuration;

namespace AzureFolderSync
{
    public class Arguments
    {

        public string paramKey;
        public bool isRequired;

        public bool isBool;
        public bool isDefault; 

        public string paramDescription;
        public string paramValue;
        public bool boolValue;

        // TODO: нужно вынести configuration manager из аргументов.
        // склеивать аргументы нужны на другом уровне
        // примерно так:
        public static Arguments[] InitArguments()
        {
            Arguments[] newArguments = new Arguments[5];

            newArguments[0] = new Arguments();
            newArguments[0].paramKey = "-accountname";
            newArguments[0].paramDescription = "Azure account name";
            newArguments[0].isRequired = false;
            newArguments[0].isBool = false;
            newArguments[0].isDefault = true;
            newArguments[0].paramValue = ConfigurationManager.AppSettings.Get("accountName");

            newArguments[1] = new Arguments();
            newArguments[1].paramKey = "-accountkey";
            newArguments[1].paramDescription = "Key to azure container";
            newArguments[1].isRequired = false;
            newArguments[1].isBool = false;
            newArguments[1].isDefault = true;
            newArguments[1].paramValue = ConfigurationManager.AppSettings.Get("accountKey"); 

            newArguments[2] = new Arguments();
            newArguments[2].paramKey = "-blobname";
            newArguments[2].paramDescription = "Name of the blob ";
            newArguments[2].isRequired = false;
            newArguments[2].isBool = false;
            newArguments[2].isDefault = true;
            newArguments[2].boolValue = false;
            newArguments[2].paramValue = ConfigurationManager.AppSettings.Get("blobName");

            newArguments[3] = new Arguments();
            newArguments[3].paramKey = "-dir";
            newArguments[3].paramDescription = "Dir to sync";
            newArguments[3].isRequired = false;
            newArguments[3].isBool = false;
            newArguments[3].isDefault = true;
            newArguments[3].paramValue = ConfigurationManager.AppSettings.Get("dirToSync");

            newArguments[4] = new Arguments();
            newArguments[4].paramKey = "-log";
            newArguments[4].paramDescription = "Path to log file";
            newArguments[4].isRequired = false;
            newArguments[4].isBool = false;
            newArguments[4].isDefault = true;
            newArguments[4].paramValue = ConfigurationManager.AppSettings.Get("logFile");

            return newArguments;
        }

      
        // Check if all required parameters are provided
       public  static bool IfAllArgsProvided(Arguments[] arguments)
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
