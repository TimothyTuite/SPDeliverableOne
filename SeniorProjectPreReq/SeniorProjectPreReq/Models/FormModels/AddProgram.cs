using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeniorProjectPreReq.Models.FormModels
{
    public class AddProgram
    {
        public string schoolName { get; set; }
        public IEnumerable<SelectListItem> ThePrograms { get; set; }

        public IEnumerable<string> SelectedPrograms { get; set; }

        public IEnumerable<string> previousPrograms { get; set; }

        public int schoolID { get; set; }

        public int Year { get; set; }
    }
}