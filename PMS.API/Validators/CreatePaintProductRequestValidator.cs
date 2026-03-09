using System;
using FluentValidation;
using PMS.API.DTOs;

namespace PMS.API.Validators;

public class CreatePaintProductRequestValidator : AbstractValidator<CreatePaintProductRequest>
{
    public CreatePaintProductRequestValidator()
    {
        RuleFor(cpp => cpp.PaintProductName).NotEmpty().
        WithMessage("Name can not be empty").
        MaximumLength(50).
        WithMessage("Name length must be less than 50");
        
        RuleFor(cpp => cpp.Description).NotEmpty().
        MaximumLength(250).
        WithMessage("Description length must be less than 50");

    }
}
