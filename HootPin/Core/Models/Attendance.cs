using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HootPin.Core.Models
{
    public class Attendance
    {
        public Hoot Hoot { get; set; }

        [Key]
        [Column(Order = 1)]
        public int HootId { get; set; }

        public ApplicationUser Attendee { get; set; }

        [Key]
        [Column(Order = 2)]
        public string AttendeeId { get; set; }
    }
}