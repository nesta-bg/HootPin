using HootPin.Models;
using System.Collections.Generic;

namespace HootPin.ViewModels
{
    public class HootsViewModel
    {
        public IEnumerable<Hoot> UpcomingHoots { get; set; }
        public bool ShowActions { get; set; }
        public string SearchTerm { get; set; }
        public string Heading { get; set; }
    }
}