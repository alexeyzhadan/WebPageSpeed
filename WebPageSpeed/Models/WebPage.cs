using System;
using System.ComponentModel.DataAnnotations;

namespace WebPageSpeed.Models
{
    public class WebPage : IdentifiableEntity
    {
        [Required]
        public string Uri { get; set; }

        [Required]
        public double MinResponseTime { get; set; }

        [Required]
        public double MaxResponseTime { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}