using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetCities()
        {
            return new JsonResult(
                    new[]
                    {
                       new { id = 1, name = "Ghaza"  },
                       new { id = 2, name = "Quds"  },
                       new { id = 3, name = "Haifa"  },
                       new { id = 4, name = "Khalil"  },
                       new { id = 5, name = "Yafa"  }
                    }
                );
        }
    }
}
