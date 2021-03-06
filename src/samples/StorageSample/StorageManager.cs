using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.Storage.Fluent.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
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

    }
}