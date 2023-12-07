using AutoMapper;
using LastTodoApp.Domain.Dto;
using LastTodoApp.Domain.Entities;

namespace LastTodoApp.Web.AutoMapperProfile
{
    public class AutomapperProfile : Profile
    {

        public AutomapperProfile()
        {
            CreateMap<Domain.Entities.Task, TaskDto>().ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore()); ;
        }


    }
}
