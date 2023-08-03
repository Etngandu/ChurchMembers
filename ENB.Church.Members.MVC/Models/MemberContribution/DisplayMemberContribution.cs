using ENB.Church.Members.Entities;
using Microsoft.EntityFrameworkCore;

namespace ENB.Church.Members.MVC.Models
{
    public class DisplayMemberContribution
    {
        public int Id { get; set; }
        public Ministry? Ministry { get; set; }
        public int MinistryId { get; set; }
        public Member? Member { get; set; }
        public int MemberId { get; set; }
        public Ref_payment_method Payment_Method { get; set; }        
        public decimal Contribution_amount { get; set; }
        public DateTime Contribution_Date { get; set; }
        public string Contribution_Comments { get; set; } = string.Empty;
        public string MinistryName { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
