using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GamesWebApp.Authorization;
using GamesWebApp.Data;
using GamesWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamesWebApp.Pages.Games;
using System.Web;

namespace GamesWebApp.Pages.Games
{
    public class ProfileModel : DI_BasePageModel
    {
        public ProfileModel(
           ApplicationDbContext context,
           IAuthorizationService authorizationService,
           UserManager<IdentityUser> userManager)
           : base(context, authorizationService, userManager)
        {
        }

        // public IList<Game> Game { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public async Task OnGetAsync(string id)
        {
            var currentUserId = UserManager.GetUserId(User);

            var users = Context.ApplicationUser
                 .Include(c => c.Games).Where(x=>x.UserName==id);

            ApplicationUser = await users.FirstOrDefaultAsync();

            LikeCount = Context.Like.Include(x => x.Taker).Where(x => x.Taker.UserName == id).Count();
        }

       public int LikeCount { get; set; }


    }
}
