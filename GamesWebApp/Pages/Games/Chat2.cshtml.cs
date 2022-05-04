using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesWebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamesWebApp.Pages.Games
{
    [AllowAnonymous]
    public class Chat2Model : DI_BasePageModel
    {

        public Chat2Model(
                ApplicationDbContext context,
                IAuthorizationService authorizationService,
                UserManager<IdentityUser> userManager)
                : base(context, authorizationService, userManager)
        {

        }

        /*
        [BindProperty]
        public List<SelectListItem> Users { get; set; }

        [BindProperty]
        public string MyUser { get; set; }

        */
        public void OnGet()
        {
            /*
            Users = UserManager.Users.ToList()
          .Select(a => new SelectListItem { Text = a.UserName, Value = a.UserName })
          .OrderBy(s => s.Text).ToList();

            //get logged in user name
            MyUser = User.Identity.Name;
            */
    }


}
}
