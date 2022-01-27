using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Models;
using eTickets.Data.Base;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {

        private readonly IEntityBaseRepository<Actor> _ActorService;

        public ActorsController(IEntityBaseRepository<Actor> service)
        {
            _ActorService = service;
        }

        //https://localhost:44330/Actors(ControllerName)/Index
        public async Task<IActionResult> Index()//ActionResult by default (Index)
        {
            var data = await _ActorService.GetAllAsync(); //get the data
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
            await _ActorService.AddAsync(actor);
            return  RedirectToAction(nameof(Index));
        }
        //Get  : Actor/Details/1

        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _ActorService.GetByIdAsync(id);
            if (actorDetails == null)
                return View("NotFound");
            return View(actorDetails);
        }

        //Get  : Actor/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _ActorService.GetByIdAsync(id);
            if (actorDetails == null)
                return View("NotFound");
            return View(actorDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id ,[Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _ActorService.UpdateAsync(id,actor);
            return RedirectToAction(nameof(Index));
        }

        //Get  : Actor/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _ActorService.GetByIdAsync(id); //check if the actor is exist
            if (actorDetails == null)
                return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _ActorService.GetByIdAsync(id); //check if the actor is exist
            if (actorDetails == null)
                return View("NotFound");
            await _ActorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
