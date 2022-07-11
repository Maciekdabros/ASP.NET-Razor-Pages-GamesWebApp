using System;
using System.Collections.Generic;
using GamesWebApp.Authorization;
using GamesWebApp.Data;
using GamesWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GamesWebApp.Pages.Games
{
    public class LikeModel : DI_BasePageModel
    {
        public LikeModel(
          ApplicationDbContext context,
          IAuthorizationService authorizationService,
          UserManager<IdentityUser> userManager)
          : base(context, authorizationService, userManager)
        {
        }
        public JsonResult OnGet(string id)
        {
            var currentUserId = UserManager.GetUserId(User);
            //ApplicationUser update = Context.ApplicationUser.ToList().Find(l => l.Number_of_likes == currentUserId);
            //update.Number_of_likes += 1;

            var like = Context.Like
                 .Include(x => x.Giver)
                 .Include(x => x.Taker)
                 .Where(x => x.Giver.Id == currentUserId)
                 .Where(x => x.Taker.Id == id).FirstOrDefault();

            if (like != null)
            {
                Context.Like.Remove(like);
            }
            else {
                var Giver = Context.ApplicationUser.Where(x => x.Id == currentUserId).FirstOrDefault();
                var Taker = Context.ApplicationUser.Where(x => x.Id == id).FirstOrDefault();

                like = new Like()
                {
                    Giver = Giver,
                    Taker = Taker
                };

                Context.Like.Add(like);
            }
    
            Context.SaveChanges();
            var likes = Context.Like.Include(x=>x.Taker).Where(x=>x.Taker.Id==id).Count();

            return new JsonResult(likes);
        }
    }
}
