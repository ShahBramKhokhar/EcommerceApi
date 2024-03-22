using System.ComponentModel.DataAnnotations;

namespace WebRexErpAPI.Business.Industry.Dto
{
    public class IndustryDto
    {

        public int? Id { get; set; } = 0;

        public string Name { get; set; }

        public int ItemCount { get; set; }

    }
}
