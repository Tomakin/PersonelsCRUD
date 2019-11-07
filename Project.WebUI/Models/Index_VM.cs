using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebUI.Models
{
    public class Index_VM
    {
        public List<Personel> personels { get; set; }
        public Personel personel { get; set; }

        public List<SelectListItem> ContactTypes { get; set; }

        public List<SelectListItem> Countries { get; set; }
    }
}
