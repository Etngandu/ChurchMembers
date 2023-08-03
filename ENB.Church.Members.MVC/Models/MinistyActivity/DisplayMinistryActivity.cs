using ENB.Church.Members.Entities.Collections;
using ENB.Church.Members.Entities;

namespace ENB.Church.Members.MVC.Models
{
    public class DisplayMinistryActivity
    {
        public int Id { get; set; }
        public Activity? Activity { get; set; }
        public int ActivityId { get; set; }
        public Ministry? Ministry { get; set; }
        public int? MinistryId { get; set; }
        public DateTime MinistryActivity_Start_Date { get; set; }
        public DateTime MinistryActivity_End_Date { get; set; }
        public DateTime DateCreated { get; set; }
        public string MinistryName { get; set; } = string.Empty;
        public string ActivityName { get; set; } = string.Empty;
        public DateTime DateModified { get; set; }
        public MembersActivities? MembersActivities { get; set; }
    }
}
