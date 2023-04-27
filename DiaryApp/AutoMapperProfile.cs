using AutoMapper;
using DataTransferObjects;
using DiaryAppDbContext;

namespace DiaryApp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<DailyNoteDTO, DailyNote>().ReverseMap();
        }
    }
    
}
