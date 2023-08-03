using ENB.Church.Members.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ENB.Church.Members.MVC.Models
{
    public class CreateAndEditMemberActivity: IValidatableObject
    {
        public int Id { get; set; }        
        public Member? Member { get; set; }
        public int? MemberId { get; set; }
        public Ministry? Ministry { get; set; }
        public int? MinistryId { get; set; }
        public MinistryActivity? MinistryActivity { get; set; }
        public int? MinistryActivityId { get; set; }
        public DateTime MemberActivity_Start_Date { get; set; }
        public DateTime MemberActivity_End_Date { get; set; }
        public List<SelectListItem>? ListMembers { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(MemberId==0) 
            {
                yield return new ValidationResult("MemberId can't be 0", new[] { "MemberId" });
            }
        }
    }
}
