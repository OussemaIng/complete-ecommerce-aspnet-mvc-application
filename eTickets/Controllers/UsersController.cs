using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using eTickets.Data.Base;
using StackExchange.Redis;
using eTickets.Data;
using System.Linq;
using eTickets.Utils;
using Microsoft.AspNetCore.Http.Extensions;

namespace eTickets.Controllers
{
    public class UsersController : Controller
    {
        private readonly IEntityBaseRepository<User> _UserService;
        public readonly AppDbContext _context;
        public UsersController(IEntityBaseRepository<User> userService, AppDbContext context)
        {
            _UserService = userService;
            _context = context;
        }

        //registration Action
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        //registration Post Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(User user)
        {
            bool Status = false;
            string message = string.Empty;

            ModelState.Remove("ActivationCode");
            ModelState.Remove("IsEmailVerified");
            if (ModelState.IsValid)
            {
                //email exist
                var isExist = IsEmailExist(user.EmailID);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }
                //generate activation code
                user.ActivationCode = System.Guid.NewGuid();
                //password ,ConfirmPassword hashing
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);

                user.IsEmailVerified = false;

                _UserService.AddAsync(user);
                //SendVerificationEmail(user.EmailID, user.ActivationCode.ToString());
                message = "Registration Successfully";
                Status = true;
            }
            else
            {
                message = "Invalid Request";
            }
            ViewBag.Status = Status;
            ViewBag.Message = message;
            return View(user);
        }
        //email validation
        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            var validation = _context.Users.Where(u => u.EmailID == emailID).FirstOrDefault();
            if (validation != null)
                return true;
            return false;
        }
        [NonAction]
        public void SendVerificationEmail(string EmailId, string ActivationCode)
        {

        }

    }
}
