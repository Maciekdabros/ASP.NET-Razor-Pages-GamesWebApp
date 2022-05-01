using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.IO;
namespace GamesWebApp.Models
{
    public class Game
    {
        
        public int GameID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }


        [Range(0, 9999.99)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }


        public TradeType Type { get; set; }


        public enum TradeType
        {
            Exchange,
            Sale
        }

        public GameStatus Status { get; set; }

        public enum GameStatus
        {
            Submitted,
            Approved,
            Rejected
        }
        public int PlatformID { get; set; }
        [ForeignKey("PlatformID")]
        public Platform Platform { get; set; }

        public string OwnerID { get; set; }

        [ForeignKey("OwnerID")]
        public IdentityUser ApplicationUser { get; set; }

       // public IFormFile FormFile { get; set; }

        public byte[] Content { get; set; }


    }
}
