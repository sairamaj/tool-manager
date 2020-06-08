using System.ComponentModel.DataAnnotations;

namespace ToolManager.Shared
{
    public class NewToolInfo
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}