using CityInfo.API.Data;
using CityInfo.API.Entities;
using CityInfo.API.Models;
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

        public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? search, int pageNumber, int pageSize)
        {
            var query = _context.Cities as IQueryable<City>;
            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                query = query.Where(c => c.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.Name.Contains(search) || (c.Description != null && c.Description.Contains(search)));
            }

            var count = await query.CountAsync();

            var paginationMetaData = new PaginationMetadata(pageSize, count, pageNumber);

            var cities = await query.OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (cities, paginationMetaData);
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
