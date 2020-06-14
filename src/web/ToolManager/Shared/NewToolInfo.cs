using System.ComponentModel.DataAnnotations;

namespace ToolManager.Shared
{
    public class NewToolInfo
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Tags { get; set; }
        public string SourceUrl {get; set;}

        public override string ToString(){
            var info = string.Empty;

            foreach(var prop in this.GetType().GetProperties()){
                info += $"{prop.Name}: {prop.GetValue(this)}\r\n";
            }

            return info;
        }
    }
}