using ENB.Church.Members.Entities.Collections;
using ENB.Church.Members.Entities;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ENB.Church.Members.MVC.Models
{
    public class CreateAndEditMinistryActivity: IValidatableObject
    {
        public int Id { get; set; }
        public Activity? Activity { get; set; }
        public int ActivityId { get; set; }
        public Ministry? Ministry { get; set; }
        public int? MinistryId { get; set; }
        public DateTime MinistryActivity_Start_Date { get; set; }
        public DateTime MinistryActivity_End_Date { get; set; }        
        public MembersActivities? MembersActivities { get; set; }

        public List<SelectListItem>? ListActivities { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(ActivityId== 0) 
            { yield return new ValidationResult("Activity Id can't be 0", new[] { "ActivityId" }); }
        }
    }
}
