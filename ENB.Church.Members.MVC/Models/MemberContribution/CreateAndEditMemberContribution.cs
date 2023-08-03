using ENB.Church.Members.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ENB.Church.Members.MVC.Models
{
    public class CreateAndEditMemberContribution: IValidatableObject
    {
        public int Id { get; set; }
        public Ministry? Ministry { get; set; }
        public int MinistryId { get; set; }
        public Member? Member { get; set; }
        public int MemberId { get; set; }
        public Ref_payment_method Payment_Method { get; set; }
        public decimal Contribution_amount { get; set; }
        public DateTime Contribution_Date { get; set; }
        public string Contribution_Comments { get; set; } = string.Empty;
        public List<SelectListItem>? ListMinistry { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Payment_Method==Ref_payment_method.None)
            { yield return new ValidationResult("Payment_Method can't be None", new[] {"Payment_Method"}); }
        }
    }
}
