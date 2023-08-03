using ENB.Church.Members.Entities.Collections;
using ENB.Church.Members.Entities;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace ENB.Church.Members.MVC.Models
{
    public class CreateAndEditMember
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string EmailAddress { get; set; } = String.Empty;
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; } = String.Empty;

        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }
        public MembersActivities? MemberActivities { get; set; }
        public MembersContributions? MemberContributions { get; set; }
        public string Other_details { get; set; } = String.Empty;       
        public Address? AddressCustomer { get; set; }
    }

    public class CreateAndEditMemberValidator : AbstractValidator<CreateAndEditMember>
    {
        public CreateAndEditMemberValidator()
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
           .WithMessage($"DateOfBirth should be less than {DateTime.Now}");

            RuleFor(x => x.Other_details)
          .NotEmpty().WithMessage("Other_details  can't be empty");

        }

    }
}
