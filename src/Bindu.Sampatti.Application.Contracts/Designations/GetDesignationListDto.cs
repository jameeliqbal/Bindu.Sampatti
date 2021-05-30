using Volo.Abp.Application.Dtos;

namespace Bindu.Sampatti.Designations
{
    public class GetDesignationListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
