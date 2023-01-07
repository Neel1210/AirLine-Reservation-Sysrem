using airLineReservationSystem.Dao;
using airLineReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace airLineReservationSystem.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUpSucess()
        {
            return View();
        }

        public IActionResult SignUpFailed()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            int ans = LoginDao.AddUser(user);
            if(ans>0)
                return RedirectToAction("SignUpSucess", "Login");
            return RedirectToAction("SignUpFailed", "Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(User user)
        {
            Console.WriteLine(user.UserId + "-" + user.Password);
            User user1 = LoginDao.validteUser(user.UserId,user.Password);
            if (user1 == null)
            {
                return RedirectToAction("AccessDenied","Login");
            }
            else
            {
                if(user1.Type=="Admin")
                    return RedirectToAction("Index","Admin", user1);
                else
                    return RedirectToAction("Index","User",user1);
            }
        }

    }


}
