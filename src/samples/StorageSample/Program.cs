using System;
using StorageSample.Models;

namespace StorageSample
{
//     {
//   "appId": "db625714-22d6-4ad3-b36a-80d7811d6dbd",
//   "displayName": "azure-cli-2020-06-09-15-58-06",
//   "name": "http://azure-cli-2020-06-09-15-58-06",
//   "password": "08c8dfc1-8487-4e93-a30a-b65c90556a30",
//   "tenant": "4925b807-9380-4135-93cc-9c23aa7c411b"
// }
    class Program
    {
        static void Main(string[] args)
        {
            var con = new ConnectionInfo{
                ClientId = "db625714-22d6-4ad3-b36a-80d7811d6dbd",
                ClientSecret = "08c8dfc1-8487-4e93-a30a-b65c90556a30",
                TenantId = "4925b807-9380-4135-93cc-9c23aa7c411b",
                SubscriptionId = "dfbed381-5580-4a1a-902c-2ec4b3eaef27",
                ToolStorageKey = "DefaultEndpointsProtocol=https;AccountName=toolmanager;AccountKey=ZdHS3iJ8xzlamsvQmHnwrZIs4K+iDJsEdZIqzuWDAKNRU5o2tftvkub0yHfusn2bQatPOoD1d7JyA4vRl2o5uw==;EndpointSuffix=core.windows.net"
            };
            var manager  = new StorageManager(con);
            // manager.CreateNewTool(new ToolInfo{
            //     Name = "tool3"
            // }).Wait();

            System.Console.WriteLine( manager.GetReadMe("tool3").Result );
        }
    }
}
