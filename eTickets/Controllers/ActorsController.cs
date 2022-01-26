using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data.Services.Interfaces;

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
            var data = await _service.GetAll(); //get the data
            return View(data);
        }
    }
}
