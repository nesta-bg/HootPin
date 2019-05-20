using HootPin.Models;
using HootPin.ViewModels;
using System.Web.Mvc;
using System.Linq;

namespace HootPin.Controllers
{
    public class HootsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HootsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Hoots
        public ActionResult Create()
        {
            var viewModel = new HootFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View(viewModel);
        }
    }
}