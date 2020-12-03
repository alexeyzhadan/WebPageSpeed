using System.ComponentModel.DataAnnotations;

namespace WebPageSpeed.Models
{
    public class WebPage : IdentifiableEntity
    {
        [Required]
        public string Path { get; set; }

        [Required]
        public double MinResponseTime { get; set; }

        [Required]
        public double MaxResponseTime { get; set; }

        [Required]
        public long WebSiteId { get; set; }

        public WebSite WebSite { get; set; }
    }
}