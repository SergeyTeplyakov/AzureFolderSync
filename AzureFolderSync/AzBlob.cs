using System;

using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using System.IO;







public class AzBlob

{

    public static string blobName = "";
    public static string blobKey = "";
    public static string accountName = "";
    public static string syncDir = "";


   
    public AzBlob()
    {

    }

    public static CloudBlobContainer InitBlob(string accountName, string accountKey, string blobName)
    {
        string connectionString = "DefaultEndpointsProtocol=https;AccountName=" + accountName + ";AccountKey=" + accountKey;

        // Parse the connection string and return a reference to the storage account.
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

        CloudBlobContainer container = blobClient.GetContainerReference(blobName);


        try
        {
            bool ifExists = container.Exists();
            if (!ifExists)
            {
        //        logBox.AppendText("Blob container doesn't exist! \n");
                return null;
            }
        }
        catch
        {
      //      logBox.AppendText("Wrong Azure parameters! \n");
            return null;
        }

        return container;
    }



    public static string CutName(string blobName)

    {

        blobName = blobName.Trim('/');
        string[] cuttedList = blobName.Split(new char[] { '/', '\\' });
        return cuttedList[cuttedList.Length - 1];

    }


    public static void CompareBlobWithFolder(IEnumerable<IListBlobItem> containerList, string pathToDir)
    {

        foreach (IListBlobItem item in containerList)
        {
            if (item.GetType() == typeof(CloudBlockBlob))
            {
                CloudBlockBlob blob = (CloudBlockBlob)item;
    //            logBox.AppendText("Checking " + blob.Name + "\n");

                if (File.Exists(pathToDir + "\\" + CutName(blob.Name)))
                {
  //                  logBox.AppendText(blob.Name + " blockblob exixts!\n");
                }
                else
                {
   //                 logBox.AppendText(blob.Name + " file doesn't exixts! Deleting blob! \n");
                    blob.Delete();
                }
            }
            else if (item.GetType() == typeof(CloudPageBlob))
            {
                CloudPageBlob pageBlob = (CloudPageBlob)item;
    //            logBox.AppendText("Checking " + pageBlob.Name + "\n");

                if (File.Exists(pathToDir + "\\" + CutName(pageBlob.Name)))
                {
   //                 logBox.AppendText(pageBlob.Name + " pageblob exixts!\n");
                }
                else
                {
  //                  logBox.AppendText(pageBlob.Name + " file doesn't exixts! Deleting blob! \n");
                    pageBlob.Delete();
                }
            }
            else if (item.GetType() == typeof(CloudBlobDirectory))
            {
                CloudBlobDirectory directory = (CloudBlobDirectory)item;

   //             logBox.AppendText("Checking " + directory.Prefix.Trim('/') + "\n");

                CompareBlobWithFolder(directory.ListBlobs(false), pathToDir + "\\" + directory.Prefix.Trim('/'));


            }
        }
    }


    // Process all files in the directory passed in, recurse on any directories 
    // that are found, and process the files they contain.
    public static void ProcessDirectory(string targetDirectory)
    {
        // Process the list of files found in the directory.
        string[] fileEntries = Directory.GetFiles(targetDirectory);
        foreach (string fileName in fileEntries)
            ProcessFile(fileName, targetDirectory);

        // Recurse into subdirectories of this directory.
        string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
        foreach (string subdirectory in subdirectoryEntries)
            ProcessDirectory(subdirectory);
    }

    // Insert logic for processing found files here.
    public static void ProcessFile(string path, string targetDirectory)
    {
        
        CloudBlobContainer container = AzBlob.InitBlob(accountName, blobKey, blobName);

        UploadFileToBlob(container, path, MakePathToBlob(path, syncDir));

    }



    public static string MakePathToBlob(string filePath, string homeDir)
    {
        string blobPath = "";

        if (filePath.Trim().StartsWith(homeDir))
        {
            int lastPos = homeDir.Length;
            blobPath = filePath.Substring(lastPos + 1);
        }
        else
            throw new ArgumentException("Wrong file path", "original");

        return blobPath;
    }
    public static void UploadFileToBlob(CloudBlobContainer container, string path, string blobPath)

    {

        CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobPath);
        if (!blockBlob.Exists())
        {
            blockBlob.UploadFromFile(path);
 //           logBox.AppendText(path + " uploaded! \n");
        }
  //      else
//            logBox.AppendText(path + " Already exists! \n");

    }
}
