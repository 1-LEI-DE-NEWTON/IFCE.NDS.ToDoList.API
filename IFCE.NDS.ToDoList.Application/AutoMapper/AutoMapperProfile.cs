using AutoMapper;
using NDS_ToDo.Application.DTOs.Assignment;
using NDS_ToDo.Application.DTOs.AssignmentList;
using NDS_ToDo.Domain.Entities;
using NDS_ToDo.Domain.FIlter;

namespace NDS_ToDo.Application.AutoMapper
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AssignmentSearchDto, AssignmentFilter>().ReverseMap();
            CreateMap<AssignmentDto, Assignment>().ReverseMap();
            CreateMap<AddAssignmentDto, Assignment>().ReverseMap();
            CreateMap<EditAssignmentDto, Assignment>().ReverseMap();

            CreateMap<AssignmentListDto, AssignmentList>().ReverseMap();
            CreateMap<AddAssignmentListDto, AssignmentList>().ReverseMap();
            CreateMap<EditAssignmentListDto, AssignmentList>().ReverseMap();

        }
    }  
}
