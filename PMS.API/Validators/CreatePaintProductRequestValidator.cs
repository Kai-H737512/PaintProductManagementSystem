using System;
using FluentValidation;
using PMS.API.DTOs;

namespace PMS.API.Validators;

public class CreatePaintProductRequestValidator : AbstractValidator<CreatePaintProductRequest>
{
    public CreatePaintProductRequestValidator()
    {
        RuleFor(cpp => cpp.PaintProductName).MaximumLength(50)
        .WithMessage("Name length must be less than 50");

        RuleFor(cpp => cpp.Description).MaximumLength(250);
    }
}
