using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class UpdatePointOfInterestDto
    {
        [Required(ErrorMessage = "You must provide the name.")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(220)]
        public string? Description { get; set; }
    }
}
