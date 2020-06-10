using System.Threading.Tasks;
using StorageSample.Models;

namespace StorageSample
{
    interface IStorageManager
    {
        Task CreateNewTool(ToolInfo toolInfo);
    }
}