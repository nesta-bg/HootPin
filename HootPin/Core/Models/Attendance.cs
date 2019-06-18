namespace HootPin.Core.Models
{
    public class Attendance
    {
        public Hoot Hoot { get; set; }
        public int HootId { get; set; }
        public ApplicationUser Attendee { get; set; }
        public string AttendeeId { get; set; }
    }
}