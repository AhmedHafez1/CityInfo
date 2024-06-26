﻿using CityInfo.API.Entities;
using CityInfo.API.Models;

namespace CityInfo.API.Repositories
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? search, int pageNumber, int pageSize);
        Task<City?> GetCityAsync(int cityId);
        Task<bool> CityExistsAsync(int cityId);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?> GetOnePointOfInterestAsync(int cityId, int pointId);
        Task<PointOfInterest> AddPointOfInterestAsync(int cityId, PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();
    }
}
