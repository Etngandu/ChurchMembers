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
    public class MinistryActivity : DomainEntity<int>, IDateTracking
    {
        public MinistryActivity()
        {
            MembersActivities = new();
        }
        public Activity? Activity  { get; set; }
        public int ActivityId { get; set; }
        public Ministry? Ministry { get; set; }
        public int? MinistryId { get; set; }
        public DateTime MinistryActivity_Start_Date { get; set; }
        public DateTime MinistryActivity_End_Date { get; set; }
        public DateTime DateCreated { get ; set ; }
        public DateTime DateModified { get ; set ; }
        public MembersActivities MembersActivities  { get; set; }
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
