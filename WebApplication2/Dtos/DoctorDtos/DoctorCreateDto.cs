using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data.Data;

namespace WebApplication2.Dtos.DoctorDtos;

public class DoctorCreateDto
{
    public string Name { get; set; }
    public int Experience { get; set; }
    public int DepartmentId { get; set; }
}

public class DoctorCreateDtoValidator : AbstractValidator<DoctorCreateDto>
{
    public DoctorCreateDtoValidator()
    {
        RuleFor(d => d.Name).NotEmpty().MaximumLength(20);
        RuleFor(d => d.Experience).NotEmpty().InclusiveBetween(1, 30);
        RuleFor(d => d.DepartmentId).NotEmpty();
    }
}
