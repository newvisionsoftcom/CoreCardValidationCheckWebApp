using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptLib.Models
{
    public class ComplexietyModel
    {
        [Key]
        public int ComplexietyId { get; set; }

        [Required]
        [DisplayName("Complexiety Name")]
        public string ComplexietyName { get; set; }
    }
}
