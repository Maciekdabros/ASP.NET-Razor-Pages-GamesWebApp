using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        /*
        public ApplicationUser()
        {
            Messages = new HashSet<Message>();
        }
        */

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public List<Game> Games { get; set; }

        public List<Message> Messages { get; set; }

        public virtual ICollection<Like> LikesGiven { get; set; }

        public virtual ICollection<Like> LikesTaken { get; set; }

    }
}
