using BookStoreMvc.DataAccess.Data;
using BookStoreMvc.DataAccess.Repository.IRepository;
using BookStoreMvc.Models;
using BookStoreMvc.Models.ViewModels;
using BookStoreMvc.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

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

        public IActionResult RoleManagement(string? userId)
        {
            var userRoles = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            ApplicationUser user = _db.ApplicationUsers.Include(u => u.Company).FirstOrDefault(u => u.Id == userId);
            //var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId; //understand how .RoleId works
            //user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;

            UserVM UserVM = new ()
            {
                ApplicationUser = user,
                UserRole = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId, //understand how .RoleId works
                RoleList = _db.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id
                }),
                CompanyList = _db.Companies.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                UserCompany = user.Company?.Id
            };

            return View(UserVM);
        }

        [HttpPost]
        public IActionResult RoleManagement(UserVM userVM)
        {
            ApplicationUser userFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == userVM.ApplicationUser.Id);
            userFromDb.CompanyId = userVM.UserCompany;
            _db.ApplicationUsers.Update(userFromDb);

            //The UserRoles table is a join table for many-to-many between Users and Roles.
            //EF Core tracks(UserId, RoleId) as a composite primary key.
            //You can’t UPDATE part of a composite key — you must delete and reinsert.
            var userRoleFromDb = _db.UserRoles.FirstOrDefault(u => u.UserId == userVM.ApplicationUser.Id);
            _db.UserRoles.Remove(userRoleFromDb);
            _db.SaveChanges();

            _db.UserRoles.Add(new IdentityUserRole<string>
            {
                UserId = userVM.ApplicationUser.Id,
                RoleId = userVM.UserRole
            });
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
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
