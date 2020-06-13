using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using ToolManager.Server.Models;

namespace ToolManager.Server.Repository
{
    class FakeStorageManager : IStorageManager
    {
        private readonly ConnectionInfo connectionInfo;
        private const string ToolStoageResourceGroup = "toolmanager";

        public FakeStorageManager(IOptions<ConnectionInfo> connectionInfo)
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
            yield return new ToolInfo { Name = "Tool1" };
            yield return new ToolInfo { Name = "Tool2" };
            yield return new ToolInfo { Name = "Tool3" };
        }

        public async Task<string> GetReadMe(string toolName)
        {
            return File.ReadAllText(@"c:\temp\readme.md");
        }

    }
}