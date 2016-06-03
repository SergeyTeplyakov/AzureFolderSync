using System;

//Parameters of the program
public  class Params
{


    public static string accountName = "";
    public static string accountKey = "";
    public static string blobName = "";

    public static string folderToSync = "";

    public static bool pathToConfigFound = false;
    public const  string pathToConfigKey = "-p";
    public static string pathToConfig = "AzBlob.cfg";

    public static bool pathToLogKeyFound = false;
    public const string pathToLogKey = "-l";
    public static string pathToLog = "AzBlob.log";




}
