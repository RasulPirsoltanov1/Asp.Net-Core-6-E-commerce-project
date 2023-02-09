using FluentValidation;
using MultiShop.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Business.Validators.Course
{
    public class CategoryCreateVmValidator : AbstractValidator<CategoryCreateVM>
    {

        public CategoryCreateVmValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Must not Null")
                .MaximumLength(150).WithMessage("Maximum length:150");
        }
    }
}
