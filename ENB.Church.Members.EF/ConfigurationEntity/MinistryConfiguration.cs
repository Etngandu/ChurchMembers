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
    public class MinistryConfiguration : IEntityTypeConfiguration<Ministry>
    {
        public void Configure(EntityTypeBuilder<Ministry> builder)
        {

            builder.Property(x => x.MinistryCode).IsRequired().HasMaxLength(60);
            builder.Property(x => x.MinistryName).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Ministry_Otherdetails).IsRequired().HasMaxLength(250);
           
        }
    }
}
