using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bindu.Sampatti.Locations
{
    public class CreateLocationDto 
    {
        [Required]
        [StringLength(LocationConsts.MaxNameLength)]
        public string Name { get; set; }
        public string Notes { get; set; }
        [Required]
        public bool IsEnabled { get; set; }
    }
}
