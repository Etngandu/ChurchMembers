using ENB.Church.Members.Entities;

namespace ENB.Church.Members.MVC.Models
{
    public class DisplayMemberActivity
    {
        public int Id { get; set; }
        public Member? Member { get; set; }
        public int? MemberId { get; set; }
        public Ministry? Ministry { get; set; }
        public int? MinistryId { get; set; }
        public MinistryActivity? MinistryActivity { get; set; }
        public int? MinistryActivityId { get; set; }
        public DateTime MemberActivity_Start_Date { get; set; }
        public DateTime MemberActivity_End_Date { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string ActivityName { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
    }
}
