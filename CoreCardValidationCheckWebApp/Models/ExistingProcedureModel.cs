using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCardValidationCheckWebApp.Models
{
    public class ExistingProcedureModel
    {
        [Key]
        public int ExistingProcedureId { get; set; }

        [Required]
        [DisplayName("Existing Procedure Name")]
        public string ExistingProcedureName { get; set; }
    }
}
