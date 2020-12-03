using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPageSpeed.Models
{
    [NotMapped]
    public class AnalysisWebPage : IdentifiableEntity
    {
        [Required]
        public string Uri { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double MinResponseTime { get; set; }

        [Required]
        public double MaxResponseTime { get; set; }
    }
}