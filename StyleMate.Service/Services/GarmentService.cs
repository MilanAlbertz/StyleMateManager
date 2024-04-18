using StyleMate.Data;
using StyleMate.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StyleMate.Service.Services
{
    public class GarmentService : IGarmentService
    {
        private readonly StyleMateDataContext _dbContext;

        public GarmentService(StyleMateDataContext context)
        {
            _dbContext = context;
        }

        public List<StyleMateGarment> GetStyleMateGarment()
        {
            var garments = _dbContext.StyleMateGarments
                .Include(g => g.ImageUrls)
                .ToList();
            return garments;
        }
        public List<StyleMateGarment> Get10StyleMateGarment(string type, string gender)
        {
            var query = _dbContext.StyleMateGarments.AsQueryable();

            if (!string.IsNullOrEmpty(type) && type != "Everything")
            {
                query = query.Where(g => g.Type == type);
            }

            if (!string.IsNullOrEmpty(gender))
            {
                query = query.Where(g => g.Gender == gender);
            }

            query = query.Include(g => g.ImageUrls)
                         .Where(g => g.ImageUrls.Any(i => i.Url != null));

            try
            {
                var randomGarments = query.ToList();

                // Shuffle the list of garments
                var random = new Random();
                randomGarments = randomGarments.OrderBy(g => random.Next()).ToList();

                // Take the first 10 garments
                randomGarments = randomGarments.Take(10).ToList();

                return randomGarments;
            }
            catch (Exception ex)
            {
                // Handle exception
                return null;
            }
        }

        public List<StyleMateGarment> GetLikedGarments(string userId)
        {
            var garmentIds = _dbContext.LikedGarments
                           .Where(ul => ul.UserId == userId)
                           .Select(ul => ul.GarmentId)
                           .ToList();

            return _dbContext.StyleMateGarments
                           .Where(g => garmentIds.Contains(g.Id)).Include(g => g.ImageUrls)
                           .Where(g => g.ImageUrls.Any(i => i.Url != null))
                           .ToList();
        }

        public Task StoreLikedGarmentToUser(int garmentId, string userId)
        {
            _dbContext.Add(new UserLikedGarment() { GarmentId = garmentId, UserId = userId });
            _dbContext.SaveChanges();

            return Task.CompletedTask;
        }
    }
}