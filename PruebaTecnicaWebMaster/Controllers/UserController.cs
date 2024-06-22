using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaWebMaster.Models;
using PruebaTecnicaWebMaster.Repositories;

namespace PruebaTecnicaWebMaster.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [Authorize(policy: "Admin")]
        public async Task<IActionResult> Index()
        {
            var userlist = _userRepository.GetAll();
            return View(userlist);
        }

        // Add User
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.Add(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // Update User
        public IActionResult Edit(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, User user)
        {
            if(id != user.IdUser)
            {
                return BadRequest();
            }

            if (ModelState.IsValid) {
                try
                {
                    _userRepository.Update(user);
                }
                catch (DbUpdateConcurrencyException) 
                {
                    if (_userRepository.GetById(user.IdUser) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        //Delete User
        public IActionResult Delete(int id)
        {
            var user = _userRepository.GetById(id);
            if(user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _userRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
