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
            List<StyleMateGarment> garments = _dbContext.StyleMateGarments.ToList();
            return garments;
        }
    }
}