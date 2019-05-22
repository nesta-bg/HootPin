using HootPin.Models;
using System.Collections.Generic;

namespace HootPin.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Hoot> UpcomingHoots { get; set; }
        public bool ShowActions { get; set; }
    }
}