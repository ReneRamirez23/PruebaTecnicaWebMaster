using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaWebMaster.Models;
using System.Security.Claims;
using PruebaTecnicaWebMaster.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnicaWebMaster.Controllers
{

    public class LoginController : Controller
    {
        private readonly BD_ControlVentasContext _dbContext;

        public LoginController(BD_ControlVentasContext context)
        {

            _dbContext = context;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM LoginVm)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.FirstName == LoginVm.user);

            if (user == null || LoginVm.password != user.Password)
            {
                ModelState.AddModelError(string.Empty, "Incorrect data");
                return RedirectToAction("ErrorLogin");
            }

            var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name, user.FirstName),
                     new Claim(ClaimTypes.Role, user.TypeUser)
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ErrorLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "index");
        }
    }

}

