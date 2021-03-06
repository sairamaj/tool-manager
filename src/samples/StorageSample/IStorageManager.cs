using System.Collections.Generic;
using System.Threading.Tasks;
using StorageSample.Models;

namespace StorageSample
{
    interface IStorageManager
    {
        Task CreateNewTool(ToolInfo toolInfo);
        IAsyncEnumerable<ToolInfo> Get();
        Task<string> GetReadMe(string toolName);
    }
}