using ToolManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToolManager.Server.Repository;
using ToolManager.Server.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ToolManager.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToolsController : ControllerBase
    {
        private readonly ILogger<ToolsController> logger;
        private readonly IStorageManager storageManager;
        private readonly IWebHostEnvironment environment;

        public ToolsController(
            IWebHostEnvironment environment,
            ILogger<ToolsController> logger,
            IStorageManager storageManager)
        {
            this.environment = environment;
            this.logger = logger;
            this.storageManager = storageManager ?? throw new ArgumentNullException(nameof(storageManager));
        }

        [HttpGet]
        [Route("/tools")]
        public async Task<IEnumerable<ToolResource>> Get()
        {
            var list = new List<ToolResource>();
            await foreach (var tool in this.storageManager.Get())
            {
                var resource = new ToolResource { Name = tool.Name };
                list.Add(resource);
                resource.ReadMe = await this.storageManager.GetReadMe(resource.Name);
            }

            return list;
        }

        [HttpGet]
        [Route("/tools/download/{name}/zip")]
        public IActionResult DownloadToolZip(string name)
        {
            var filePath = @"c:\temp\test.zip";
            return PhysicalFile(filePath, MimeTypes.GetMimeType(filePath), Path.GetFileName(filePath));
        }

        [HttpPost]
        [Route("/tools/upload/{name}/{filename}")]
        public async Task Post(string name, string filename)
        {
            System.Console.WriteLine("__________________________");
            System.Console.WriteLine($"Name: {name}");
            System.Console.WriteLine($"filename: {filename}");
            System.Console.WriteLine("__________________________");
            if (HttpContext.Request.Form.Files.Any())
            {
                foreach (var file in HttpContext.Request.Form.Files)
                {
                    System.Console.WriteLine("=========================");
                    System.Console.WriteLine($"FileName: {file.FileName}");
                    System.Console.WriteLine($"ContentRootPath: {environment.ContentRootPath}");
                    System.Console.WriteLine("=========================");
                    var path = Path.Combine(environment.ContentRootPath, "uploads", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
        }
    }
}
