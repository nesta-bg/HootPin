using HootPin.Core.Models;

namespace HootPin.Core.ViewModels
{
    public class HootDetailsViewModel
    {
        public Hoot Hoot { get; set; }
        public bool IsAttending { get; set; }
        public bool IsFollowing { get; set; }
    }
}