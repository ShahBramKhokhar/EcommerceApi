using System.ComponentModel.DataAnnotations;

namespace WebRexErpAPI.Business.Category.Dto
{
    public class CategoryDto
    {

        public int? Id { get; set; } = 0;

        public string Name { get; set; }

        public int ItemCount { get; set; }

    }
}
