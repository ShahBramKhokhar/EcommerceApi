
using WebRexErpAPI.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebRexErpAPI.Models
{
    [Index(nameof(Name))]
    [Index(nameof(ItemCount))]
    public class Category : BaseQBEntity
    {
       
        public string? Name { get; set; }
        public int ItemCount { get; set; }

    }
}
