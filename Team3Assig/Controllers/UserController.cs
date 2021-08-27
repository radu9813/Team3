using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team3Assig.Controllers
{   
    [Authorize(Roles ="Administrator")]
    public class UsersController : Controller
    {
        private UserManager<IdentityUser> userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            this.ViewData["Administrators"] = await this.userManager.GetUsersInRoleAsync("Administrator");
            this.ViewData["Operators"] = await this.userManager.GetUsersInRoleAsync("Operator");
            return this.View(await this.userManager.Users.ToListAsync());
        }

        public async Task<IActionResult> AssignAdminRole(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            await this.userManager.AddToRoleAsync(user, "Administrator");
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> UnassignAdminRole(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            await this.userManager.RemoveFromRoleAsync(user, "Administrator");
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> AssignOperatorRole(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            await this.userManager.AddToRoleAsync(user, "Operator");
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> UnassignOperatorRole(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            await this.userManager.RemoveFromRoleAsync(user, "Operator");
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
