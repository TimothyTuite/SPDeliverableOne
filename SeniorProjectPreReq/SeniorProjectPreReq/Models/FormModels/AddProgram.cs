using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeniorProjectPreReq.Models.FormModels
{
    public class AddProgram
    {
        public IEnumerable<SelectListItem> ThePrograms { get; set; }

        public IEnumerable<int> SelectedPrograms { get; set; }

        public int schoolID { get; set; }

        public int Year { get; set; }
    }
}