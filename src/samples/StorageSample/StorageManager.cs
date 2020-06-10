using System.Threading.Tasks;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.Storage.Fluent.Models;
using Microsoft.WindowsAzure.Storage;
using StorageSample.Models;

namespace StorageSample
{
    class StorageManager : IStorageManager
    {
        private readonly ConnectionInfo connectionInfo;
        private const string ToolStoageResourceGroup = "toolmanager";

        public StorageManager(ConnectionInfo connectionInfo)
        {
            this.connectionInfo = connectionInfo ?? throw new System.ArgumentNullException(nameof(connectionInfo));
        }

        public async Task CreateNewTool(ToolInfo toolInfo)
        {
            var blobClient = this.StorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(toolInfo.Name);
            await container.CreateIfNotExistsAsync();
            var readMeBlob = container.GetBlockBlobReference("readme.md");
            await readMeBlob.UploadFromFileAsync(@"c:\temp\tool3\readme.md");
            var zipBlob = container.GetBlockBlobReference("bin.zip");
            await zipBlob.UploadFromFileAsync(@"C:\temp\tool3\bin\tool3bin.zip");
        }

        public async Task<string> GetReadMe(string name){

            var blobClient = this.StorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(name);
            var readMeBlob = container.GetBlockBlobReference("readme.md");
            return await readMeBlob.DownloadTextAsync();
        }

        public CloudStorageAccount StorageAccount => CloudStorageAccount.Parse(this.connectionInfo.ToolStorageKey);
    }
}