using ENB.Church.Members.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Church.Members.Entities
{
    public class Staff_Skill : DomainEntity<int>, IDateTracking
    {
        public Staff? Staff  { get; set; }
        public int StaffId { get; set; }
        public Ref_Skill Ref_Skill  { get; set; }
        public Skill_Level Skill_Level  { get; set; }
        public DateTime DateCreated { get ; set ; }
        public DateTime DateModified { get ; set ; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Ref_Skill == Ref_Skill.None)
            {
                yield return new ValidationResult("Ref_Skill can't be None", new[] { "Ref_Skill" });
            }
            if (Skill_Level == Skill_Level.None)
            {
                yield return new ValidationResult("Skill_Level can't be None", new[] { "Skill_Level" });
            }
        }
    }
}
