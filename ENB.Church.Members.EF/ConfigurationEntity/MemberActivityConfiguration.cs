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
    public class MemberActivityConfiguration : IEntityTypeConfiguration<MemberActivity>
    {
        public void Configure(EntityTypeBuilder<MemberActivity> builder)
        {
           
            builder.HasOne<Member>(cs => cs.Member)
                .WithMany(cf => cf.MemberActivities)
                .HasForeignKey(y => y.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<MinistryActivity>(cs => cs.MinistryActivity)
                .WithMany(cf => cf.MembersActivities)
                .HasForeignKey(y => y.MinistryActivityId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<Ministry>(cs => cs.Ministry)
                .WithMany(cf => cf.MemberActivities)
                .HasForeignKey(y => y.MinistryId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
