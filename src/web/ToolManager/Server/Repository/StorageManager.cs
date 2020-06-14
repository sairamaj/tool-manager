using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using ToolManager.Server.Models;

namespace ToolManager.Server.Repository
{
    class StorageManager : IStorageManager
    {
        private readonly ConnectionInfo connectionInfo;
        private const string ToolStoageResourceGroup = "toolmanager";

        public StorageManager(IOptions<ConnectionInfo> connectionInfo)
        {
            if (connectionInfo is null)
            {
                throw new System.ArgumentNullException(nameof(connectionInfo));
            }

            this.connectionInfo = connectionInfo.Value ?? throw new System.ArgumentNullException(nameof(connectionInfo));
        }

        public CloudStorageAccount StorageAccount => CloudStorageAccount.Parse(this.connectionInfo.ToolStorageKey);

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

        public async IAsyncEnumerable<ToolInfo> Get()
        {
            await Task.Delay(0);
            var blobClient = this.StorageAccount.CreateCloudBlobClient();
            BlobContinuationToken continuationToken = null;
            //var toolPrefix = null;
            do
            {
                var result = await blobClient.ListContainersSegmentedAsync(
                    null,
                    ContainerListingDetails.Metadata,
                    10,
                    continuationToken,
                    null,
                    null);

                foreach (var container in result.Results)
                {
                    System.Console.WriteLine(container);
                    yield return new ToolInfo { Name = container.Name };
                }

            } while (continuationToken != null);
        }

        public async Task<string> GetReadMe(string toolName)
        {
            var blobClient = this.StorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(toolName);
            var readMeBlob = container.GetBlockBlobReference("readme.md");
            return await readMeBlob.DownloadTextAsync();
        }

        public Task Upload(string toolName, string name, Stream stream)
        {
            throw new System.NotImplementedException();
        }
    }
}