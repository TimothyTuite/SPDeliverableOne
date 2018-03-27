using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeniorProjectPreReq.Models.FormModels
{
    public class ProgramsCheckBoxList
    {
        public int year { get; set; }
        public IList<CheckBoxes> AvailablePrograms { get; set; }
        public IList<string> preSelectids { get; set; }
        public ProgramsCheckBoxList()
        {
            AvailablePrograms = new List<CheckBoxes>();
            preSelectids = new List<string>(); 
        }
    }
}