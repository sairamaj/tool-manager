using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ToolManager.Server.Models;

namespace ToolManager.Server.Repository
{
    public interface IStorageManager
    {
        Task CreateNewTool(ToolInfo toolInfo);
        IAsyncEnumerable<ToolInfo> Get();
        Task<string> GetReadMe(string toolName);
        Task Upload(string toolName, string name, Stream stream);
    }
}