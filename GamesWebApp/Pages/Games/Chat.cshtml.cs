using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesWebApp.Data;
using GamesWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GamesWebApp.Pages.Games
{
    public class ChatModel: DI_BasePageModel
    {     
            public ChatModel(
                 ApplicationDbContext context,
                 IAuthorizationService authorizationService,
                 UserManager<IdentityUser> userManager)
                 : base(context, authorizationService, userManager)
            {

            }
            public async Task<IActionResult> OnGetAsync(string message)
            {
            Context.Message.Add(
            new Message
            {
                ApplicationUser = Context.ApplicationUser.Where(x=>x.Id==UserManager.GetUserId(User)).FirstOrDefault(),
                Text=message,
                UserID = UserManager.GetUserId(User),
                Date=DateTime.Now,
            });

            await Context.SaveChangesAsync();
            return Page();

            }
     
    }
}
