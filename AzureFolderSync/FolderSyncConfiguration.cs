using System;

namespace AzureFolderSync
{
    // TODO: add comment!
    public class FolderSyncConfiguration
    {
        public FolderSyncConfiguration(string accountName, string accountKey, string blobName, string inputDir)
        {
            // Эти исключения никогда не обрабатываются!!!
            if (string.IsNullOrEmpty(accountName)) throw new ArgumentNullException(nameof(accountName));
            if (string.IsNullOrEmpty(accountKey)) throw new ArgumentNullException(nameof(accountKey));
            if (string.IsNullOrEmpty(blobName)) throw new ArgumentNullException(nameof(blobName));
            if (string.IsNullOrEmpty(inputDir)) throw new ArgumentNullException(nameof(inputDir));

            AccountName = accountName;
            AccountKey = accountKey;
            BlobName = blobName;
            InputDir = inputDir;
        }

        public string AccountName { get; }
        public string AccountKey { get; }
        public string BlobName { get; }
        public TimeSpan Timeout { get; }
        public int RetryCount { get; }
        public string InputDir { get; }
    }
}