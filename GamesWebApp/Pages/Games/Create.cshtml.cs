using GamesWebApp.Authorization;
using GamesWebApp.Data;
using GamesWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using GamesWebApp.Pages;
using System.ComponentModel.DataAnnotations;

namespace GamesWebApp.Pages.Games
{
    public class CreateModel : DI_BasePageModel
    {
        public CreateModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {

        }


        public IActionResult OnGet()
        {
            PopulatePlatformsDropDownList(Context);

            return Page();
        }

        [BindProperty]
        public Game Game { get; set; }

        
        [BindProperty]
        public BufferedSingleFileUploadDb FileUpload { get; set; }


        public async Task<IActionResult> OnPostAsync()
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

            Game.OwnerID = UserManager.Users.Where(c => c.UserName == User.Identity.Name).Select(c => c.Id).First();


            //Game.ApplicationUser = UserManager.Users.Where(c => c.Id == Game.OwnerID).First();
            //Game.OwnerID = UserManager.GetUserId(User);

            // requires using ContactManager.Authorization;
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                        User, Game,
                                                        ContactOperations.Create);
            
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }


            Context.Game.Add(Game);
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
