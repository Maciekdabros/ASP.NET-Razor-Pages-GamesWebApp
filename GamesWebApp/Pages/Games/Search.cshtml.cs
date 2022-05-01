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
    [AllowAnonymous]
    public class SearchModel : DI_BasePageModel
    {
        public SearchModel(
           ApplicationDbContext context,
           IAuthorizationService authorizationService,
           UserManager<IdentityUser> userManager)
           : base(context, authorizationService, userManager)
        {
        }
        public JsonResult OnGet(decimal min, decimal max)
        {
            var games = Context.Game.Where(p => p.Price >= min && p.Price <= max).Select(x=>x.GameID);
            return new JsonResult(games);
        }
    }
}
