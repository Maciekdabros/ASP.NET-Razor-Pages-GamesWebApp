using GamesWebApp.Authorization;
using GamesWebApp.Data;
using GamesWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace GamesWebApp.Pages.Games
{
    
    public class EditModel : DI_BasePageModel
    {
        public EditModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Game Game { get; set; }

        [BindProperty]
        public BufferedSingleFileUploadDb FileUpload { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Game = await Context.Game.Include(c => c.Platform).FirstOrDefaultAsync(m => m.GameID == id);

            if (Game == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                      User, Game,
                                                      ContactOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            PopulatePlatformsDropDownList(Context, Game.PlatformID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (FileUpload?.FormFile != null)
            {
                    using (var memoryStream = new MemoryStream())
                    {
                        await FileUpload.FormFile.CopyToAsync(memoryStream);

                        //Upload the file if less than 2 MB
                        // if (memoryStream.Length < 2097152)
                        // {
                        Game.Content = memoryStream.ToArray();
                        // }
                        // else
                        // {
                        //   ModelState.AddModelError("File", "The file is too large.");
                        //  return Page();
                        // }
                    }
            }

            // Fetch Contact from DB to get OwnerID.
            var game = await Context
                .Game.AsNoTracking()
                .FirstOrDefaultAsync(m => m.GameID == id);

            if (game == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, game,
                                                     ContactOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Game.OwnerID = game.OwnerID;

            Context.Attach(Game).State = EntityState.Modified;

            if (Game.Status == Models.Game.GameStatus.Approved)
            {
                // If the contact is updated after approval, 
                // and the user cannot approve,
                // set the status back to submitted so the update can be
                // checked and approved.
                var canApprove = await AuthorizationService.AuthorizeAsync(User,
                                        Game,
                                        ContactOperations.Approve);

                if (!canApprove.Succeeded)
                {
                    Game.Status = Models.Game.GameStatus.Submitted;
                }
            }

            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        public class BufferedSingleFileUploadDb
        {
            [Display(Name = "File")]
            public IFormFile FormFile { get; set; }

        }
    }
}