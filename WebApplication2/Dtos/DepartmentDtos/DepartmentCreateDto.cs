using FluentValidation;
using WebApplication2.Extensions;

namespace WebApplication2.Dtos.DepartmentDtos;

public class DepartmentCreateDto
{
    public string Name { get; set; }
    public int Limit { get; set; }
    public IFormFile Image { get; set; }
}

public class DepartmentCreateDtoValidator : AbstractValidator<DepartmentCreateDto>
{
    public DepartmentCreateDtoValidator()
    {
        RuleFor(d => d.Name).NotEmpty().MaximumLength(20);
        RuleFor(d => d.Limit).NotEmpty().InclusiveBetween(1, 50);
        RuleFor(d => d.Image).NotEmpty();
        RuleFor(d => d.Image).Custom((i, context) =>
        {
            if (i == null)
            {
                context.AddFailure("Image is required");
                return;
            }
            if (!i.IsImage()) context.AddFailure("Invalid format");
            if (i.DoesSizeExceed(100)) context.AddFailure("File size is too large");
        });
    }
}
