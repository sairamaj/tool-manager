using System.Collections.Generic;
using System.IO;
using System.Text;
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
        private const string ToolsDirectory = @"c:\temp\toolmanager";

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
            var newTool = Path.Combine(ToolsDirectory, toolInfo.Name);
            Directory.CreateDirectory(newTool);
            var metaData = $"author=foo|description={toolInfo.Description}|tags={toolInfo.Tags}";
            var metadatFile = Path.Combine(newTool, "metadata.txt");
            await File.WriteAllTextAsync(metadatFile, metaData, Encoding.UTF8);
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