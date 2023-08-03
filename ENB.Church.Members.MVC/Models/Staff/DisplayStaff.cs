using ENB.Church.Members.Entities.Collections;
using ENB.Church.Members.Entities;
using System.ComponentModel.DataAnnotations;

namespace ENB.Church.Members.MVC.Models
{
    public class DisplayStaff
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty; 
        public string LastName { get; set; } = string.Empty;
        
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth     {  get;  set; }

        public string EmailAddress { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;        
        public string Other_details { get; set; } = string.Empty;
        public string? FullName    {   get;     set;      }
        public Address? AddressStaff { get; set; }
        public StaffSkills? StaffSkills { get; set; }
        public MinistriesStaffs? MinistriesStaffs { get; set; }        
        public DateTime DateCreated { get; set; }        
        public DateTime DateModified { get; set; }
    }
}
