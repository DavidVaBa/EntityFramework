using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMVC.Models
{
    public class Filter
    {

        public string search { get; set; }

        public string option { get; set; }

        public string order { get; set; }
    }
}
