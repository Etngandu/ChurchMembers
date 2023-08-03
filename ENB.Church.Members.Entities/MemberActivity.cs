using ENB.Church.Members.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Church.Members.Entities
{
    public class MemberActivity : DomainEntity<int>, IDateTracking
    {
        public Member?  Member { get; set; }
        public int? MemberId { get; set; }
        public Ministry? Ministry  { get; set; }
        public int? MinistryId { get; set; }
        public MinistryActivity? MinistryActivity  { get; set; }
        public int? MinistryActivityId { get; set; }
        public DateTime MemberActivity_Start_Date { get; set; }
        public DateTime MemberActivity_End_Date { get; set; }
        public DateTime DateCreated { get ; set ; }
        public DateTime DateModified { get ; set ; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
