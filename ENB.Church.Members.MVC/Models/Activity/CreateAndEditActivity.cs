using ENB.Church.Members.Entities.Collections;
using ENB.Church.Members.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ENB.Church.Members.MVC.Models
{
    public class CreateAndEditActivity : IValidatableObject
    {
        public int Id { get; set; }

        [DisplayName("Activity Type")]
        public Activity_Type Activity_Type { get; set; }

        [DisplayName("Activity Status")]
        public ActivityStatus ActivityStatus { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        // public string? Color { get; set; }

        [DisplayName("Activity Description")]
        public string Activity_Description { get; set; } = string.Empty;

        [DisplayName("Activity Other Details")]
        public string Activity_OtherDetails { get; set; } = string.Empty;     
        public MinistriesActivities? MinistriesActivities { get; set; }
        public MembersActivities? MembersActivities { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Activity_Type == Activity_Type.None) 
            { yield return new ValidationResult("Activity_Type can't be None", new[] { "Activity_Type" }); }
        }
    }
}
