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
                if (string.IsNullOrWhiteSpace(this.ReadMe))
                {
                    return string.Empty;
                }
                return Markdown.ToHtml(this.ReadMe);
            }
        }
    }
}