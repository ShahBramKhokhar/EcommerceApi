using System.ComponentModel.DataAnnotations;

namespace WebRexErpAPI.DataAccess.Models
{
    public class UserType
    {
        [Key]
        public int Id { get; set; }
        public string TypeName { get; set; }


    }
}
