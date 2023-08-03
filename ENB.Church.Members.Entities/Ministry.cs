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
    public class Ministry : DomainEntity<int>, IDateTracking
    {
        public Ministry()
        {
            MinistriesStaffs = new();
            MinistryActivities = new();
            MemberContributions = new();
            MemberActivities= new();    
        }
        public string MinistryCode { get; set; } = string.Empty;
        public string MinistryName { get; set; } = string.Empty;
        public string Ministry_Otherdetails { get; set; }=string.Empty;
        public DateTime DateCreated { get ; set ; }
        public DateTime DateModified { get ; set; }
        public MinistriesStaffs  MinistriesStaffs { get; set; }
        public MinistriesActivities  MinistryActivities { get; set; }
        public MembersActivities MemberActivities  { get; set; }
        public MembersContributions MemberContributions  { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
