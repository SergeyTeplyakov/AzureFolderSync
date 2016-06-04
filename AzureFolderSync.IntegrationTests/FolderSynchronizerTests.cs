using NUnit.Framework;

namespace AzureFolderSync.IntegrationTests
{
    [TestFixture]
    public class FolderSynchronizerTests
    {
        [Test]
        public void SyncFolder1()
        {
            var configuration = new FolderSyncConfiguration("Foo", "Bar", "Baz", "Sample/Folder1");
            var synchronizer = new FolderSynchronizer(configuration);
            synchronizer.Sync();
        }

        [Test]
        public void SyncFolderOnDiskC()
        {
            var configuration = new FolderSyncConfiguration("Foo", "Bar", "Baz", "C:/temp/Sample/Folder1");
            var synchronizer = new FolderSynchronizer(configuration);
            synchronizer.Sync();
        }
    }
}
