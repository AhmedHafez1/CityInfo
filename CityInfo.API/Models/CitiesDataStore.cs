namespace CityInfo.API.Models
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public CitiesDataStore()
        {
            Cities = new List<CityDto>
            {
                       new CityDto { Id = 1, Name = "Ghaza"  },
                       new CityDto { Id = 2, Name = "Quds"  },
                       new CityDto { Id = 3, Name = "Haifa"  },
                       new CityDto { Id = 4, Name = "Khalil"  },
                       new CityDto { Id = 5, Name = "Yafa"  }
            };
        }
    }
}
