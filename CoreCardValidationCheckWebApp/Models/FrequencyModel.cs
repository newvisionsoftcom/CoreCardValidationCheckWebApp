using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCardValidationCheckWebApp.Models
{
    public class FrequencyModel
    {
        [Key]
        public int FrequencyId { get; set; }

        [Required]
        [DisplayName("Frequency Name")]
        public string FrequencyName { get; set; }
    }
}
