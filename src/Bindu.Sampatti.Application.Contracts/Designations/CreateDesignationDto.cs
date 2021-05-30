using System.ComponentModel.DataAnnotations;

namespace Bindu.Sampatti.Designations
{
    public class CreateDesignationDto
    {
        [Required]
        [StringLength(DesignationConsts.MaxNameLength)]
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }
    }
}
