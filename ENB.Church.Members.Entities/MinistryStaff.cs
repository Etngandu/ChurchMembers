using ENB.Church.Members.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Church.Members.Entities
{
    public class MinistryStaff : DomainEntity<int>, IDateTracking
    {
        public Staff?  Staff  { get; set; }
        public int StaffId { get; set; }
        public Ministry? Ministry { get; set; }
        public int MinistryId { get; set; }
        public DateTime Date_Joined_Ministry { get; set; }
        public DateTime? Date_Left_Ministry { get; set; }
        public DateTime DateCreated { get ; set ; }
        public DateTime DateModified { get ; set ; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
