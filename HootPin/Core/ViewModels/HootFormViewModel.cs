using HootPin.Controllers;
using HootPin.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace HootPin.Core.ViewModels
{
    public class HootFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public string Heading { get; set; }

        public DateTime GetDateTime()
        { 
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }

        public string Action
        {
            get
            {
                Expression<Func<HootsController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<HootsController, ActionResult>> create = (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }
    }
}