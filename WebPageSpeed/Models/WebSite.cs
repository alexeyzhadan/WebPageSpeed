﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebPageSpeed.Models
{
    public class WebSite : IdentifiableEntity
    {
        [Required]
        public string Authority { get; set; }

        [Required]
        public DateTime DateOfAnalysis { get; set; }

        public List<WebPage> WebPages { get; set; }
    }
}