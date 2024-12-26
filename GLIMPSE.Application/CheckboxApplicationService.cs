using AutoMapper;
using GLIMPSE.Application.Dtos;
using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class CheckboxApplicationService : BaseApplicationService<Checkbox, CheckboxDTO>, ICheckboxApplicationService
    {
        public CheckboxApplicationService(IMapper mapper, ICheckboxService checkboxService): base(mapper, checkboxService)
        {
        }
    }
}
