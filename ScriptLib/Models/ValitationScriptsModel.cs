using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptLib.Models
{
    public class ValitationScriptsModel
    {
        [Key]
        public int ScriptId { get; set; }

        [DisplayName("Script Name")]
        public string ScriptName { get; set; }

        [DisplayName("Script Path")]
        public string ScriptPath { get; set; }


        public int TaskActivityId { get; set; }

        [DisplayName("Task Activity Name")]
        public string TaskActivityName { get; set; }


        //[DisplayName("Validation Steps Name")]
        //public string ValidationSteps { get; set; }
        public List<ValidationStepsModel> ValidationStepsList { get; set; }


        public List<IFormFile> fileData;
    }
}
