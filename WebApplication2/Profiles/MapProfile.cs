using AutoMapper;
using WebApplication2.Data.Entities;
using WebApplication2.Dtos.DepartmentDtos;
using WebApplication2.Dtos.DoctorDtos;
using WebApplication2.Dtos.StudentDtos;
using WebApplication2.Extensions;

namespace WebApplication2.Profiles;

public class MapProfile : Profile
{
    private readonly IHttpContextAccessor _contextAccessor;

    public MapProfile(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
        var builder = new UriBuilder();
        builder.Port = (int)_contextAccessor.HttpContext.Request.Host.Port;
        builder.Scheme = _contextAccessor.HttpContext.Request.Scheme;
        builder.Host = _contextAccessor.HttpContext.Request.Host.Host;
        var url = builder.Uri.AbsoluteUri;
        CreateMap<Department, DepartmentReturnDto>()
            .ForMember(d => d.Image, map => map.MapFrom(d => url + "uploads/" + d.Image));
        CreateMap<Doctor, DoctorInDepartmentReturnDto>();
        CreateMap<DepartmentCreateDto, Department>()
            .ForMember(d => d.Image, map => map.MapFrom(d => d.Image.SaveFileAsync().Result));
        CreateMap<DepartmentUpdateDto, Department>()
            .ForMember(d => d.Image, map => map.MapFrom(d => d.Image != null ? d.Image.SaveFileAsync().Result : null));
        CreateMap<Doctor, DoctorReturnDto>();
        CreateMap<DoctorCreateDto, Doctor>();
    }
}
