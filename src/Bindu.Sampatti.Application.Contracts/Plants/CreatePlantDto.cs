using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bindu.Sampatti.Plants
{
    public class CreatePlantDto
    {
        [Required]
        [StringLength(PlantConsts.MaxNameLength)]
        public string Name { get; set; }
        [Required]
        public Guid Location { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }
    }
}
