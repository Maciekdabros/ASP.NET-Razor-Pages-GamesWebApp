using GamesWebApp.Authorization;
using GamesWebApp.Data;
using GamesWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GamesWebApp.Pages.Games
{
    
    public class DeleteModel : DI_BasePageModel
    {
        public DeleteModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Game Game { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Game = await Context.Game.FirstOrDefaultAsync(
                                                 m => m.GameID == id);

            if (Game == null)
            {
                return NotFound();
            }

            Game = await Context.Game
                 .AsNoTracking()
                 .Include(c => c.Platform)
                 .FirstOrDefaultAsync(m => m.GameID == id);

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, Game,
                                                     ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            PopulatePlatformsDropDownList(Context, Game.PlatformID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var game = await Context
                .Game.AsNoTracking()
                .FirstOrDefaultAsync(m => m.GameID == id);

            if (game == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, game,
                                                     ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Context.Game.Remove(game);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
