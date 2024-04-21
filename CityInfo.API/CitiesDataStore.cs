using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public CitiesDataStore()
        {
            Cities = new List<CityDto>
            {
                       new CityDto { Id = 1, Name = "Ghaza" ,
                           PointsOfInterests = new List<PointsOfInterest> {
                               new PointsOfInterest { Id = 1, Name = "Hamas" },
                               new PointsOfInterest { Id = 2, Name = "Nosirat" },
                               new PointsOfInterest { Id = 3, Name = "Tal Elhawa" }
                           }
                       },
                       new CityDto { Id = 2, Name = "Quds",
                           PointsOfInterests = new List<PointsOfInterest> {
                               new PointsOfInterest { Id = 4, Name = "Aqsa" },
                               new PointsOfInterest { Id = 5, Name = "Sakhra" },
                               new PointsOfInterest { Id = 6, Name = "Marwan" }
                           }
                       },
                       new CityDto { Id = 3, Name = "Haifa",
                           PointsOfInterests = new List<PointsOfInterest> {
                               new PointsOfInterest { Id = 7, Name = "Palestine" },
                               new PointsOfInterest { Id = 8, Name = "Muslim" },
                               new PointsOfInterest { Id = 9, Name = "Arabic" }
                           }
                       },
                       new CityDto { Id = 4, Name = "Khalil",
                           PointsOfInterests = new List<PointsOfInterest> {
                               new PointsOfInterest { Id = 10, Name = "Masjid" },
                               new PointsOfInterest { Id = 11, Name = "Library" },
                           }
                       },
                       new CityDto { Id = 5, Name = "Yafa",
                           PointsOfInterests = new List<PointsOfInterest> {
                               new PointsOfInterest { Id = 12, Name = "Muslimon" },
                           }
                       }
            };
        }
    }
}
