using ToolManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ToolManager.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToolsController : ControllerBase
    {
        private readonly ILogger<ToolsController> logger;

        public ToolsController(ILogger<ToolsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<ToolResource> Get()
        {
            return new List<ToolResource>{
                new ToolResource{
                    Name = "tool_1",
                    ReadMe = System.IO.File.ReadAllText("test.MD")
                },
                new ToolResource{
                    Name = "tool_2",
                    ReadMe = System.IO.File.ReadAllText("test.MD")
                }

            };
        }
    }
}
