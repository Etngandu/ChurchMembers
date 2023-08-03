using ENB.Church.Members.Entities;
using FluentValidation;

namespace ENB.Church.Members.MVC.Models
{
    public class CreateAndEditMinistry
    {
        public int Id { get; set; }
        public string MinistryCode { get; set; } = string.Empty;
        public string MinistryName { get; set; } = string.Empty;
        public string Ministry_Otherdetails { get; set; } = string.Empty;
    }
    public class CreateAndEditMinistryValidator : AbstractValidator<CreateAndEditMinistry>
    {
        public CreateAndEditMinistryValidator()
        {
            RuleFor(x => x.MinistryCode)
            .NotEmpty()
            .WithMessage("MinistryCode  can't be empty");

            RuleFor(x => x.MinistryName)
             .NotEmpty().WithMessage("MinistryName  can't be empty");
            

            RuleFor(x => x.Ministry_Otherdetails)
            .NotEmpty().WithMessage("Ministry_Otherdetails  can't be empty");

        }

    }
}
