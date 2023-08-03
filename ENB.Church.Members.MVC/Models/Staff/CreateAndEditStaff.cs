using ENB.Church.Members.Entities.Collections;
using ENB.Church.Members.Entities;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace ENB.Church.Members.MVC.Models
{
    public class CreateAndEditStaff
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Other_details { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public Address? AddressStaff { get; set; }
        public StaffSkills? StaffSkills { get; set; }
        public MinistriesStaffs? MinistriesStaffs { get; set; }
    }
    public class CreateAndEditStaffValidator : AbstractValidator<CreateAndEditStaff>
    {
        public CreateAndEditStaffValidator()
        {
            RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("FirstName  can't be empty");

            RuleFor(x => x.LastName)
           .NotEmpty().WithMessage("LastName  can't be empty");

            RuleFor(x => x.EmailAddress)
           .NotEmpty().WithMessage("Mail")
           .EmailAddress();

            RuleFor(x => x.Gender)
           .NotEqual(Gender.None)
           .WithMessage("Gender  can't be None");

            RuleFor(x => x.PhoneNumber)
           .NotEmpty().WithMessage("PhoneNumber  can't be empty");

            RuleFor(x => x.DateOfBirth)
           .LessThan(x => DateTime.Now)
           .WithMessage($"DateOfBirth should be less than {DateTime.Today}");

            RuleFor(x => x.Other_details)
          .NotEmpty().WithMessage("Other_details  can't be empty");

        }

    }
}
