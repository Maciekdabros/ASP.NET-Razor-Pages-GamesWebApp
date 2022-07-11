using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GamesWebApp.Models
{
    public class Message
    {
        public int MessageID { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]

        public ApplicationUser ApplicationUser { get; set; }

        /*
        public Message()
        {
            Date = DateTime.Now;
        }
        */
    }
}
