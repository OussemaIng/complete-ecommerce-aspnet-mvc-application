using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data.Services.Interfaces;
using eTickets.Models;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {

        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }

        //https://localhost:44330/Actors(ControllerName)/Index
        public async Task<IActionResult> Index()//ActionResult by default (Index)
        {
            var data = await _service.GetAllAsync(); //get the data
            return View(data);
        }

        //Get : Actors/Create
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")]Actor actor)
        {
            if(!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.AddAsync(actor);
            return  RedirectToAction(nameof(Index));
        }
        //Get  : Actor/Details/1

        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null)
                return View("Empthy");
            return View(actorDetails);
        }
    }
}
