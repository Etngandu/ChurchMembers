using ENB.Church.Members.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Church.Members.Entities
{
    public class MemberContribution : DomainEntity<int>, IDateTracking
    {
        public Ministry?  Ministry  { get; set; }
        public int MinistryId { get; set; }
        public Member?  Member { get; set; }
        public int MemberId { get; set; }
        public Ref_payment_method Payment_Method { get; set; }

        [Precision(18, 2)]
        public decimal Contribution_amount { get; set; }
        public DateTime Contribution_Date { get; set; }
        public string Contribution_Comments { get; set; } = string.Empty;
        public DateTime DateCreated { get ; set ; }
        public DateTime DateModified { get ; set ; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
