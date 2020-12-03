namespace WebPageSpeed.Models.ViewModel
{
    public class WebPageResultViewModel
    {
        public string Host { get; set; }
        public string Path { get; set; }
        public double MinResponseTime { get; set; }
        public double MaxResponseTime { get; set; }
    }
}