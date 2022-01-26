using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {

        private readonly AppDbContext _context;

        public ActorsController(AppDbContext context)
        {
            _context = context;
        }

        //https://localhost:44330/Actors(ControllerName)/Index
        public async Task<IActionResult> Index()//ActionResult by default (Index)
        {
            var allActors = await  _context.Actors.ToListAsync(); //get the data
            return View(allActors);
        }
    }
}
