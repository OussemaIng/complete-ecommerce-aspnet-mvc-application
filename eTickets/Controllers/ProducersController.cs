using eTickets.Data;
using eTickets.Data.Base;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IEntityBaseRepository<Producer> _ProducerService;

        public ProducersController(IEntityBaseRepository<Producer> service)
        {
            _ProducerService = service;
        }

        public async Task<IActionResult> Index()//ActionResult by default (Index)
        {
            var data = await _ProducerService.GetAllAsync(); //get the data
            return View(data);
        }

        //Get : Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _ProducerService.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }
        //Get  : Actor/Details/1

        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _ProducerService.GetByIdAsync(id);
            if (producerDetails == null)
                return View("NotFound");
            return View(producerDetails);
        }

        //Get  : Actor/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var producerDetails = await _ProducerService.GetByIdAsync(id);
            if (producerDetails == null)
                return View("NotFound");
            return View(producerDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _ProducerService.UpdateAsync(id, producer);
            return RedirectToAction(nameof(Index));
        }

        //Get  : Actor/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var producerDetails = await _ProducerService.GetByIdAsync(id); //check if the actor is exist
            if (producerDetails == null)
                return View("NotFound");
            return View(producerDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerDetails = await _ProducerService.GetByIdAsync(id); //check if the actor is exist
            if (producerDetails == null)
                return View("NotFound");
            await _ProducerService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
