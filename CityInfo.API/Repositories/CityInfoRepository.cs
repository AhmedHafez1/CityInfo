using CityInfo.API.Data;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Repositories
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c => c.Name).Include(c => c.PointsOfInterest).ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId)
        {
            return await _context.Cities.Include(c => c.PointsOfInterest).FirstOrDefaultAsync(c => c.Id == cityId);
        }

        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task<PointOfInterest?> GetOnePointOfInterestAsync(int cityId, int pointId)
        {
            return await _context.PointsOfInterest.FirstOrDefaultAsync(p => p.Id == pointId && p.CityId == cityId);
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointsOfInterest.Where(p => p.CityId == cityId).ToListAsync();
        }

        public async Task<PointOfInterest> AddPointOfInterestAsync(int cityId, PointOfInterest pointOfInterest)
        {
            pointOfInterest.CityId = cityId;

            await _context.PointsOfInterest.AddAsync(pointOfInterest);

            await SaveChangesAsync();

            return pointOfInterest;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
