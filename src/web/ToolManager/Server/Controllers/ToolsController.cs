using ToolManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToolManager.Server.Repository;
using ToolManager.Server.Models;

namespace ToolManager.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToolsController : ControllerBase
    {
        private readonly ILogger<ToolsController> logger;
        private readonly IStorageManager storageManager;

        public ToolsController(
            ILogger<ToolsController> logger,
            IStorageManager storageManager)
        {
            this.logger = logger;
            this.storageManager = storageManager ?? throw new ArgumentNullException(nameof(storageManager));
        }

        [HttpGet]
        [Route("/tools")]
        public async Task<IEnumerable<ToolResource>> Get()
        {
            var list =  new List<ToolResource>();
            await foreach( var tool in  this.storageManager.Get() )
            {
                var resource = new ToolResource{ Name = tool.Name};
                list.Add(resource);
                resource.ReadMe = await this.storageManager.GetReadMe(resource.Name);
            }

            return list;
        }

        [HttpGet]
        [Route("/tools/download/{name}/zip")]
        public async Task<string> DownloadToolZip(string name)
        {
            return await Task.FromResult( $"{name} zip will be here" );
        }
    }
}
