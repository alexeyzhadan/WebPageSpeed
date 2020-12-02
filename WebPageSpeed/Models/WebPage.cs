namespace WebPageSpeed.Models
{
    public class WebPage
    {
        public string Uri { get; set; }
        public double MinResponseTime { get; set; }
        public double MaxResponseTime { get; set; }
    }
}