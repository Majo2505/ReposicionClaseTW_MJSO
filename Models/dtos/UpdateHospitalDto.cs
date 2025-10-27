using System.ComponentModel.DataAnnotations;

namespace EC4clase1.Models.dtos
{
    public class UpdateHospitalDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public int Type { get; set; }
    }
}
