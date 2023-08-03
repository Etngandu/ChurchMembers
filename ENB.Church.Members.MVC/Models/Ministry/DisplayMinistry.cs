namespace ENB.Church.Members.MVC.Models
{
    public class DisplayMinistry
    {
        public int Id { get; set; }
        public string MinistryCode { get; set; } = string.Empty;
        public string MinistryName { get; set; } = string.Empty;
        public string Ministry_Otherdetails { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
