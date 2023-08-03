using ENB.Church.Members.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ENB.Church.Members.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(150)")]
        public string? FirstName { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? LastName { get; set; }
    }
}
