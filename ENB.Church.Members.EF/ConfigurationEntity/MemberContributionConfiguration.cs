using ENB.Church.Members.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Church.Members.EF.ConfigurationEntity
{
    public class MemberContributionConfiguration : IEntityTypeConfiguration<MemberContribution>
    {
        public void Configure(EntityTypeBuilder<MemberContribution> builder)
        {
           
            builder.Property(x => x.Contribution_Comments).IsRequired().HasMaxLength(250);
           
        }
    }
}
