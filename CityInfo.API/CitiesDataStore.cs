using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        public CitiesDataStore()
        {
            Cities = new List<CityDto>
            {
                       new CityDto { Id = 1, Name = "Ghaza" ,
                           PointsOfInterests = new List<PointOfInterestDto> {
                               new PointOfInterestDto { Id = 1, Name = "Hamas" },
                               new PointOfInterestDto { Id = 2, Name = "Nosirat" },
                               new PointOfInterestDto { Id = 3, Name = "Tal Elhawa" }
                           }
                       },
                       new CityDto { Id = 2, Name = "Quds",
                           PointsOfInterests = new List<PointOfInterestDto> {
                               new PointOfInterestDto { Id = 4, Name = "Aqsa" },
                               new PointOfInterestDto { Id = 5, Name = "Sakhra" },
                               new PointOfInterestDto { Id = 6, Name = "Marwan" }
                           }
                       },
                       new CityDto { Id = 3, Name = "Haifa",
                           PointsOfInterests = new List<PointOfInterestDto> {
                               new PointOfInterestDto { Id = 7, Name = "Palestine" },
                               new PointOfInterestDto { Id = 8, Name = "Muslim" },
                               new PointOfInterestDto { Id = 9, Name = "Arabic" }
                           }
                       },
                       new CityDto { Id = 4, Name = "Khalil",
                           PointsOfInterests = new List<PointOfInterestDto> {
                               new PointOfInterestDto { Id = 10, Name = "Masjid" },
                               new PointOfInterestDto { Id = 11, Name = "Library" },
                           }
                       },
                       new CityDto { Id = 5, Name = "Yafa",
                           PointsOfInterests = new List<PointOfInterestDto> {
                               new PointOfInterestDto { Id = 12, Name = "Muslimon" },
                           }
                       }
            };
        }
    }
}
