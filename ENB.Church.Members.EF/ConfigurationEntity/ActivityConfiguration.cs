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
    public class ActivityConfiguration: IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {

            builder.Property(x => x.Color).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Activity_Description).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Activity_OtherDetails).IsRequired().HasMaxLength(250);
           
        }
    }
}
