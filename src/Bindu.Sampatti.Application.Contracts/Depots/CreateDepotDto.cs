    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bindu.Sampatti.Depots
{
    class CreateDepotDto
    {
        [Required]
        [StringLength(DepotConsts.MaxNameLength)]
        public string Name { get; set; }
        [Required]
        public Guid Location { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }
    }
}
