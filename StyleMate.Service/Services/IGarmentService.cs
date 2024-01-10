using StyleMate.Data.EntityModels;
using System;

namespace StyleMate.Service.Services
{
    /// <summary>
    /// Interface defining the garment service
    /// </summary>
    public interface IGarmentService
    {
        /// <summary>
        /// Get a list with all garments
        /// </summary>
        /// <returns>The list of garments</returns>
        List<StyleMateGarment> GetStyleMateGarment();
        List<StyleMateGarment> Get10StyleMateGarment(string type, string gender);
    }
}