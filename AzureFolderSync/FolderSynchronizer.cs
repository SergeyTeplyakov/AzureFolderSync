using System;
using System.IO;

namespace AzureFolderSync
{
    public class SynchronizationException : Exception
    {
        public SynchronizationException(string message) : base(message)
        {
        }
    }

    public class FolderSynchronizer
    {
        private readonly FolderSyncConfiguration _configuration;

        public FolderSynchronizer(FolderSyncConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Это может быть статическим методом, но поскольку мы не знаем, что именно нужно.
        // может быть мы сможем сделать retry логику, и тогда экземплярный метод - это очень ок!
        public void Sync()
        {
            // Let's check that folder is valid!

            if (!Directory.Exists(_configuration.InputDir))
            {
                throw new SynchronizationException($"Input folder '{_configuration.InputDir}' does not exists on disk.");
            }

            Console.WriteLine("Syncing...");

            Console.WriteLine("Done!!!");
        }
    }
}