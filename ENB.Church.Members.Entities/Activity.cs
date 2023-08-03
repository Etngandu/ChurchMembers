using ENB.Church.Members.Entities.Collections;
using ENB.Church.Members.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Church.Members.Entities
{
    public class Activity : DomainEntity<int>, IDateTracking
    {
        public Activity()
        {
            MinistriesActivities = new();
           
        }
        public Activity_Type Activity_Type  { get; set; }
        public ActivityStatus ActivityStatus  { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? Color { get; set; }
        public string Activity_Description { get; set; } = string.Empty;
        public string Activity_OtherDetails { get; set; } = string.Empty;
        public DateTime DateCreated { get ; set; }
        public DateTime DateModified { get ; set ; }

        public MinistriesActivities MinistriesActivities  { get; set; }
      

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Activity_Type==Activity_Type.None) 
            { 
                yield return new ValidationResult("Activity_Type can't be None", new[] {"Activity_Type"}); 
            }
        }
    }
}
