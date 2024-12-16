using AutoMapper;
using GLIMPSE.Application.Dtos;
using GLIMPSE.Domain.Models;

namespace GLIMPSE.Application.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CardDTO, Card>()
            .ReverseMap();      

            CreateMap<BoardDTO, Board>()
            .ReverseMap();  

            CreateMap<CheckboxDTO, Checkbox>()
            .ReverseMap();  

            CreateMap<LaneDTO, Lane>()
            .ReverseMap();  

            CreateMap<ProjectDTO, Project>()
            .ReverseMap();  

            CreateMap<RepositoryDTO, Repository>()
            .ReverseMap();  

            CreateMap<RoleDTO, Role>()
            .ReverseMap();  

            CreateMap<TagDTO, Tag>()
            .ReverseMap();  

            CreateMap<UserDTO, User>()
            .ReverseMap();

            CreateMap<BlobFileDTO, BlobFile>()
            .ReverseMap();   
        }
    }
}
