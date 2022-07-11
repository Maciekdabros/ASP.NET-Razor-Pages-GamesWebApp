using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GamesWebApp.Models
{
    public class Like
    {
        public int LikeID { get; set; }

        public ApplicationUser Giver { get; set; }

        public ApplicationUser Taker { get; set; }

    }
}
