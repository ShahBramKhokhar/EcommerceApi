using WebRexErpAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRexErpAPI.DataAccess.Models
{
    [Index(nameof(Attempts))]
    [Index(nameof(IPAddress))]
    [Index(nameof(ItemId))]
    public class AllowItemOffer
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public Item? Item { get; set; }
        public int Attempts { get; set; }
        public string? IPAddress { get; set; }
    }
}
