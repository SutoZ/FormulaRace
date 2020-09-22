using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Race.Model.Models;
using Race.Repo.ApplicationContext;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Race.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration conf;
        private readonly IRaceContext context;

        public AccountController(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager, IConfiguration conf,
            IRaceContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.conf = conf;
            this.context = context;
        }

        [HttpGet]
        public async Task<JsonResult> CreateDefaultUsers()
        {
            string role_registeredUser = "RegisteredUser";
            string role_administrator = "Administrator";

            if (await roleManager.FindByNameAsync(role_registeredUser) == null)
                await roleManager.CreateAsync(new IdentityRole(role_registeredUser));

            if (await roleManager.FindByNameAsync(role_administrator) == null)
                await roleManager.CreateAsync(new IdentityRole(role_administrator));
            
            //https://www.learnentityframeworkcore.com/concurrency/

            var userList_concurrent = new ConcurrentBag<ApplicationUser>();

            var user_test = await CheckIfTestUserExists(role_registeredUser);
            var user_admin = await CheckIfAdminExists(role_registeredUser, role_administrator);

            userList_concurrent.Add(user_test);
            userList_concurrent.Add(user_admin);

            if (userList_concurrent.Count > 0)
                await context.SaveChangesAsync(true, default);

            return new JsonResult(new
            {
                Count = userList_concurrent.Count,
                Users = userList_concurrent
            });

        }

        private async Task<ApplicationUser> CheckIfTestUserExists(string role_registeredUser)
        {
            var user_test = new ApplicationUser
            {
                Id = "1",
                Email = conf.GetSection("TestUser").GetSection("Email").Value.ToString(),
                UserName = conf.GetSection("TestUser").GetSection("UserName").Value.ToString(),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            ApplicationUser asdf = await userManager.FindByNameAsync(conf.GetSection("TestUser").GetSection("UserName").Value.ToString());

            if (await userManager.FindByNameAsync(conf.GetSection("TestUser").GetSection("UserName").Value.ToString()) == null)
                await userManager.CreateAsync(user_test, "Asdf12345678.");

            await userManager.AddToRoleAsync(user_test, role_registeredUser);
            user_test.EmailConfirmed = true;
            user_test.LockoutEnabled = false;

            return user_test;
        }

        private async Task<ApplicationUser> CheckIfAdminExists(string role_registeredUser, string role_administrator)
        {
            var user_admin = new ApplicationUser
            {
                Id = "2",
                Email = conf.GetSection("Admin").GetSection("Email").Value.ToString(),
                UserName = conf.GetSection("Admin").GetSection("UserName").Value.ToString(),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (await userManager.FindByNameAsync(conf.GetSection("Admin").GetSection("UserName").Value.ToString()) == null)
                await userManager.CreateAsync(user_admin, "MySecr3t.");

            await userManager.AddToRoleAsync(user_admin, role_registeredUser);
            await userManager.AddToRoleAsync(user_admin, role_administrator);

            user_admin.EmailConfirmed = true;
            user_admin.LockoutEnabled = false;

            return user_admin;
        }
    }
}
