using BookStoreMvc.DataAccess.Data;
using BookStoreMvc.DataAccess.Repository.IRepository;
using BookStoreMvc.Models;
using BookStoreMvc.Models.ViewModels;
using BookStoreMvc.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetALl()
        {
            List<ApplicationUser> objUsersList = _db.ApplicationUsers.Include(u=>u.Company).ToList();
            
            var userRoles = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (var user in objUsersList)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;

                if (user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Name = ""
                    };
                }
            }

            return Json(new { data = objUsersList });
        }

        [HttpPost]
        //what is FromBody?
        public IActionResult LockUnlock([FromBody]string id)
        {
            var objFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while locking/unlocking" });
            }
            else {
                if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
                {
                    //user is currently locked, we will unlock them
                    objFromDb.LockoutEnd = DateTime.Now;
                }
                else
                {
                    objFromDb.LockoutEnd = DateTime.Now.AddYears(100);
                }
                _db.SaveChanges();
                return Json(new { success = true, message = "Operation successful" });
            }
        }

        #endregion
    }
}
