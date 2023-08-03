using ENB.Church.Members.Entities.Collections;
using ENB.Church.Members.Entities;
using System.ComponentModel.DataAnnotations;

namespace ENB.Church.Members.MVC.Models
{
    public class DisplayMember
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;        
        public string LastName { get; set; } = String.Empty;        
        public string EmailAddress { get; set; } = String.Empty;        
        public Gender Gender { get; set; }       
        public string PhoneNumber { get; set; } = String.Empty;
        
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth     { get;   set; }

        public MembersActivities? MemberActivities { get; set; }
        public MembersContributions? MemberContributions { get; set; }        
        public string Other_details { get; set; } = String.Empty;        
        public string? FullName    { get; set; }
        public Address? AddressCustomer { get; set; }        
        public DateTime DateCreated { get; set; }       
        public DateTime DateModified { get; set; }
    }
}
