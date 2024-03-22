using WebRexErpAPI.Models;
using WebRexErpAPI.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRexErpAPI.DataAccess.Models
{
    [Index(nameof(UserId))]
    [Index(nameof(ItemId))]
    public class SaveLater 
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public Item? Item { get; set; }
    }
}
