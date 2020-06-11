using Markdig;

namespace ToolManager.Shared
{
    public class ToolResource
    {
        public string Name { get; set; }
        public string ReadMe { get; set; }
        public string MarkdownInfo
        {
            get
            {
                return Markdown.ToHtml(this.ReadMe);
            }
        }
    }
}