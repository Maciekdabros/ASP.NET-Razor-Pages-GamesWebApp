using GamesWebApp.Authorization;
using GamesWebApp.Data;
using GamesWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GamesWebApp.Pages.Games
{
    [AllowAnonymous]
    public class DetailsModel : DI_BasePageModel
    {
        public DetailsModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public Game Game { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Game = await Context.Game.FirstOrDefaultAsync(m => m.GameID == id);

            if (Game == null)
            {
                return NotFound();
            }

            Game = await Context.Game
                 .AsNoTracking()
                 .Include(c => c.Platform)
                 .FirstOrDefaultAsync(m => m.GameID == id);

            var isAuthorized = User.IsInRole(Constants.AdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized
                && currentUserId != Game.OwnerID
                && Game.Status != Models.Game.GameStatus.Approved)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, Models.Game.GameStatus status)
        {
            var game = await Context.Game.FirstOrDefaultAsync(
                                                      m => m.GameID == id);

            if (game == null)
            {
                return NotFound();
            }

            var contactOperation = (status == Models.Game.GameStatus.Approved)
                                                       ? ContactOperations.Approve
                                                       : ContactOperations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, game,
                                        contactOperation);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            game.Status = status;
            Context.Game.Update(game);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}