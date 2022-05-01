using GamesWebApp.Data;
using GamesWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GamesWebApp.Pages.Games
{
    public class DI_BasePageModel : PageModel
    {
        protected ApplicationDbContext Context { get; }
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<IdentityUser> UserManager { get; }


        public DI_BasePageModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager) : base()
        {
            Context = context;
            UserManager = userManager;
            AuthorizationService = authorizationService;

        }

        public DI_BasePageModel(ApplicationDbContext context)
        {
            Context = context;
        }

        public SelectList PlatformNameSL { get; set; }

        public void PopulatePlatformsDropDownList(ApplicationDbContext _context,
            object selectedPlatform = null)
        {
            var platformsQuery = from d in _context.Platform
                                 orderby d.Name
                                 select d;

            PlatformNameSL = new SelectList(platformsQuery.AsNoTracking(),
                        "PlatformID", "Name", selectedPlatform);
        }



    }


   
}