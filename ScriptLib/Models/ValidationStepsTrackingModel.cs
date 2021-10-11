using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptLib.Models
{
    public class ValidationStepsTrackingModel
    {
        public List<ValidationStepsModel> ValidationStepsList { get; set; }

        //[Key]
        //public int TaskActivityId { get; set; }
        //public int GroupId { get; set; }

        //[DisplayName("Task Activity Name")]
        //public string TaskActivityName { get; set; }

        //public int CategoryId { get; set; }
        //public int ComplexietyId { get; set; }
        //public int ExistingProcedureId { get; set; }
        //public int FrequencyId { get; set; }


        //public string CategoryName { get; set; }
        //public string ComplexietyName { get; set; }
        //public string ExistingProcedureName { get; set; }
        //public string FrequencyName { get; set; }

    }
}
