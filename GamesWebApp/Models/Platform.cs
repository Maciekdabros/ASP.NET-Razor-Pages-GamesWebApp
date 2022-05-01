using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesWebApp.Models
{
    public class Platform
    {
        public int PlatformID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public List<Game> Games { get; set; }
    }
}
