using HootPin.Models;
using System.Collections.Generic;

namespace HootPin.ViewModels
{
    public class HootFormViewModel
    {
        public string Venue { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}