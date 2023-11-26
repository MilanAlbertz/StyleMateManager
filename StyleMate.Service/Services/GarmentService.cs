using StyleMate.Data;
using StyleMate.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

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
    }
}