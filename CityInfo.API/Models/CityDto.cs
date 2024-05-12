using System.Collections;

namespace CityInfo.API.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Name of the city.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int NumberOfPointsOfInterest
        {
            get
            {
                return PointsOfInterest.Count;
            }
        }

        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; } = new List<PointOfInterestDto>();
    }
}
